using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

public class SpinnerDG: MonoBehaviour 
{	
	public List<float> weightList;
	public List<int> prizeList;

	public float defaultAngle = 360f * 5;
	
	protected bool isRotating;
	protected float rotateSpeed;
	protected float rotateAcceleration;
	protected List<float> angleList;

	private float angleUnit;
	
	void Start()
	{
		isRotating = false;
		angleUnit = 360f/weightList.Count;
		angleList = new List<float>();
		for (int i = 0; i < weightList.Count; i++)
		{
			float angle = defaultAngle + i * angleUnit;
			angleList.Add(angle);
			prizeList.Add(i);
		}
	}
	public void Spin()
	{
		if (!isRotating)
		{
			isRotating = true;
			int randomAngleIndex = BiasRandom(weightList);
			float rotateAngle = angleList[randomAngleIndex];
			Vector3 rotateEnd = transform.eulerAngles - Vector3.forward * rotateAngle;
			Shift<int>(randomAngleIndex,ref prizeList);
			Shift<float>(randomAngleIndex, ref weightList);
			transform.DORotate(rotateEnd, 5.0f, RotateMode.FastBeyond360).OnComplete(RotateEnd);
		}
	}

	void RotateEnd()
	{
		/*
		 * You may need to add something like reward here
		 */
		Debug.Log("CONGRATULATIONS! GET PRIZE AT INDEX " + prizeList[0]);
		isRotating = false;
	}

	void Shift<T>(int shift,ref List<T> list)
	{
		List<T> newList = new List<T>();
		for (int i = 0; i < weightList.Count; i++)
		{
			newList.Add(list[(i + shift) % list.Count]);
		}
		list = newList;
	}
	
	int BiasRandom(List<float> weightList)
	{
		float total = weightList.Sum();
		if (total == 0) 
		{
			return 0;
		}
		float rand = Random.Range(0, total);
		for (int i = 0; i < weightList.Count; i++)
		{
			rand -= weightList[i];
			if (rand <= 0)
			{
				return i;
			}
		}
		return 0;
	}
}