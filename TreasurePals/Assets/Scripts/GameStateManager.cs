using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tabletop;

public class GameStateManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
	
	}

	void Awake() {

		StateMachine stateMachine = new StateMachine ();

		List<PlayerColors> selectedPlayers = new List<PlayerColors> ();
		selectedPlayers.Add (PlayerColors.Red);
		selectedPlayers.Add (PlayerColors.Green);
		selectedPlayers.Add (PlayerColors.Orange); 

		stateMachine.setupGameForPlayers (selectedPlayers);


	}
	
	// Update is called once per frame
	void Update () {


		// if trigger start next round


		// 
	
	}
}
