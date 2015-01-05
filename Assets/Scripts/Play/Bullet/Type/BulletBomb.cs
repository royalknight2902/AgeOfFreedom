using UnityEngine;
using System.Collections;

public class BulletBomb : BulletTemplate
{
    [HideInInspector]
    public const float kMinVelocity = 5f;

    private Transform target;

    private float m_fX;
    private float m_fY;
    private Vector3 m_startPosition;
    private float m_fTime = 0.0f;
    private float m_fAngle;
    private float m_fVelocity;
    private float g = Mathf.Abs(Physics.gravity.y);

    //public bool canCollision;
    AutoExplosion explosion;

    public BulletBomb()
        : base()
    {
        isCollision = false;
    }
    public override void Initalize(BulletController controller)
    {
        updateController(controller);
        explosion = controller.GetComponentInChildren<AutoExplosion>();

        target = enemy.transform;
        m_startPosition = bulletController.gameObject.transform.localPosition;

        // tinh khoang cach giua vien dan va muc tieu
        m_fX = bulletController.gameObject.transform.InverseTransformPoint(target.position).x;

        if (m_fX >= 0 && m_fX < 5)
        {
            m_fX = 5;
        }
        else if(m_fX < 0 && m_fX > -5)
        {
            m_fX = -5;
        }

        m_fY = bulletController.gameObject.transform.InverseTransformPoint(target.position).y;

        calculatorByTime(calculatorByVelocity(Random.Range(55, 60)));
        //canCollision = false;
    }

    public override void Update()
    {
        m_fTime += Time.deltaTime * 18;

        if (m_fTime >= (Mathf.Abs(m_fX) / (m_fVelocity * Mathf.Cos(m_fAngle))))
        {
            isCollision = true;
            explosion.initalizeDamageExplosion(bulletController.ATK);
            MonoBehaviour.Destroy(bulletController.gameObject);
        }

        Vector3 temp = Vector3.zero;
        if (m_fX < 0)
            temp.x = m_startPosition.x - m_fVelocity * m_fTime * Mathf.Cos(m_fAngle);
        else
            temp.x = m_startPosition.x + m_fVelocity * m_fTime * Mathf.Cos(m_fAngle);
        temp.y = m_startPosition.y + m_fVelocity * m_fTime * Mathf.Sin(m_fAngle)
            + 0.5f * Physics.gravity.y * m_fTime * m_fTime;
        bulletController.gameObject.transform.localPosition = temp;

    }

    #region
    //private void calculatorBullet()
    //{
    //    // tinh khoang cach giua vien dan va muc tieu
    //    m_fX = bulletController.gameObject.transform.InverseTransformPoint(target.position).x;
    //    m_fY = bulletController.gameObject.transform.InverseTransformPoint(target.position).y;

    //    float absX = Mathf.Abs(m_fX);
    //    float absY = Mathf.Abs(m_fY);

    //    // xet goc ban
    //    int angleMin;
    //    int angleMax;
    //    if (m_fY < 0)
    //        angleMin = 10;
    //    else
    //        angleMin = (int)(Mathf.Rad2Deg * Mathf.Atan(absY / absX));
    //    if (angleMin < 30)
    //        angleMin = 30;

    //    angleMax = angleMin + 1;
    //    if (angleMax > 90)
    //    {
    //        m_fAngle = angleMin;
    //    }
    //    else
    //        m_fAngle = Random.Range(angleMin, angleMax + 1);

    //    float g = Mathf.Abs(Physics.gravity.y);
    //    float radAngle = Mathf.Deg2Rad * m_fAngle;
    //    float temp = g * Mathf.Pow(absX, 2) / (2 * Mathf.Pow(Mathf.Cos(radAngle), 2) * (Mathf.Tan(radAngle) * absX - m_fY));
    //    Debug.Log(angleMin);
    //    Debug.Log(m_fAngle);
    //    Debug.Log(temp);
    //    if (temp > Mathf.Pow(kMinVelocity, 2))
    //    {
    //        m_fVelocity = Mathf.Sqrt(temp);
    //    }
    //    else
    //    {
    //        isBulletBomb = false;
    //        m_fVelocity = kMinVelocity;
    //    }

    //    m_fTimeSpeed *= 1 + (float)(m_fAngle - angleMin) / 90;
    //}
    #endregion

    private void calculatorByTime(float time_move)
    {
        float absX = 0;
        absX = Mathf.Abs(m_fX);
        //float absY = Mathf.Abs(m_fY);

        //float Vomin = absX / time_move;
        m_fAngle = Mathf.Atan(m_fY / absX + g * Mathf.Pow(time_move, 2) / (2 * absX));
        m_fVelocity = absX / (time_move * Mathf.Cos(m_fAngle));
    }

    private float calculatorByVelocity(float Vo)
    {
        float absX = 0;
        absX = Mathf.Abs(m_fX);
        //float absY = Mathf.Abs(m_fY);

        float a = g * Mathf.Pow(absX, 2) / (2 * Mathf.Pow(Vo, 2));
        float c = g * Mathf.Pow(absX, 2) / (2 * Mathf.Pow(Vo, 2)) + m_fY;

        Vector3 v = calculatorEquation2(a, -absX, c);

        // return time for move
        if (v.x == 0)
            return 10;
        else
        {
            float alpha1 = Mathf.Atan(v.y);
            float alpha2 = Mathf.Atan(v.z);

            float time1 = absX / (Vo * Mathf.Cos(alpha1));
            float time2 = absX / (Vo * Mathf.Cos(alpha2));

            float time = time1 >= time2 ? time1 : time2;
            if (time <= 8 || time >= 10)
                return 10;
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