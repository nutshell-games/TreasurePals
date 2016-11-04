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
			GameObject prefabRef = null;
			switch (treasureLocationReference [i].treasure.type) {
			case TreasureType.A:
				prefabRef = Tier1TreasurePrefab;
				break;
			case TreasureType.B:
				prefabRef = Tier2TreasurePrefab;
				break;
			case TreasureType.C:
				prefabRef = Tier3TreasurePrefab;
				break;
			case TreasureType.D:
				prefabRef = Tier4TreasurePrefab;
				break;
			case TreasureType.E:
				prefabRef = Tier5TreasurePrefab;
				break;
			default:
				break;
			}	

			GameObject tp = Instantiate (prefabRef, transform) as GameObject;
			tp.transform.localScale = prefabRef.transform.localScale;
			tp.transform.position = TreasureLocations [i].position;

		treasureLocationReference [i].physicalLocation = TreasureLocations [i].gameObject;

		}
	}

}
