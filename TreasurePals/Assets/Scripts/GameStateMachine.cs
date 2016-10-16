using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


namespace Tabletop 
{
	
	public class ScoreReport
	{

		public PlayerColors color;
		public Dictionary<TreasureType, int> treasureTypeTotals;
		public int totalScore = 0;
		public int totalTreasures = 0;
		public int finalTreasureTypeValue = 0;

		public ScoreReport(PlayerColors color)
		{
			this.color = color;
		}

		public void getTreasureTypeTotals(List<Treasure> capturedTreasures)
		{
			totalTreasures = capturedTreasures.Count;

			treasureTypeTotals[TreasureType.A] = 0;
			treasureTypeTotals[TreasureType.B] = 0;
			treasureTypeTotals[TreasureType.C] = 0;
			treasureTypeTotals[TreasureType.D] = 0;
			treasureTypeTotals[TreasureType.F] = 0;

			foreach (Treasure treasure in capturedTreasures)
			{
				if (treasureTypeTotals.ContainsKey(treasure.type))
				{
					treasureTypeTotals[treasure.type] += 1;
				}
				else {
					treasureTypeTotals[treasure.type] = 1;
				}

				totalScore += treasure.value;
			}

			finalTreasureTypeValue = 0;

			finalTreasureTypeValue += treasureTypeTotals[TreasureType.A] * 1;
			finalTreasureTypeValue += treasureTypeTotals[TreasureType.B] * 10;
			finalTreasureTypeValue += treasureTypeTotals[TreasureType.C] * 100;
			finalTreasureTypeValue += treasureTypeTotals[TreasureType.D] * 1000;
			finalTreasureTypeValue += treasureTypeTotals[TreasureType.F] * 10000;
		}
	}

	public class StateMachine {


		static int maxRounds = 3;
		static int maxAir = 25;
		static int maxRoll = 6;
		static int minRoll = 2;

		// can be received from the server
		// fixed order of treasure values
		int[][] startingTreasures;

		public List<Player> players;
		public Dictionary<PlayerColors,Player> playersByColor;

		public GameStates currentGameState;

		public int firstPlayer = 0;

		public PlayerColors[] playerOrder;

		public List<Treasure> treasureQueue = new List<Treasure> ();
		public Stack<Treasure> treasureCollected = new Stack<Treasure> ();
		public List<Treasure> treasureCaptured = new List<Treasure> ();

		//public Dictionary<PlayerColors,List<Treasure>> treasureCollectedReport;
		public Dictionary<PlayerColors,List<Treasure>> treasureCapturedReport;
		public List<ScoreReport> treasureScoredReport;

		public List<TreasureLocation> treasureLocations = new List<TreasureLocation> ();

		public RoundStates currentRoundState = RoundStates.RoundEnded;
		public int currentRound = 0;
		public int currentAir = 0;

		public int numberOfPlayers = 0;

		public TurnStates currentTurnState = TurnStates.TurnEnded;
		public Player currentPlayer;
		public int currentPlayerIndex = 0;
		public int currentPlayerRoll = 0;
		public int currentPlayerMovement = 0;

		public int lastRoll = 0;

		public Player winner = null;


		public StateMachine() {

			players = new List<Player> ();

			treasureCapturedReport = new Dictionary<PlayerColors, List<Treasure>> ();
			treasureScoredReport = new List<ScoreReport>();
		}

		// GAME SETUP
		//===========

		public void setupGameForPlayers ( List<PlayerColors> selectedPlayers  ) {

			Debug.Log ("setupGameForPlayers");

			foreach (PlayerColors playerColor in selectedPlayers) {
				
				treasureCapturedReport.Add (playerColor, new List<Treasure> ());
				playersByColor.Add(playerColor, spawnPlayer(playerColor));
			}
				
			generateStartingTreasures ();
			setupTreasures ();
		}


		private void generateStartingTreasures () {

			// TODO randomly generate values
			// TODO receive starting treasures from server
			startingTreasures = new int[4][];

			startingTreasures [0] = new int[] { 0, 0, 3, 4, 5, 3, 2 };
			startingTreasures [1] = new int[] { 6, 7, 8, 9, 10, 9, 8, 7 };
			startingTreasures [2] = new int[] { 11, 12, 13, 14, 15, 14, 13, 12 };
			startingTreasures [3] = new int[] { 16, 17, 18, 19, 20, 19, 18, 17 };
		}

		public void setPlayerOrder (PlayerColors[] _playerOrder) {

			this.playerOrder = _playerOrder;
		}

