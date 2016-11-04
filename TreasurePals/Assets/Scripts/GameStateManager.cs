using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tabletop;

public class GameStateManager : MonoBehaviour {
	public static GameStateManager instance;

	public StateMachine stateMachine = new StateMachine();
	List<PlayerColors> selectedPlayers = new List<PlayerColors> ();

	public Submarine subScript; //submarine script..
	public MenuManager Menu; // Menu's script

	void Awake() {
		if (instance == null) {
			instance = this;
		} else
			Destroy (this);
	}

	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown ("l")) {
			StartSequence ();
		}
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

	public void StartSequence(){
		subScript.Dive ();
		StartCoroutine (OpenNumPlayerMenuWithDelay ());
	}
	
	IEnumerator OpenNumPlayerMenuWithDelay (){
		Debug.LogError ("Opening menus in 3 seconds");
		yield return new WaitForSeconds (3.0f);
		//subScript.DisableBubbles ();
		Menu.OpenNumberOfPlayer ();
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

		//number of players selected, move player selection menu away, 
		//Player order menu pops up.

	}



	public void SelectPlayerOrder(){
		// number of players selected.
		//move player selection menu away.
		// return back to diving submarine,
	}

	public void StartGameSequence(){
		StartCoroutine (gameSequenceWithDelay ());
		//animate submarine to the top,
		//spawn divers = number of players while animating them.



		//slowly pan down while treasures get spawned with bubble effect.
		//pan back up after all treasures have spawned.  
		//begin turn 1, round 1.
	}

	IEnumerator gameSequenceWithDelay(){
		SetNumPlayers (2);
		Menu.CloseMenus ();
		subScript.CreateDivers (stateMachine.numberOfPlayers);
		yield return new WaitForSeconds (1.0f);
		subScript.UnloadDivers ();
		yield return new WaitForSeconds (1.0f);
		TreasureManager.instance.InitialPrefabPlacement ();

	}





}
