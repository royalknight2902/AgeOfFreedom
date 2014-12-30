using UnityEngine;
using System.Collections;

public class BulletArchitect : BulletTemplate
{
    [HideInInspector]
    public const float kMinVelocity = 5f;

    private Vector3 prePosition;
    private Transform target;

    private float m_fX;
    private float m_fY;
    private Vector3 m_startPosition;
    private float m_fTime = 0.0f;
    private float m_fAngle;
    private float m_fVelocity;
    private float g = Mathf.Abs(Physics.gravity.y);

    public override void Initalize(BulletController controller)
    {
        isCollision = true;
        controller.gameObject.SetActive(false);
        updateController(controller);
        target = enemy.transform;
        m_startPosition = bulletController.gameObject.transform.localPosition;

        // tinh khoang cach giua vien dan va muc tieu
        m_fX = bulletController.gameObject.transform.InverseTransformPoint(target.position).x;
        m_fY = bulletController.gameObject.transform.InverseTransformPoint(target.position).y;

        // tinh toan van toc ban dau va goc ban cua dan
        calculatorByTime(calculatorByVelocity(Random.Range(55, 60)));

        if (m_fX >= 0)
        {
            controller.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * m_fAngle));
        }
        else
        {
            controller.transform.Rotate(new Vector3(0, 0, 180 - Mathf.Rad2Deg * m_fAngle));
        }

        prePosition = controller.gameObject.transform.position;

        controller.gameObject.SetActive(true);
    }

    public override void Update()
    {
        // lay goc cu cua mui ten
        float Vx_old = m_fVelocity * Mathf.Cos(m_fAngle);
        float Vy_old = m_fVelocity * Mathf.Sin(m_fAngle) - g * m_fTime;
        float angle_old = Mathf.Rad2Deg * Mathf.Atan(Vy_old / Vx_old);

        m_fTime += Time.deltaTime * 6;

        Vector3 temp = Vector3.zero;
        if (m_fX < 0)
            temp.x = m_startPosition.x - m_fVelocity * m_fTime * Mathf.Cos(m_fAngle);
        else
            temp.x = m_startPosition.x + m_fVelocity * m_fTime * Mathf.Cos(m_fAngle);
        temp.y = m_startPosition.y + m_fVelocity * m_fTime * Mathf.Sin(m_fAngle)
            + 0.5f * Physics.gravity.y * m_fTime * m_fTime;
        bulletController.gameObject.transform.localPosition = temp;

        float Vx = m_fVelocity * Mathf.Cos(m_fAngle);
        float Vy = m_fVelocity * Mathf.Sin(m_fAngle) - g * m_fTime;
        if (m_fX >= 0)
        {
            bulletController.transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan(Vy / Vx) - angle_old));
        }
        else
        {
            bulletController.transform.Rotate(new Vector3(0, 0, -(Mathf.Rad2Deg * Mathf.Atan(Vy / Vx) - angle_old)));
        }

        Transform transform = bulletController.gameObject.transform;
        // if enemy destroy after create bullet
        if (enemy == null)
        {
            if (Vector3.Distance(transform.position, prePosition) <= PlayConfig.DistanceBulletChase)
            {
                MonoBehaviour.Destroy(bulletController.gameObject);
            }
        }

        if (m_fTime >= Mathf.Abs(m_fX) / (m_fVelocity * Mathf.Cos(m_fAngle)) + 1)
        {
            MonoBehaviour.Destroy(bulletController.gameObject);
        }
    }

    private void calculatorByTime(float time_move)
    {
        float absX = 0;
        if (Mathf.Abs(m_fX) < 5)
            absX = 5;
        else
            absX = Mathf.Abs(m_fX);
        //float absY = Mathf.Abs(m_fY);

        //float Vomin = absX / time_move;
        m_fAngle = Mathf.Atan(m_fY / absX + g * Mathf.Pow(time_move, 2) / (2 * absX));
        m_fVelocity = absX / (time_move * Mathf.Cos(m_fAngle));
    }

    private float calculatorByVelocity(float Vo)
    {
        float absX = 0;
        if (Mathf.Abs(m_fX) < 5)
            absX = 5;
        else
            absX = Mathf.Abs(m_fX);
        //float absY = Mathf.Abs(m_fY);

        float a = g * Mathf.Pow(absX, 2) / (2 * Mathf.Pow(Vo, 2));
        float c = g * Mathf.Pow(absX, 2) / (2 * Mathf.Pow(Vo, 2)) + m_fY;

        Vector3 v = calculatorEquation2(a, -absX, c);

        // return time for move
        if (v.x == 0)
            return Random.Range(1.5f, 2.5f);
        else
        {
            float alpha1 = Mathf.Atan(v.y);
            float alpha2 = Mathf.Atan(v.z);

            float time1 = absX / (Vo * Mathf.Cos(alpha1));
            float time2 = absX / (Vo * Mathf.Cos(alpha2));

            float time = time1 >= time2 ? time1 : time2;
            if (time <= 1.5 || time >= 2.5)
                return Random.Range(1.5f, 2.5f);
            else
                return time;
        }
    }

    Vector3 calculatorEquation2(float a, float b, float c)
    {
        float delta = Mathf.Pow(b, 2) - 4 * a * c;

        if (delta < 0)
        {
            return new Vector3(0, 0, 0);
        }
        else if (delta == 0)
        {
            float x = -b / (2 * a);
            return new Vector3(1, x, x);
        }
        else
        {
            float x1 = (-b + Mathf.Sqrt(delta)) / (2 * a);
            float x2 = (-b - Mathf.Sqrt(delta)) / (2 * a);
            return new Vector3(1, x1, x2);
        }
    }
}
