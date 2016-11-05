using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tabletop;
public class TreasureManager : MonoBehaviour {
	public static TreasureManager instance;

	public List<Transform> TreasureLocations = new List<Transform>();

	public List<GameObject> TreasuresUI = new List<GameObject> ();
	[SerializeField]
	private GameObject TreasurePrefabUI;



	void Awake(){
		if (instance == null) {
			instance = this;
		} else
			Destroy (this);
		GetTreasureLocations ();
	}

	void Start(){
		//InitialPrefabPlacement ();
	}
	void GetTreasureLocations(){
		foreach (Transform child in gameObject.transform) {
			TreasureLocations.Add (child);
			//child.gameObject.AddComponent<TreasureLocation> ();
		}
	}

	//place physical prefabs on treasure spots

	public void TreasurePlacement(){
		List<TreasureLocation> treasureLocationReference = GameStateManager.instance.stateMachine.treasureLocations;
		int numTreasures = treasureLocationReference.Count;
		for (int i = 0; i < numTreasures; i++) {
			GameObject tp = Instantiate (TreasurePrefabUI, transform) as GameObject;
			TreasurePrefab tbScript = tp.GetComponent<TreasurePrefab> ();
			tp.transform.localScale = TreasurePrefabUI.transform.localScale;
			tp.transform.position = TreasureLocations [i].position;
			tbScript.SetTreasurePrefabTo (treasureLocationReference [i].treasure.type);

			TreasuresUI.Add (tp);
			treasureLocationReference [i].physicalLocation = TreasureLocations [i].gameObject;

		}
	}

	public void RemoveTreasure(int treasureIndex){
		TreasuresUI [treasureIndex - 1].GetComponent<TreasurePrefab> ().SetTreasurePrefabTo (TreasureType.E);
	}

	public void PlaceTreasure(){
	
	}


}
