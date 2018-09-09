using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class Rotator : MonoBehaviour {

	
	//BEFORE

	// // Update is called once per frame
	// void Update () {
	// 	transform.Rotate(Vector3.right * velocity * Time.deltaTime);
	// }


	 public float velocity;
}

class RotatorSystem : ComponentSystem
{

	struct Components
	{
		public Rotator rotator;
		public Transform transform;
	}

	protected override void OnUpdate(){

		float dTime = Time.deltaTime;

		var _components = GetEntities<Components>();

		for( int a=0; a<_components.Length; a++ ){
			_components[a].transform.Rotate(0f, _components[a].rotator.velocity * dTime, 0f);
		}
	}

}