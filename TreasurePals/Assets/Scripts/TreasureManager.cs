using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tabletop;
public class TreasureManager : MonoBehaviour {
	public static TreasureManager instance;

	public List<Transform> TreasureLocations = new List<Transform>();
	[SerializeField]
	private GameObject Tier1TreasurePrefab;
	[SerializeField]
	private GameObject Tier2TreasurePrefab;
	[SerializeField]
	private GameObject Tier3TreasurePrefab;
	[SerializeField]
	private GameObject Tier4TreasurePrefab;
	[SerializeField]
	private GameObject Tier5TreasurePrefab;


	void Awake(){
		if (instance == null) {
			instance = this;
		} else
			Destroy (this);
		GetTreasureLocations ();
	}

	void GetTreasureLocations(){
		foreach (Transform child in gameObject.transform) {
			TreasureLocations.Add (child);
			//child.gameObject.AddComponent<TreasureLocation> ();
		}
	}

	//place physical prefabs on treasure spots

	public void InitialPrefabPlacement(){
		int numTreasures = TreasureLocations.Count;
		List<TreasureLocation> treasureLocationReference = GameStateManager.instance.stateMachine.treasureLocations;
		for (int i = 0; i < numTreasures; i++) {
			if (treasureLocationReference [i].treasure.type == TreasureType.A) {
				GameObject tp = Instantiate (Tier1TreasurePrefab, transform) as GameObject;
				tp.transform.position = TreasureLocations [i].position;
			}
			else if (treasureLocationReference [i].treasure.type == TreasureType.B) {
				GameObject tp = Instantiate (Tier2TreasurePrefab, transform) as GameObject;
				tp.transform.position = TreasureLocations [i].position;
				
			}
			else if (treasureLocationReference [i].treasure.type == TreasureType.C) {
				GameObject tp = Instantiate (Tier3TreasurePrefab, transform) as GameObject;
				tp.transform.position = TreasureLocations [i].position;
				
			}
			else if (treasureLocationReference [i].treasure.type == TreasureType.D) {
				GameObject tp = Instantiate (Tier4TreasurePrefab, transform) as GameObject;
				tp.transform.position = TreasureLocations [i].position;
				
			}
			else if (treasureLocationReference [i].treasure.type == TreasureType.E) {
				GameObject tp = Instantiate (Tier5TreasurePrefab, transform) as GameObject;
				tp.transform.position = TreasureLocations [i].position;
				
			}
			treasureLocationReference [i].physicalLocation = TreasureLocations [i].gameObject;
		}
	}

}
