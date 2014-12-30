using UnityEngine;
using System.Collections;

public class BulletChase : BulletTemplate
{
    private Vector3 prePosition;

    public BulletChase()
        : base()
    {
        isCollision = true;
    }

    public override void Initalize(BulletController controller)
    {
        // ẩn viên đạn, xác định mục tiêu và hướng (tránh lỗi ngay từ đầu)
        controller.gameObject.SetActive(false);

        updateController(controller);

        Update();
        controller.gameObject.SetActive(true);
    }

    public override void Update()
    {
        Transform transform = bulletController.gameObject.transform;

        Vector3 relative = transform.InverseTransformPoint(prePosition);
        float angle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, angle);

        // if enemy destroy after create bullet
        if (enemy == null)
        {
            if (Vector3.Distance(transform.position, prePosition) <= PlayConfig.DistanceBulletChase)
            {
                MonoBehaviour.Destroy(bulletController.gameObject);
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, prePosition, bulletController.Speed * PlayerInfo.Instance.userInfo.timeScale / 1000);
            }
        }
        else
        {
            prePosition = enemy.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, prePosition, bulletController.Speed * PlayerInfo.Instance.userInfo.timeScale / 1000);
            if (isCollision && Vector3.Distance(transform.position, prePosition) <= PlayConfig.DistanceBulletChase)
            {
                MonoBehaviour.Destroy(bulletController.gameObject);
            }
        }
    }

}
