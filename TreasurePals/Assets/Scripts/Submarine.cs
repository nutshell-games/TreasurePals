using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

///<summary> Submarine creates diver placeholders and their animation
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
	/// <summary>
	/// Removes diver placeholders
	/// </summary>
	/// <returns>The all divers.</returns>
	public IEnumerator DestroyAllDivers(){
		Debug.LogError ("Destroying all diver placeholders");
		yield return new WaitForSeconds (2.0f);
		for (int i = ListOfDivers.Count - 1; i >= 0; i--) {
			Destroy (ListOfDivers [i]);
		}
		ListOfDivers.Clear ();
	}

	/// <summary>
	/// Animates divers out of submarine to a fixed position
	/// </summary>
	/// <returns>The divers.</returns>
	/// <param name="numOfDivers">Number of divers.</param>
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
		foreach(Transform child in PositionParent.transform){
			Destroy (child.gameObject);
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

	/// <summary>
	/// Moves the diver of ListOfDiver[diverIndex] to new location of TreasureLocation[locationIndex-1]
	/// </summary>
	/// <param name="diverIndex">Diver index.</param>
	/// <param name="locationIndex">Location index.</param>
	public void MoveDiverToSpot(int diverIndex, int locationIndex){
		if (locationIndex == -1) {
			subAnim.MoveDiverTo (ListOfDivers [diverIndex], subAnim.subScript.transform);
		} else {
			//NEED TO DO THIS AGAIN TO ACCOUNT FOR ALL TREASURES
			subAnim.MoveDiverTo (ListOfDivers [diverIndex], TreasurePlaceholderManager.instance.TreasureLocations [locationIndex - 1]);
		}
	}

	void MoveDiverToSubmarine(){
	
	}

}
