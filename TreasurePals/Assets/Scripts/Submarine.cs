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
	public SubAnimator subAnim;

	public List<GameObject> ListOfDivers = new List<GameObject>();
	public List<GameObject> ListOfPositions = new List<GameObject> ();


	public void Dive(){
		subAnim.AllAboard ();
	}


	public IEnumerator UnloadDivers(int numOfDivers){
		CreateDivers (numOfDivers);
		yield return new WaitForSeconds (0.5f);
		subAnim.AllExit (ListOfDivers, ListOfPositions);	
		yield return new WaitForSeconds (0.5f);
	}


	void CreateDivers(int num){//destroys all existing divers and create new ones
		for (int i = ListOfDivers.Count-1; i >= 0; i--) {
			Destroy (ListOfDivers [i]);
		}
		ListOfDivers.Clear ();
		//create position placeholders;
		for (int i = 0; i < num; i++) {
			ListOfPositions.Add(Instantiate (PositionPrefab, PositionParent.transform) as GameObject);
			GameObject diver = Instantiate (diversPrefab, transform) as GameObject;
			diver.transform.localScale = diversPrefab.transform.localScale;
			ListOfDivers.Add (diver);
		}
	}

	public void MoveDiverToSpot(int diverIndex, int locationIndex){
		if (locationIndex == -1) {
			subAnim.MoveDiverTo (ListOfDivers [diverIndex], subAnim.subScript.transform);
		} else {
			//NEED TO DO THIS AGAIN TO ACCOUNT FOR ALL TREASURES
			subAnim.MoveDiverTo (ListOfDivers [diverIndex], TreasureManager.instance.TreasureLocations [locationIndex - 1]);
		}
	}

	void MoveDiverToSubmarine(){
	
	}

}
