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
	[SerializeField]
	private Text AirIndicatorSub;
	[SerializeField]
	private Text AirIndicatorMenu;

	public List<GameObject> ListOfDivers = new List<GameObject>();
	public List<GameObject> ListOfPositions = new List<GameObject> ();


	void Start(){
		GameStateManager.instance.stateMachine.AirDelegate += AirLevel;
	}
	
	public void Dive(){
		subAnim.AllAboard ();
	}

	/// <summary>
	/// Removes diver placeholders
	/// </summary>
	/// <returns>The all divers.</returns>
	public IEnumerator DestroyAllDivers(){
		Debug.LogError ("Destroying all diver placeholders");
		yield return new WaitForSeconds (0.2f);
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
		yield return new WaitForSeconds (0.1f);
		subAnim.AllExit (ListOfDivers, ListOfPositions);	
		yield return new WaitForSeconds (0.1f);
	}


	void CreateDivers(int num){//destroys all existing divers and create new ones
		for (int i = ListOfDivers.Count-1; i >= 0; i--) {
			Destroy (ListOfDivers [i]);
		}
		for (int i = ListOfPositions.Count-1; i >= 0; i--) {
			Destroy (ListOfPositions [i]);
		}
		ListOfDivers.Clear ();
		ListOfPositions.Clear ();
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
			subAnim.MoveDiverTo (ListOfDivers [diverIndex], TreasurePlaceholderManager.instance.TreasureLocations [locationIndex]);
		}
	}

	void MoveDiverToSubmarine(){
	
	}


	public void AirLevel(int n){
		AirIndicatorSub.text = n.ToString();
		AirIndicatorMenu.text = "Air: " + n.ToString();
	}

	public void ToggleDisplayAir(bool t){
		AirIndicatorSub.gameObject.SetActive (t);
	}
}