		private Player spawnPlayer (PlayerColors playerColor) {

			Debug.Log ( String.Format("spawn player {0}",playerColor) );

			Player newPlayer = new Player (playerColor);
			players.Add (newPlayer);

			return newPlayer;
		}


		private void setupTreasures () {

			Debug.Log ("setup treasures");

			treasureQueue = new List<Treasure> ();

			for (var t = 0; t < startingTreasures.GetLength(0); t++) {

				int[] treasuresOfType = startingTreasures [t];

				for (var i = 0; i < treasuresOfType.Length; i++) {

					Treasure newTreasure;
					TreasureType treasureType = 0;

					switch (t) {

					case 0:
						treasureType = TreasureType.A;
						break;

					case 1:
						treasureType = TreasureType.B;
						break;

					case 2:
						treasureType = TreasureType.C;
						break;

					case 3:
						treasureType = TreasureType.D;
						break;

					}

					newTreasure = new Treasure(treasureType,treasuresOfType[i]);
					treasureQueue.Add (newTreasure);
				}

			}

			for (var t=0; t < treasureQueue.Count; t++) 
			{
				treasureLocations.Add(new TreasureLocation());
				treasureLocations[t].treasure = treasureQueue[t];
				Debug.Log(String.Format("location: {0}, treasure: {1}", treasureLocations[t].treasure.type, treasureQueue[t].type));
			}

			if (treasureQueue.Count != treasureLocations.Count) 
			{
				throw new System.Exception ("treasure queue does not match treasure locations.");
			}

			Debug.Log( String.Format("Treasure queue: {0}, locations queue: {1}", treasureQueue.Count, treasureLocations.Count) );
		}




// GAME CLEANUP
//=============

		public void proceedToScoring() {

			currentGameState = GameStates.ScoringInProgress;

			// create list of each Player's collected treasures
			generateTreasureReport();

			foreach (KeyValuePair<PlayerColors,List<Treasure>> kvp in treasureCapturedReport) {

				ScoreReport report = new ScoreReport(kvp.Key);
				report.getTreasureTypeTotals(kvp.Value);

				treasureScoredReport.Add (report);
			}
		}

		void determineWinner()
		{
			currentGameState = GameStates.ScoringComplete;

			IEnumerable<ScoreReport> query = treasureScoredReport.OrderByDescending(
			(ScoreReport report) => report.totalScore);

			int highScore = query.ElementAt(0).totalScore;
			List<ScoreReport> highScorers = new List<ScoreReport>();


			foreach (ScoreReport report in query)
			{
				if (report.totalScore == highScore)
				{
					highScorers.Add(report);
				}
			}

			if (highScorers.Count == 1)
			{
				currentGameState = GameStates.GameHasWinner;
				winner = playersByColor[highScorers[0].color];

			}
			else {
				breakTie(highScorers);
			}
		}


		void breakTie(List<ScoreReport> highScorers)
		{

			// create final treasure value by type
			List<int> finalTreasureTypeValues = new List<int>();


			IEnumerable<ScoreReport> query = highScorers.OrderByDescending(
				report => report.finalTreasureTypeValue );
			
			int highScore = query.ElementAt(0).finalTreasureTypeValue;

			foreach (ScoreReport report in query)
			{
				if (report.totalScore == highScore)
				{
					highScorers.Add(report);
				}
			}

			if (highScorers.Count == 1)
			{
				currentGameState = GameStates.GameHasWinner;
				winner = playersByColor[highScorers[0].color];

			}
			else {
				currentGameState = GameStates.GameIsDraw;
			}
		}


		public Dictionary<PlayerColors, List<Treasure>> generateTreasureReport()
		{

			Debug.Log("generateTreasureReport");

			treasureCapturedReport = new Dictionary<PlayerColors, List<Treasure>>();


			foreach (Treasure treasure in treasureCaptured)
			{

				PlayerColors color = treasure.owner.color;

				treasureCapturedReport[color].Add(treasure);
			}

			foreach (KeyValuePair<PlayerColors, List<Treasure>> kvp in treasureCapturedReport)
			{
				Debug.Log("Player " + kvp.Key + " treasures: " + kvp.Value.Count);
			}

			return treasureCapturedReport;
		}


		Treasure createComboTreasure(List<Treasure> treasures)
		{

			int comboValue = 0;
			foreach (Treasure treasure in treasures)
			{
				comboValue += treasure.value;
			}

			return new Treasure(TreasureType.F, comboValue, treasures.Count);
		}

