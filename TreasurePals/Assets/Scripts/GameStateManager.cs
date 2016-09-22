using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum PlayerColors {
	Red, Green, Blue, Yellow, Purple, Orange
}

public enum TreasureType {
	A, B, C, D, E
}

public enum GameStates {
	GameStarted,GameInProgress,GameEnded,
	TreasureScoring,TreasureTotaled,VictorDecided
} 

public enum RoundStates {
	RoundStarted,RoundInProgress,RoundEnded,
	AirDeducted,AirEmpty,
	TreasureScoring
}

public enum TurnStates {
	TurnStarted,TurnEnded,
	PlayerRolling,PlayerRolled,PlayerMoving,PlayerMoved,
	TreasureAvailable,TreasureUnavailable,
	TreasureCollected,TreasurePassed
}

namespace Tabletop {

	public class Treasure {
		
		public TreasureType type;
		public int value;
		public bool isCollected = false;
		public PlayerColors collectedByPlayer;

		public Treasure (TreasureType type, int value) {
			this.type = type;
			this.value = value;
		}

		public void collect (PlayerColors collectingPlayer) {

			if (this.isCollected) {
				throw new System.Exception ("Treasure already collected.");
			}

			this.isCollected = true;
			this.collectedByPlayer = collectingPlayer;
		}
	}

	public class Player {

		public int currentPosition = -1;
		public PlayerColors color;
		public bool isDiving = true;

		public List<Treasure> collectedTreasures;
		public List<Treasure> capturedTreasures;

		public Player (PlayerColors color) {
			
			this.color = color;
		}

		public void returnToShip() {

			if (isDiving) {
				isDiving = false;
			}
		}
	}
		
	public class TreasureLocation {

		public Player player = null;
		public Treasure treasure = null;

		public TreasureLocation () {

		}
	}

	public class GameStateMachine {
	
		public List<TreasureLocation> treasureLocations;

		// can be received from the server
		// fixed order of treasure values
		int[][] startingTreasures;

		public List<Player> players;

		public int currentGameState;

		public int firstPlayer = 0;

		public PlayerColors[] playerOrder;

		public List<Treasure> treasureQueue;
		public List<Treasure> treasureCollected;
		public List<Treasure> treasureCaptured;

		public int currentRound = 0;
		public RoundStates currentRoundState = RoundStates.RoundEnded;
		public int currentAir = 0;

		static int maxRounds = 3;
		static int maxAir = 25;
		static int maxRoll = 6;
		static int minRoll = 2;

		public int numberOfPlayers = 0;

		public Player currentPlayer;
		public int currentPlayerIndex = 0;
		public int currentPlayerRoll = 0;
		public int currentPlayerMovement = 0;
		public TurnStates currentTurnState = TurnStates.TurnEnded;

		// GAME SETUP
		//===========

		private void setupGameForPlayers () {

			foreach (PlayerColors playerColor in playerOrder) {
				spawnPlayer (playerColor);
			}

			// TODO receive starting treasures
			startingTreasures = new int[4][];

			startingTreasures [0] = new int[] { 0, 0, 3, 4, 5, 3, 2 };
			startingTreasures [1] = new int[] { 6, 7, 8, 9, 10, 9, 8, 7 };
			startingTreasures [2] = new int[] { 11, 12, 13, 14, 15, 14, 13, 12 };
			startingTreasures [3] = new int[] { 16, 17, 18, 19, 20, 19, 18, 17 };

			generateTreasures ();
		}

		public void setPlayerOrder (PlayerColors[] playerColors) {

			playerOrder = playerColors;
		}

		private void spawnPlayer (PlayerColors playerColor) {

			Player newPlayer = new Player (playerColor);
			players.Add (newPlayer);
		}


		private void generateTreasures () {

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

			for (var t=0; t < treasureQueue.Count; t++) {
				treasureLocations.Add(new TreasureLocation());
			}
		}

		// Remove all collected treasures from queue
		public void cleanupTreasures() {

			foreach (Treasure treasure in treasureQueue) {
				if (treasure.isCollected) {
					treasureQueue.Remove (treasure);
				}
			}


		}

		public void proceedToScoring() {

			// create list of each Player's collected treasures

		}


