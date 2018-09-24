using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class UpdateWithListsStress : MonoBehaviour {

	public int listLength;

	private NativeArray<float> floatLists;
	
	private JobHandle jobHandle;

	// Use this for initialization
	void Awake () {
		floatLists = new NativeArray<float>(listLength, Allocator.Persistent);
	}

	private void Update()
	{

		LoopJob loopJob = new LoopJob
		{
			//floatList = _temp
			floatList = floatLists
		};


		loopJob.Schedule(listLength, 64).Complete();

		loopJob.floatList.CopyTo(floatLists);

		Debug.Log(floatLists.Length + " : " + floatLists[0]);

		//loopJob.floatList.Dispose();

	}

	[ComputeJobOptimization]
	public struct LoopJob : IJobParallelFor
	{
		public NativeArray<float> floatList;

		public void Execute(int index)
		{
			var _next = floatList[index];
			_next += 0.1f;
			floatList[index] = _next;
		}
	}

}

