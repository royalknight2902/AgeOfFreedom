using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour 
{
    public float Speed;
    public bool isDown;
    public bool hasEffect;
    public EBulletEffect effect;

    public int ATK { get; set; }


    public virtual void initEffect(GameObject enemy)
    {
    }
}
