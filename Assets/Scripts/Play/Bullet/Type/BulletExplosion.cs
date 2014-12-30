using UnityEngine;
using System.Collections;

public class BulletExplosion : BulletTemplate {

	Collider parentCollider;
	Collider childCollider;
	EBulletColliderType colliderType;

	public BulletExplosion()
		: base()
	{
		colliderType = EBulletColliderType.NONE;
        isCollision = true;
	}

	public override void Initalize(BulletController controller) 
	{
		updateController(controller);

		parentCollider = controller.collider;
		if(parentCollider is SphereCollider)
		{
			childCollider = controller.gameObject.GetComponentsInChildren<SphereCollider>()[1];
			colliderType = EBulletColliderType.SPHERE;
		}
		else if(parentCollider is BoxCollider)
		{
			childCollider = controller.gameObject.GetComponentsInChildren<BoxCollider>()[1];
			colliderType = EBulletColliderType.BOX;
		}
		else if(parentCollider is CapsuleCollider)
		{
			childCollider = controller.gameObject.GetComponentsInChildren<CapsuleCollider>()[1];
			colliderType = EBulletColliderType.CAPSULE;
		}

		getChildColliderValue();
	}
	
	public override void Update () 
	{
		getChildColliderValue();
	}

	void getChildColliderValue()
	{
		if (colliderType == EBulletColliderType.BOX)
		{
			BoxCollider childTemp = childCollider as BoxCollider;
			BoxCollider parentTemp = parentCollider as BoxCollider;

			parentTemp.center = childTemp.center;
			parentTemp.size = childTemp.size;
		}
		else if (colliderType == EBulletColliderType.CAPSULE)
		{
			CapsuleCollider childTemp = childCollider as CapsuleCollider;
			CapsuleCollider parentTemp = parentCollider as CapsuleCollider;

			parentTemp.center = childTemp.center;
			parentTemp.radius = childTemp.radius;
			parentTemp.height = childTemp.height;
		}
		else if(colliderType == EBulletColliderType.SPHERE)
		{
			SphereCollider childTemp = childCollider as SphereCollider;
			SphereCollider parentTemp = parentCollider as SphereCollider;

			parentTemp.center = childTemp.center;
			parentTemp.radius = childTemp.radius;
		}
	}
}
