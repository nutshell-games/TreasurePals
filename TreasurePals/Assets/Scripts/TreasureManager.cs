using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tabletop;
public class TreasureManager : MonoBehaviour {
	public List<Transform> TreasureLocations = new List<Transform>();
	public List<Treasure> ListsOfTreasures = new List<Treasure>();
	[SerializeField]
	private GameObject Tier1TreasurePrefab;
	[SerializeField]
	private GameObject Tier2TreasurePrefab;
	[SerializeField]
	private GameObject Tier3TreasurePrefab;
	[SerializeField]
	private GameObject Tier4TreasurePrefab;


	[SerializeField]
	private int NumTreasureTier1;
	[SerializeField]
	private int NumTreasureTier2;
	[SerializeField]
	private int NumTreasureTier3;
	[SerializeField]
	private int NumTreasureTier4;
	[SerializeField]
	private int NumTreasureTier5;

	void Awake(){
		GetTreasureLocations ();
	}
	// Use this for initialization
	void Start () {
		GetTreasureLocations ();
	}

	void GetTreasureLocations(){
		foreach (Transform child in gameObject.transform) {
			TreasureLocations.Add (child);
			//child.gameObject.AddComponent<TreasureLocation> ();
		}
	}

	//place physical prefabs on treasure spots

	void InitialTreasurePlacement(){
		int locations = 0;
		for (int i = 0; i < NumTreasureTier1; i++) {
			GameObject treasure = Instantiate (Tier1TreasurePrefab, transform) as GameObject;
			treasure.transform.position = TreasureLocations [locations].position;
			locations++;
		}
		for (int i = 0; i < NumTreasureTier2; i++) {
			GameObject treasure = Instantiate (Tier2TreasurePrefab, transform) as GameObject;
			treasure.transform.position = TreasureLocations [locations].position;
			locations++;
		}
		for (int i = 0; i < NumTreasureTier3; i++) {
			GameObject treasure = Instantiate (Tier3TreasurePrefab, transform) as GameObject;
			treasure.transform.position = TreasureLocations [locations].position;
			locations++;
		}
		for (int i = 0; i < NumTreasureTier4; i++) {
			GameObject treasure = Instantiate (Tier4TreasurePrefab, transform) as GameObject;
			treasure.transform.position = TreasureLocations [locations].position;
			locations++;
		}


	}

}