		List<Treasure> takeTreasuresToCombo(Stack<Treasure> collectedTreasures)
		{
			if (collectedTreasures.Count == 0)
			{
				return null;
			}
			else {

				int treasuresInGroup = 3;

				List<Treasure> treasureGroup = new List<Treasure>();

				int index = 0;
				while (index < treasuresInGroup && collectedTreasures.Count>0)
				{
					treasureGroup.Add(collectedTreasures.Pop());
					index++;
				}

				return treasureGroup;
			}
		}


		// Remove all collected treasures from queue
		public void cleanupTreasures()
		{
			Debug.Log("cleanupTreasures for round " + currentRound);

			// each player tracks his own captured treasures
			foreach (Player player in players)
			{
				foreach (Treasure treasure in player.capturedTreasures)
				{
					treasureCaptured.Add(treasure);
				}

				foreach (Treasure aCollectedTreasure in player.collectedTreasures)
				{
					treasureCollected.Push(aCollectedTreasure);
				}
			}

			Debug.Log("treasures captured:" + treasureCaptured.Count);
			Debug.Log("treasures collected:" + treasureCollected.Count);

			foreach (Treasure treasure in treasureQueue)
			{
				if (treasure.state == TreasureStates.Collected ||
				    treasure.state == TreasureStates.Captured)
				{
					treasureQueue.Remove(treasure);
				}
			}

			// redistribute collected treasures into groups of 3 treasures at bottom of sea
			List<Treasure> group;
			while ((group = takeTreasuresToCombo(this.treasureCollected))!= null)
			{
				treasureQueue.Add(createComboTreasure(group));
			}


			// reset treasure locations
			for (var t = 0; t < treasureLocations.Count; t++)
			{

				if (treasureQueue.ElementAt(t) != null)
				{
					treasureLocations[t].treasure = treasureQueue[t];
					treasureLocations[t].player = null;
					treasureLocations[t].active = true;
				}
				else {
					// deactivate empty location
					treasureLocations[t].treasure = null;
					treasureLocations[t].player = null;
					treasureLocations[t].active = false;
				}

			}

		}



//	GAME LOOP
//===========

		public void reportGameState()
		{
			foreach (Player player in players)
			{
				Debug.Log(String.Format("currentPlayerIndex: {0}, currentPlayer:{1} position:{2}",
									currentPlayerIndex, currentPlayer.color, currentPlayer.currentPosition));
			}

			Debug.Log(String.Format("currentRound:{0} currentAir:{1} roundState:{2} turnState:{3}",
									currentRound, currentAir, currentRoundState, currentTurnState));
		}

		public void startNextRound() {

			Debug.Log ("starting next round");


			if (currentRoundState != RoundStates.RoundEnded) {
				throw new System.Exception ("Cannot start next round, round not over");
			}

			if (currentRound > 0)
			{
				cleanupTreasures();

				// reset players
				foreach (Player player in players)
				{
					player.reset();
				}
			}
				

			if (currentRound < maxRounds) {
				currentRound++;
				currentRoundState = RoundStates.RoundStarted;
				currentAir = maxAir;
				Debug.Log(String.Format("Round started: {0} roundState {1} maxAir {2}",
				                        currentRound,currentRoundState,currentAir));

			} else {
				Debug.Log("all rounds over");
				endGame();
			}


		}

		void endGame()
		{
			currentGameState = GameStates.GameEnded;
			proceedToScoring();
			determineWinner();
		}

		public void startNextTurn() {

			Debug.Log("startNextTurn turnState:" + currentTurnState + " roundState: " + currentRoundState);

			if (currentRoundState == RoundStates.RoundEnded) {
				throw new System.Exception ("Cannot start turn, round ended");
			} 
			else if (currentTurnState != TurnStates.TurnEnded) {
				throw new System.Exception ("Cannot start turn, turn not over");
			}

			if (currentRoundState == RoundStates.RoundStarted) {

				currentPlayerIndex = firstPlayer;
				currentRoundState = RoundStates.RoundInProgress;

			} 
			else if (currentRoundState == RoundStates.RoundInProgress){

				// skip player if returned to ship
				do
				{
					currentPlayerIndex++;

					if (currentPlayerIndex >= players.Count)
						currentPlayerIndex = 0;
				}
				while (players[currentPlayerIndex].state == PlayerStates.ReturnedToShip);
			}

			currentPlayer = players[currentPlayerIndex];
			currentTurnState = TurnStates.TurnStarted;

			// deduct air
			currentAir -= currentPlayer.collectedTreasures.Count;

			// check if round over
			if (currentAir <= 0)
			{
				endRound();
			}

			reportGameState();
		}

