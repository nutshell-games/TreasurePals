using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Tabletop;

public class GameStateManager : MonoBehaviour {


	public static GameStateManager instance;

	public StateMachine stateMachine = new StateMachine();
	List<PlayerColors> selectedPlayers = new List<PlayerColors> ();

	public int numberOfPlayers = 2;
	public Submarine subScript; //submarine script..controls submarine animations, contains list of Player UI elements
	public MenuManager Menu; // Menu's script

	void Awake() {
		Screen.SetResolution (600, 900, true);
		if (instance == null) {
			instance = this;
		} else
			Destroy (this);
	}

	public float transitionDelay = 0.5f;


	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("l")) {
			OpeningSequence ();
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
			stateMachine.setCurrentPlayerRoll();
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

	public void OpeningSequence(){
		subScript.Dive ();
		StartCoroutine (OpenNumPlayerMenuWithDelay ());
	}
	
	IEnumerator OpenNumPlayerMenuWithDelay (){
		Debug.LogError ("Opening menus in 3 seconds");
		yield return new WaitForSeconds (3.0f);
		BackGroundMusicManager.instance.SetBGM (BackGroundMusicManager.BGMType.setting);
		//subScript.DisableBubbles ();
		Menu.OpenNumberOfPlayer ();
	}

	public void SetNumberOfPlayers(int num){
		numberOfPlayers = num;
	}

	//Adds num number of players and add to statemachine
	public void SetNumPlayers(int num){
		selectedPlayers.Clear ();
		switch (num) {
		case 6:
			selectedPlayers.Add (PlayerColors.Red);
			goto case 5;
		case 5:
			selectedPlayers.Add (PlayerColors.Green);
			goto case 4;
		case 4:
			selectedPlayers.Add (PlayerColors.Orange);
			goto case 3;
		case 3:
			selectedPlayers.Add (PlayerColors.Blue);
			goto case 2;
		case 2:
			selectedPlayers.Add (PlayerColors.Purple);
			goto case 1;
		case 1:
			selectedPlayers.Add (PlayerColors.Yellow);
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

	public void StartFirstRound(){
		
		SetNumPlayers (numberOfPlayers);
		Menu.CloseMenus ();
		BackGroundMusicManager.instance.SetBGM (BackGroundMusicManager.BGMType.game);
		StartCoroutine (StartFirstRoundSequence ());
		//animate submarine to the top,
		//spawn divers = number of players while animating them.



		//slowly pan down while treasures get spawned with bubble effect.
		//pan back up after all treasures have spawned.  
		//begin turn 1, round 1.
	}


	#region sequences - these are functions that may or may not be sensitive to "transitiions" or animations (so we can control the timing flow of the game)
	IEnumerator StartFirstRoundSequence(){		
		yield return new WaitForSeconds (transitionDelay);
		yield return StartCoroutine (subScript.subAnim.ComingToStop ());
		yield return StartCoroutine (StartRoundSequence ());
	}

	IEnumerator EndRoundSequence(){		
		Debug.LogError ("Ending Round");

		//animate stuff here
		//close all treasures.
		//close sub.
		//destroy player placeholders that are in sub
		//animate player not in subs
		subScript.ToggleDisplayAir(false);
		yield return new WaitForSeconds (transitionDelay);
		yield return StartCoroutine(subScript.DestroyAllDivers());
		yield return StartCoroutine (TreasurePlaceholderManager.instance.DestroyAllTreasurePlaceHolder ());

		yield return StartCoroutine (StartRoundSequence ());
	}

	IEnumerator StartRoundSequence(){

		stateMachine.startNextRound ();
		Debug.LogError ("Current Gamestate :" + stateMachine.currentGameState);
		yield return new WaitForSeconds (transitionDelay);
		if (stateMachine.currentGameState == GameStates.GameHasWinner || stateMachine.currentGameState == GameStates.GameIsDraw) {
			StartCoroutine (EndGameSequence ());
		} else {
			//unboards divers 
			//start turn sequence
			yield return StartCoroutine (subScript.UnloadDivers (stateMachine.numberOfPlayers));
			yield return new WaitForSeconds (0.1f);
			TreasurePlaceholderManager.instance.TreasurePlaceholderPlacement ();
			subScript.ToggleDisplayAir (true);
			StartCoroutine (StartTurnSequence ());
		}
	}

	IEnumerator EndGameSequence(){
		yield return new WaitForSeconds (transitionDelay);
		Debug.LogError ("GAME OVER!");
		Menu.OpenEndGameMenu ();
	}

	IEnumerator StartTurnSequence(){
		Debug.LogError ("Beginning new turn");
		yield return new WaitForSeconds (transitionDelay);

		if (stateMachine.currentRoundState == RoundStates.RoundEnded) {
			yield return StartCoroutine (EndRoundSequence ());
		} 
		else {
			
			//if we can still go, continue, if not start new round sequence;
			Menu.ToggleScrollControl(true);
			stateMachine.startNextTurn ();
			//determines whether or not to open menu
			if (stateMachine.isCurrentPlayerDiving () && stateMachine.currentPlayer.collectedTreasures.Count > 0) { // if player is DIVING and POSSES treasures,
				Menu.OpenGoUpOrDown (); //player input calls Button_Direction
			} else { //if player is NOT diving do this
				yield return StartCoroutine (DirectionNotificationSequence ());
			}
		}
		yield return null;
		//show its a player's turn,
		//pause,
		//roll dice sequence
	}

	//DISPLAYS WHICH DIRECTION THE PLAYER IS GOING
	IEnumerator DirectionNotificationSequence(){  
		yield return new WaitForSeconds (transitionDelay);
		if (stateMachine.isCurrentPlayerDiving()) {
			//display notification that player is decending
			Debug.LogError ("I'm GOING DOWN");
		} else {
			//display notification that player is ascending			
			Debug.LogError ("I'm GOING UP");
		}
		StartCoroutine(RollDiceSequence ());
		yield return null;

	}

	//ROLLS THE DICE, EITHER BY CODE OR BY PLAYER INTERACTION
	IEnumerator RollDiceSequence(){
		yield return new WaitForSeconds (transitionDelay);
		stateMachine.rollForCurrentPlayer();
		stateMachine.setCurrentPlayerRoll ();
		//show roll dice menu
		//show dice value
		//show Move direction menu,
		StartCoroutine(MoveSequence ());
		yield return null;
	}

	//OBTAINS TREASURE WITH ANIMATION
	IEnumerator GetTreasureSequence(bool t){
		Debug.LogError("Player took treasure? " + t);
		stateMachine.selectTreasure (t);
		if (t) {
			TreasurePlaceholderManager.instance.RemoveTreasureFromLocation ();
		}
		yield return new WaitForSeconds (transitionDelay);
		StartCoroutine (EndTurnSequence ());
		yield return null;
	}

	//MOVE PLAYER TO DESTINATION WITH ANIMATION  ** CURRENTLY DOES NOT ACCOUNT FOR RETURNING TO SHIP**
	IEnumerator MoveSequence(){
		yield return new WaitForSeconds (transitionDelay);
		Menu.ToggleScrollControl (false);
		stateMachine.commitMovement();
		Debug.LogError ("Moving character  " + stateMachine.currentPlayerIndex + " to Treasure location " + stateMachine.currentPlayer.currentPosition);
		subScript.MoveDiverToSpot (stateMachine.currentPlayerIndex, stateMachine.currentPlayer.currentPosition);
		//animates player to move 1 space at a time going down
		//when player stops, open TreasurePrompt menu


		StartCoroutine(FinishedMovingSequence());
		yield return null;
	}

	//WHEN PLAYER REACHES TREASURE SPOT, DO THIS 
	IEnumerator FinishedMovingSequence(){
		yield return new WaitForSeconds (transitionDelay);
		if (stateMachine.currentTurnState == TurnStates.TreasureAvailable) {// open yes or no menu if there is treasure		
			
			if (stateMachine.currentPlayer.currentPosition == stateMachine.treasureLocations.Count - 1) {
				StartCoroutine (GetTreasureSequence (true));
			}
			else
				Menu.OpenYesNoTreasure (stateMachine.treasureLocations [stateMachine.currentPlayer.currentPosition].treasure); //player input calls Button_GetTreasure


		} 
		else {
			if (stateMachine.currentPlayer.collectedTreasures.Count > 0) {// //if there is no treasure and player have treasure
				Debug.LogError("Do you want to drop your treasure?");
				Menu.OpenDropTreasure ();//player input calls Button_DropTreasure
			} else {
				StartCoroutine(EndTurnSequence ());
			}
			//theres no treasure
		}
		Menu.ToggleScrollControl (true);
		yield return null;
	}
		
	IEnumerator ReturnTreasureSequence(int myTreasureIndex, TreasureType type){
		yield return new WaitForSeconds (transitionDelay);
		Debug.LogError ("Returning Treasure");
		stateMachine.returnTreasure (myTreasureIndex);
		Debug.LogError ("Returned Treasure..");
		TreasurePlaceholderManager.instance.PutTreasureBack (myTreasureIndex, type);
		StartCoroutine (EndTurnSequence ());
		yield return null;
		//animates treasure going from player to "empty" treasure location
		
	}

	IEnumerator DontDropTreasureSequence(){
		yield return new WaitForSeconds (transitionDelay);
		
		StartCoroutine (EndTurnSequence ());
		
		yield return null;
	}

	IEnumerator EndTurnSequence(){
		Debug.LogError (stateMachine.currentTurnState);
		Debug.LogError ("End turn");
		Menu.ToggleScrollControl (false);
		stateMachine.endTurn ();
		yield return new WaitForSeconds (transitionDelay);
		StartCoroutine (StartTurnSequence ());
		//animates ending turn
		//>  Start Turn Sequence
	}

	IEnumerator RestartGameSequence(){
		Debug.LogError ("Starting new game");
		yield return new WaitForSeconds (transitionDelay);
		StartFirstRound ();
	}

	#endregion





	#region buttons - these are used for UI BUTTONS
	//USER CHOOSE TO GO  UP OR DOWN
	public void Button_Direction(bool t){
		if (t) {
			stateMachine.directCurrentPlayerToShip ();
		}
		StartCoroutine(DirectionNotificationSequence ());
	}

	//USERS CHOOSE TO GET TREASURE OR NOT

	public void Button_GetTreasure(bool t){
		StartCoroutine(GetTreasureSequence (t));
	}

	public void Button_DropTreasure(int myTreasureIndex, TreasureType type){
		Debug.LogError ("Drop Treasure!");
		StartCoroutine (ReturnTreasureSequence (myTreasureIndex, type));
	}

	public void Button_DontDropTreasure(){
		StartCoroutine (DontDropTreasureSequence ());
	}

	public void Button_RestartGame(){
		StartCoroutine (RestartGameSequence ());
	}

	public void Button_QuitGame(){
		Application.Quit ();
	}

	public void Button_StartGame(){
		OpeningSequence ();
	}
	#endregion




}
