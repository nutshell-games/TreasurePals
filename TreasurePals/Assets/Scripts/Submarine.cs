using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Submarine : MonoBehaviour {
	[SerializeField]
	private GameObject diversPrefab;
	[SerializeField]
	private GameObject PositionPrefab;
	[SerializeField]
	private GameObject PositionParent;

	[SerializeField]
	private SubAnimator subAnim;

	public List<GameObject> ListOfDivers = new List<GameObject>();
	public List<GameObject> ListOfPositions = new List<GameObject> ();


	public void Dive(){
		subAnim.AllAboard ();

		//divers hop to sub,
		//sky moves away
		//submarine decends to middle
		// bubbles appear
		//fade away

	}

	public void UnloadDivers(){
		subAnim.AllExit (ListOfDivers, ListOfPositions);
	
	}


	public void CreateDivers(int num){
		//create position placeholders;
		for (int i = 0; i < num; i++) {
			ListOfPositions.Add(Instantiate (PositionPrefab, PositionParent.transform) as GameObject);
			GameObject diver = Instantiate (diversPrefab, transform) as GameObject;
			diver.transform.localScale = diversPrefab.transform.localScale;
			ListOfDivers.Add (diver);
		}
	}










}