		public void endTurn()
		{

			if (currentTurnState != TurnStates.TreasurePassed &&
				currentTurnState != TurnStates.TreasureCollected &&
				currentTurnState != TurnStates.TreasureUnavailable)
			{
				throw new System.Exception("Player has not resolved treasure collection.");
			}

			currentTurnState = TurnStates.TurnEnded;

			Debug.Log("Turn over " + currentPlayer.color);
		}

		public void endRound()
		{
			if (currentRoundState != RoundStates.RoundInProgress)
			{
				throw new System.Exception("round not in progress");
			}

			currentRoundState = RoundStates.RoundEnded;

			foreach (Player player in players)
			{
				if (player.state != PlayerStates.ReturnedToShip)
				{
					player.state = PlayerStates.LostAtSea;
				}
			}

			Debug.Log("round ended " + currentRound);
		}

// ROLLING
//========

		public void rollForCurrentPlayer() {

			Debug.Log("rollForCurrentPlayer "+currentTurnState);

			if (currentTurnState != TurnStates.TurnStarted) {
				throw new System.Exception ("Rolling only allowed at start of turn");
			}

			lastRoll = getRandomRollValue();
			currentTurnState = TurnStates.PlayerRolling;

			Debug.Log(String.Format("currentPlayerRoll: {0} roundState: {1} turnState:{2}",
			                        lastRoll, currentRoundState, currentTurnState));
		}


		T getDiceRoll<T>(List<T> diceValues)
		{
			var rand = new System.Random();
			var index = rand.Next(0, diceValues.Count);
			var item = diceValues[index];

			return item;
		}

		public int getRandomRollValue()
		{
			List<int> diceValues = new List<int>{ 1, 2, 3, 1, 2, 3 };

			int roll1 = getDiceRoll<int>(diceValues);
			int roll2 = getDiceRoll<int>(diceValues);

			return roll1 + roll2;
		}

		public void setCurrentPlayerRoll (int rollValue) {

			if (currentTurnState != TurnStates.PlayerRolling) {
				throw new System.Exception ("Player roll is not pending.");
			}

			if (rollValue < minRoll || rollValue > maxRoll) {
				throw new System.Exception ("Invalid Roll value");
			}

			currentPlayerRoll = rollValue;
			currentPlayerMovement =  Math.Max(currentPlayerRoll - currentPlayer.collectedTreasures.Count, 0);
			currentTurnState = TurnStates.PlayerRolled;

			Debug.Log(String.Format("after Roll, currentPlayerRoll: {0} playerMovement: {1} turnState:{2}",
			                        lastRoll, currentPlayerMovement, currentTurnState));
		}

// MOVEMENT
//========

		public bool isCurrentPlayerDiving()
		{

			return currentPlayer.state == PlayerStates.Diving;
		}

		public bool directCurrentPlayerToShip()
		{
			if (currentPlayer.collectedTreasures.Count == 0)
			{
				throw new System.Exception("cannot return to ship without any treasure.");
			}

			if (currentPlayer.state == PlayerStates.Diving)
			{
				currentPlayer.returnToShip();

				return true;
			}
			else {
				return false;
			}
		}

		public void commitMovement() {

			if (currentTurnState != TurnStates.PlayerRolled) {
				throw new System.Exception ("Player roll has not rolled for movement.");
			}

			currentTurnState = TurnStates.PlayerMoving;

			// if player can move at all
			if (currentPlayerMovement > 0) 
			{
				applyMovementForCurrentPlayer (currentPlayerMovement);
				currentTurnState = TurnStates.PlayerMoved;

				// check if all players returned to ship
				int playersInShip = 0;
				foreach (Player player in players) 
				{
					if (player.state == PlayerStates.ReturnedToShip) playersInShip++;
				}

				if (playersInShip == players.Count)
				{
					Debug.Log("all players returned to ship");
					endRound();
					return;
				}

				if (getTreasureAtCurrentPlayerLocation ()) {
					currentTurnState = TurnStates.TreasureAvailable;
				} else {
					currentTurnState = TurnStates.TreasureUnavailable;
				}

			} else {
				currentTurnState = TurnStates.PlayerMoved;
			}

			Debug.Log(String.Format("turnState after movement: {0}", currentTurnState));
		}

