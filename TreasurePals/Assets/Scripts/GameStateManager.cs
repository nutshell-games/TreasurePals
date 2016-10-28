using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tabletop;

public class GameStateManager : MonoBehaviour {
	public static GameStateManager instance;

	public StateMachine stateMachine = new StateMachine();
	List<PlayerColors> selectedPlayers = new List<PlayerColors> ();

	// Use this for initialization
	void Start () {
		
	}

	void Awake() {
		if (instance == null) {
			instance = this;
		} else
			Destroy (this);
	}

	//Adds num number of players and add to statemachine
	public void SetNumPlayers(int num){
		switch (num) {
			case 6:
				Debug.LogError ("Adding 6th");
				selectedPlayers.Add (PlayerColors.Purple);
				goto case 5;
			case 5:
				Debug.LogError ("Adding 5th");
				selectedPlayers.Add (PlayerColors.Yellow);
				goto case 4;
			case 4:
				Debug.LogError ("Adding 4th");
				selectedPlayers.Add (PlayerColors.Green);
				goto case 3;
			case 3:
				Debug.LogError ("Adding 3rd");
				selectedPlayers.Add (PlayerColors.Blue);
				goto case 2;
			case 2:
				Debug.LogError ("Adding 2nd");
				selectedPlayers.Add (PlayerColors.Orange);
				goto case 1;
			case 1:
				Debug.LogError ("Adding 1st");
				selectedPlayers.Add (PlayerColors.Red);
				break;
			default:
				break;
		}
		stateMachine.setupGameForPlayers (selectedPlayers);
	}



	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("i")) {
			SetNumPlayers (5);
		}

		if (Input.GetKeyUp(KeyCode.Alpha2))
		{
			selectedPlayers.Add(PlayerColors.Red);
			selectedPlayers.Add(PlayerColors.Green);
			stateMachine.setupGameForPlayers(selectedPlayers);
		}
		else if (Input.GetKeyUp(KeyCode.Alpha3))
		{
			selectedPlayers.Add(PlayerColors.Red);
			selectedPlayers.Add(PlayerColors.Green);
			selectedPlayers.Add(PlayerColors.Orange);
			stateMachine.setupGameForPlayers(selectedPlayers);
		}
		else if (Input.GetKeyUp(KeyCode.Alpha4))
		{
			selectedPlayers.Add(PlayerColors.Red);
			selectedPlayers.Add(PlayerColors.Green);
			selectedPlayers.Add(PlayerColors.Orange);
			selectedPlayers.Add(PlayerColors.Blue);
			stateMachine.setupGameForPlayers(selectedPlayers);
		}
		else if (Input.GetKeyUp(KeyCode.Alpha5))
		{
			selectedPlayers.Add(PlayerColors.Red);
			selectedPlayers.Add(PlayerColors.Green);
			selectedPlayers.Add(PlayerColors.Orange);
			selectedPlayers.Add(PlayerColors.Blue);
			selectedPlayers.Add(PlayerColors.Purple);
			stateMachine.setupGameForPlayers(selectedPlayers);
		}
		else if (Input.GetKeyUp(KeyCode.Alpha6))
		{
			selectedPlayers.Add(PlayerColors.Red);
			selectedPlayers.Add(PlayerColors.Green);
			selectedPlayers.Add(PlayerColors.Orange);
			selectedPlayers.Add(PlayerColors.Blue);
			selectedPlayers.Add(PlayerColors.Purple);
			selectedPlayers.Add(PlayerColors.Yellow);
			stateMachine.setupGameForPlayers(selectedPlayers);
		}

		// if trigger start next round
		else if (Input.GetKeyUp("n"))
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

		// choose direction UP
		else if (Input.GetKeyUp("u"))
		{
			stateMachine.directCurrentPlayerToShip();
		}
		// MOVE
		else if (Input.GetKeyUp("m"))
		{
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

		// TreasureUnavailable and player has treasures
		else if (Input.GetKeyUp(KeyCode.Alpha0))
		{
			stateMachine.returnTreasure(0);
		}

		// start next turn
		else if (Input.GetKeyUp("e"))
		{
			stateMachine.endTurn();
		}

	
	}
}
