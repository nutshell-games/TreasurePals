using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tabletop;

public class GameStateManager : MonoBehaviour {

	StateMachine stateMachine;

	// Use this for initialization
	void Start () {
	
	}

	void Awake() {

		stateMachine = new StateMachine ();

		List<PlayerColors> selectedPlayers = new List<PlayerColors> ();
		selectedPlayers.Add (PlayerColors.Red);
		selectedPlayers.Add (PlayerColors.Green);
		selectedPlayers.Add (PlayerColors.Orange); 
		selectedPlayers.Add (PlayerColors.Blue);
		stateMachine.setupGameForPlayers (selectedPlayers);

	}
	
	// Update is called once per frame
	void Update () {

		// if trigger start next round
		if (Input.GetKeyUp("n"))
		{
			stateMachine.startNextRound();

		}
		// start player's turn
		else if (Input.GetKeyUp("t"))
		{
			stateMachine.startNextTurn();
		}
		// roll for player
		else if (Input.GetKeyUp("r"))
		{
			stateMachine.rollForCurrentPlayer();
			stateMachine.setCurrentPlayerRoll(stateMachine.lastRoll);
		}
		// choose direction DOWN
		else if (Input.GetKeyUp("d"))
		{
			stateMachine.commitMovement();
		}
		// choose direction UP
		else if (Input.GetKeyUp("u"))
		{
			stateMachine.directCurrentPlayerToShip();
			stateMachine.commitMovement();
		}
		// if TreasureAvailable
		// take treasure
		else if (Input.GetKeyUp("p"))
		{
			stateMachine.selectTreasure(true);
		}
		// leave treasure
		else if (Input.GetKeyUp("o"))
		{
			stateMachine.selectTreasure(false);
		}
		// start next turn
		else if (Input.GetKeyUp("e"))
		{
			stateMachine.endTurn();
		}

	
	}
}
