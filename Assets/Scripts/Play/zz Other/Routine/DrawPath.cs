using UnityEngine;
using System.Collections;

public class DrawPath : MonoBehaviour {

	private Transform[] Paths;

	void OnDrawGizmos()
	{
		Paths = gameObject.GetComponentsInChildren<Transform>();

		Gizmos.color = Color.blue;
		for (int i = 2; i < Paths.Length; i++)
		{
			Gizmos.DrawLine(Paths[i - 1].position, Paths[i].position);
			Gizmos.DrawWireSphere(Paths[i].position, 0.1f);
		}
	}
}
