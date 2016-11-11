using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Tabletop;
public class TreasurePlaceholderManager : MonoBehaviour {
	public static TreasurePlaceholderManager instance;

	public List<Transform> TreasureLocations = new List<Transform>();

	public List<GameObject> TreasurePlaceholders = new List<GameObject> ();
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
	public void TreasurePlaceholderPlacement(){
		List<TreasureLocation> treasureLocationReference = GameStateManager.instance.stateMachine.treasureLocations;
		int numTreasures = treasureLocationReference.Count;
		for (int i = 0; i < numTreasures; i++) {
			if (treasureLocationReference [i].treasure != null) {
				GameObject tp = Instantiate (TreasurePrefabUI, transform) as GameObject;
				TreasurePlaceholderPrefab tbScript = tp.GetComponent<TreasurePlaceholderPrefab> ();
				tp.transform.localScale = TreasurePrefabUI.transform.localScale;
				tp.transform.position = TreasureLocations [i].position;
				tbScript.SetTreasurePrefabTo (treasureLocationReference [i].treasure.type);
				TreasurePlaceholders.Add (tp);
			}

		}
	}

	public IEnumerator DestroyAllTreasurePlaceHolder(){
		Debug.LogError ("Destroying all placeholders..");
		yield return new WaitForSeconds (0.2f);
		for (int i = TreasurePlaceholders.Count - 1; i >= 0; i--) {
			Destroy (TreasurePlaceholders [i]);
		}
		TreasurePlaceholders.Clear ();
	}

	public void RemoveTreasureFromLocation(){
		int treasureLocationIndex = GameStateManager.instance.stateMachine.currentPlayer.currentPosition;
		TreasurePlaceholders [treasureLocationIndex].GetComponent<TreasurePlaceholderPrefab> ().SetTreasurePrefabTo (TreasureType.E);
	}



	public void PutTreasureBack(int treasureIndex, TreasureType type){
		Debug.LogError ("Changing treasure Image");
		int treasureLocationIndex = GameStateManager.instance.stateMachine.currentPlayer.currentPosition;
		Debug.LogError ("Passing to treasures UI");
		TreasurePlaceholders [treasureLocationIndex].GetComponent<TreasurePlaceholderPrefab> ().SetTreasurePrefabTo (type);

	}


}
