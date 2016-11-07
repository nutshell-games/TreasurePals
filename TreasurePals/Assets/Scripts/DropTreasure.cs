using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Tabletop;

public class DropTreasure : MonoBehaviour {
	public GameObject TreasureButtonPrefab;
	// Use this for initialization

	[SerializeField]
	Transform GridParent;

	public void PopulateTreasuresButtons(){
		foreach (Transform t in GridParent) {
			Destroy (t.gameObject);
		}

		List<Treasure> personsTreasure = GameStateManager.instance.stateMachine.currentPlayer.collectedTreasures;
		Debug.LogError ("You have " + personsTreasure.Count + "treasures");
		for (int i = 0; i < personsTreasure.Count; i++) {
			GameObject btn = Instantiate (TreasureButtonPrefab, GridParent) as GameObject;
			btn.transform.localScale = new Vector3 (1f, 1f, 1f);
			int treasureIndex = i;
			TreasureType t = personsTreasure [treasureIndex].type;
			btn.GetComponent<TreasurePlaceholderPrefab> ().SetTreasurePrefabTo (t);
			Debug.LogError ("Adding index value to button listener " + i);
			btn.GetComponent<Button> ().onClick.AddListener (() => Debug.LogError ("Dropping treasure, " + treasureIndex));
			btn.GetComponent<Button> ().onClick.AddListener (() => Debug.LogError (personsTreasure [treasureIndex]));
			btn.GetComponent<Button>().onClick.AddListener(() => GameStateManager.instance.Button_DropTreasure(treasureIndex, t));
			btn.GetComponent<Button>().onClick.AddListener(() => gameObject.SetActive(false));
		}

	}
}
