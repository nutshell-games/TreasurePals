using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tabletop;

public class GameStateManager : MonoBehaviour {

	StateMachine stateMachine;
	List<PlayerColors> selectedPlayers;

	// Use this for initialization
	void Start () {
	
	}

	void Awake() {

		stateMachine = new StateMachine ();

		selectedPlayers = new List<PlayerColors> ();
	}
	
	// Update is called once per frame
	void Update () {

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
