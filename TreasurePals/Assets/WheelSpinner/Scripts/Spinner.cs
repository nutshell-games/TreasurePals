using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spinner: MonoBehaviour 
{	
	public List<float> weightList;
	public List<int> prizeList;

	public float defaultAngle = 360f*5;
	public float speedDownAngle = 360f*4;
	public float rotateMaxSpeed = 700f;
	public float rotateMinSpeed = 100f;

	protected bool isRotating;
	protected float rotateSpeed;
	protected float rotateAcceleration;
	protected List<float> angleList;

	private Vector3 curEuler;
	private float speedDownZ;
	private float angleUnit;

	void Start()
	{
		isRotating = false;
		curEuler = transform.eulerAngles;
		speedDownZ = curEuler.z - defaultAngle + speedDownAngle;
		angleUnit = 360f/weightList.Count;
		angleList = new List<float>();
		prizeList = new List<int>();
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
			rotateAcceleration = (rotateMaxSpeed * rotateMaxSpeed - rotateMinSpeed * rotateMinSpeed) / (2 * rotateAngle);
			rotateSpeed = rotateMaxSpeed;
			Shift<int>(randomAngleIndex,ref prizeList);
			Shift<float>(randomAngleIndex, ref weightList);
			StartCoroutine(SpinProcess(rotateAngle));
		}
	}

	private IEnumerator SpinProcess(float angle)
	{
		curEuler = transform.eulerAngles;
		Vector3 newAngle = curEuler - angle * Vector3.forward;
		while (curEuler.z > newAngle.z)
		{
			if (curEuler.z < speedDownZ)
			{
				rotateSpeed = Mathf.MoveTowards(rotateSpeed, rotateMinSpeed, rotateAcceleration*Time.deltaTime);
			}
			curEuler.z = Mathf.MoveTowards(curEuler.z, newAngle.z, rotateSpeed * Time.deltaTime);

			transform.eulerAngles = curEuler;
			yield return null;
		}
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