using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class GM : MonoBehaviour {

	public int enemyShipIncremement;
	public GameObject enemyShipPrefab;
	public float leftBound, rightBound, topBound;

	TransformAccessArray transforms;
	MovementJobTwo moveJob;
	JobHandle moveHandle;

	private void Awake()
	{
		transforms = new TransformAccessArray(100);
	}

	[ComputeJobOptimization]
	public struct MovementJobTwo : IJobParallelForTransform
	{

		public float moveSpeed;
		public float topBound;
		public float bottomBound;
		public float deltaTime;



		public void Execute(int index, TransformAccess transform)
		{
			Vector3 pos = transform.position;
			pos += moveSpeed * deltaTime * (transform.rotation * new Vector3(0f, 0f, 1f));

			/*if (pos.z < bottomBound)
				pos.z = topBound;*/

			transform.position = pos;
		}
	}

	public void Update()
	{
		moveHandle.Complete();

		if (Input.GetKeyDown("space"))
			AddShips(enemyShipIncremement);

		moveJob = new MovementJobTwo
		{
			moveSpeed = 1f,
			topBound = topBound,
			bottomBound = 200,
			deltaTime = Time.deltaTime
		};

		Debug.Log(transforms.length);

		moveHandle = moveJob.Schedule(transforms);

		JobHandle.ScheduleBatchedJobs();


	}

	void AddShips(int amount)
	{
		moveHandle.Complete();

		transforms.capacity = transforms.length + amount;

		for (int a=0; a<amount; a++)
		{
			float xVal = Random.Range(0, 200);
			float zVal = 0f;

			Vector3 pos = new Vector3(xVal, 0f, zVal + 0);
			Quaternion rot = Quaternion.Euler(0f, 180f, 0f);

			var obj = Instantiate(enemyShipPrefab, pos, rot) as GameObject;
			obj.SetActive(true);
			transforms.Add(obj.transform);
		}

	}
}
