using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour {

	void Update()
	{
		Vector3 pos = transform.position;
		pos += transform.forward * 1f * Time.deltaTime;

		if (pos.z < -500)
			pos.z = 0;

		transform.position = pos;
	}
	

}
