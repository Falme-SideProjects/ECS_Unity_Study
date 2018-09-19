using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMCreateShips : MonoBehaviour {

	public int enemyShipIncremement;
	public GameObject enemyShipPrefab;
	public float leftBound, rightBound, topBound;

	void Update()
	{
		if (Input.GetKeyDown("space"))
			AddShips(enemyShipIncremement);
	}

	void AddShips(int amount)
	{
		for (int i = 0; i < amount; i++)
		{
			float xVal = Random.Range(leftBound, rightBound);
			float zVal = Random.Range(0f, 10f);

			Vector3 pos = new Vector3(xVal, 0f, zVal + topBound);
			Quaternion rot = Quaternion.Euler(0f, 180f, 0f);

			var obj = Instantiate(enemyShipPrefab, pos, rot) as GameObject;
		}
	}

}