		private int[] applyMovementForCurrentPlayer (int distanceToMove) {

			Debug.Log("applyMovementForCurrentPlayer "+ currentPlayer.color +
			          " currentPosition: "+currentPlayer.currentPosition+
			          " distance:" + distanceToMove + "state:" + currentPlayer.state);

			if (currentPlayer.state == PlayerStates.LeavingShip)
			{
				currentPlayer.state = PlayerStates.Diving;
			}

			// return array of movements
			int[] movementHistory = new int[distanceToMove];
			int historyIndex = 0;

			movementHistory [historyIndex] = currentPlayer.currentPosition;

			while (distanceToMove > 0) {

				Debug.Log("distance to move: " + distanceToMove);
				// find next empty location
				bool locatedNextEmptyLocation = false;

				while (locatedNextEmptyLocation == false) {

					if (currentPlayer.state == PlayerStates.Diving)
					{
						currentPlayer.currentPosition+=1;
					}
					else if (currentPlayer.state == PlayerStates.Ascending)
					{
						currentPlayer.currentPosition-=1;
					}

					Debug.Log("new position: " + currentPlayer.currentPosition);

					// player reached end of treasure locations
					if (currentPlayer.currentPosition >= treasureLocations.Count)
					{
						distanceToMove = 0;
						currentPlayer.placeInShip();
						break;
					}

					else if (currentPlayer.currentPosition <= 0)
					{
						distanceToMove = 0;
						currentPlayer.returnToShip();
						break;
					}

					Debug.Log("currentPlayer position: " + currentPlayer.currentPosition);


					// skip over location occupied by player
					if (treasureLocations [currentPlayer.currentPosition].player==null) {

						// found empty location
						movementHistory [historyIndex] = currentPlayer.currentPosition;

						historyIndex++;
						locatedNextEmptyLocation = true;

						Debug.Log("found next empty location: " + currentPlayer.currentPosition);

						// counted 1 movement
						distanceToMove--;
					} 

				}

			}

			TreasureLocation locationAfterMovement = treasureLocations[currentPlayer.currentPosition];
			locationAfterMovement.player = currentPlayer;

			return movementHistory;
		}


// TREASURE SELECTION
//===================

		public bool getTreasureAtCurrentPlayerLocation () {

			TreasureLocation currentLocation = treasureLocations [currentPlayer.currentPosition];

			Debug.Log(String.Format("treasure at location {0}",
									currentPlayer.currentPosition));

			//Debug.Log(String.Format("treasure at location {0}: {1}, {2}",
			//                        currentPlayer.currentPosition,
			//                        currentLocation.treasure.type,
			//                        currentLocation.treasure.value));

			if (currentLocation.treasure == null) {
				return false;
			} else {
				return true;
			}
		}


		public void selectTreasure (bool willCollectTreasure) {

			if (getTreasureAtCurrentPlayerLocation () == false) {
				throw new System.Exception ("No treasure to collect at this location");
			}

			if (willCollectTreasure)
			{
				Debug.Log("collecting treasure");
				TreasureLocation currentLocation = treasureLocations[currentPlayer.currentPosition];

				// player collects treasure
				Treasure currentTreasure = currentLocation.treasure;
				currentTreasure.collect(currentPlayer);
				currentPlayer.collectedTreasures.Add(currentTreasure);

				treasureCollected.Push(currentTreasure);

				// clear treasure at location
				currentLocation.treasure = null;

				currentTurnState = TurnStates.TreasureCollected;
			}
			else 
			{
				Debug.Log("declining treasure");
				currentTurnState = TurnStates.TreasurePassed;
			}
		}


		public void returnTreasure(int collectedTreasureIndex)
		{
			if (currentPlayer.collectedTreasures.Count == 0)
			{
				throw new System.Exception("current player has no treasures to return");
			}
			else if (getTreasureAtCurrentPlayerLocation())
			{
				throw new System.Exception("current location has treasure.");
			}
			else if (currentPlayer.collectedTreasures.ElementAt(collectedTreasureIndex)
					 == null)
			{
				throw new System.Exception("no collected treasure at this index");
			}

			Treasure treasureToReturn = currentPlayer.collectedTreasures.ElementAt(collectedTreasureIndex);
			treasureToReturn.release();

			currentPlayer.collectedTreasures.Remove(treasureToReturn);
			treasureLocations[currentPlayer.currentPosition].treasure = treasureToReturn;
		}


	}


}

public class GameStateMachine : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