		//	GAME LOOP
		//===========

		public void startNextRound() {

			if (currentRoundState != RoundStates.RoundEnded) {
				throw new System.Exception ("Cannot start next round, round not over");
			}

			cleanupTreasures ();

			if (currentRound < maxRounds) {
				currentRound++;
				currentRoundState = RoundStates.RoundStarted;
				currentAir = maxAir;

			} else {
				proceedToScoring ();
			}


		}

		public void startNextTurn() {

			if (currentRoundState == RoundStates.RoundEnded) {
				throw new System.Exception ("Cannot start turn, round ended");
			}

			if (currentTurnState != TurnStates.TurnEnded) {
				throw new System.Exception ("Cannot start turn, turn not over");
			}

			if (currentRoundState == RoundStates.RoundStarted) {

				currentPlayerIndex = firstPlayer;
				currentRoundState = RoundStates.RoundInProgress;
			
			} else if (currentRoundState == RoundStates.RoundInProgress){

				currentPlayerIndex++;
				if (currentPlayerIndex >= players.Count)
					currentPlayerIndex = 0;
			}

			currentTurnState = TurnStates.TurnStarted;


		}

		public void rollForCurrentPlayer() {
			if (currentTurnState != TurnStates.TurnStarted) {
				throw new System.Exception ("Rolling allowed at start of turn");
			}

			currentTurnState = TurnStates.PlayerRolling;
		}

		public bool getCurrentPlayerDirection () {

			return currentPlayer.isDiving;
		}

		public bool returnCurrentPlayerToShip () {
			
			if (currentPlayer.isDiving) {
				currentPlayer.returnToShip ();

				return true;
			} else {
				return false;
			}
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

		}

		public void commitMovement() {

			if (currentTurnState != TurnStates.PlayerRolling) {
				throw new System.Exception ("Player roll is not pending.");
			}

			if (currentTurnState != TurnStates.PlayerRolling) {
				throw new System.Exception ("Player roll is not pending.");
			}

			currentTurnState = TurnStates.PlayerMoving;

			if (currentPlayerMovement > 0) {
				applyMovementForCurrentPlayer (currentPlayerMovement);
				currentTurnState = TurnStates.PlayerMoved;
				
				if (getTreasureAtCurrentPlayerLocation ()) {
					currentTurnState = TurnStates.TreasureAvailable;
				} else {

				}
			} else {
				currentTurnState = TurnStates.PlayerMoved;
			}

		}

		private int[] applyMovementForCurrentPlayer (int distanceToMove) {

			// return array of movements
			int[] movementHistory = new int[distanceToMove];
			int historyIndex = 0;

			movementHistory [historyIndex] = currentPlayer.currentPosition;

			while (distanceToMove > 0) {

				// find next empty location
				bool locatedNextEmptyLocation = false;

				while (locatedNextEmptyLocation == false) {
					
					currentPlayer.currentPosition++;

					// skip over location occupied by player
					if (treasureLocations [currentPlayer.currentPosition].player==null) {

						// found empty location
						historyIndex++;
						movementHistory [historyIndex] = currentPlayer.currentPosition;
						locatedNextEmptyLocation = true;

					} 

					// TODO handle reached end of treasure locations
				}

				distanceToMove--;
			}

			return movementHistory;
		}

		public bool getTreasureAtCurrentPlayerLocation () {

			TreasureLocation currentLocation = treasureLocations [currentPlayer.currentPosition];

			if (currentLocation.treasure != null) {
				return true;
			} else {
				return false;
			}
		}


		public void selectTreasure (bool willCollectTreasure) {

			if (getTreasureAtCurrentPlayerLocation () == false) {
				throw new System.Exception ("No treasure to collect at this location");
			}

			if (willCollectTreasure) {
				TreasureLocation currentLocation = treasureLocations [currentPlayer.currentPosition];

				// player collects treasure
				Treasure currentTreasure = currentLocation.treasure;
				currentTreasure.collect (currentPlayer.color);
				currentPlayer.collectedTreasures.Add (currentTreasure);

				treasureCollected.Add (currentTreasure);

				// clear treasure at location
				currentLocation.treasure = null;
			}
		}


		
	}


}

public class GameStateManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
