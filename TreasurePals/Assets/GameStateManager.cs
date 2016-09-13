using UnityEngine;
using System.Collections;
using System.Collections.Generic;

enum PlayerColors {
	Red, Green, Blue, Yellow, Purple, Orange
}

enum TreasureType {
	A, B, C, D, E
}

enum GameStates {
	GameStarted,GameInProgress,GameEnded,
	TreasureScoring,TreasureTotaled,VictorDecided
} 

enum RoundStates {
	RoundStarted,RoundInProgress,RoundEnded,
	AirDeducted,AirEmpty,
	TreasureScoring
}

enum TurnStates {
	TurnStarted,TurnEnded,
	PlayerRolling,PlayerRolled,PlayerMoving,PlayerMoved,
	TreasureAvailable,TreasureUnavailable,
	TreasureCollected,TreasurePassed
}

namespace Tabletop {

	public class Treasure {
		int type;
		int value;
	}

	public class Player {

		int currentPosition = 0;
		int color;

		List<Treasure> pendingTreasures;
		List<Treasure> capturedTreasures;
	}
		
	public class TreasureLocation {


	}

	public class GameStateMachine {
	
		public List<TreasureLocation> treasureLocations;

		int[,] startingTreasures = new int[,]{
			{0,0,3,4,5,3,2},
			{6,7,8,9,10,9,8,7},
			{11,12,13,14,15,14,13,12},
			{16,17,18,19,20,19,18,17}
		};

//		Dictionary<TreasureType, int> startingTreasures = {
//			{ TreasureType.A, 7 },
//			{ TreasureType.B, 8 },
//			{ TreasureType.C, 9 },
//			{ TreasureType.D, 8 }
//		};

		public List<Player> players;

		public int currentGameState;

		public int firstPlayer = 0;

		public int[] playerOrder;

		public int currentRound = 0;
		public int currentRoundState = RoundStates.RoundEnded;
		public int currentAir = 0;

		static int maxRounds = 3;
		static int maxAir = 25;
		static int maxRoll = 6;
		static int minRoll = 2;

		public int numberOfPlayers = 0;

		public int currentPlayer = 0;
		public int currentPlayerRoll = 0;
		public int currentTurnState = TurnStates.TurnEnded;

		public void startNextRound() {
		
			if (currentRound < maxRounds && currentRoundState==RoundStates.RoundEnded) {
				currentRound++;
				currentRoundState = RoundStates.RoundStarted;
				currentAir = maxAir;
			}
		
		}

		public void setPlayerOrder (int[] playerColors) {

			playerOrder = playerColors;
		}

		private void setupGameForPlayers () {

			foreach (int playerColor in playerOrder) {
				spawnPlayer (playerColor);
			}
//			int color = PlayerColors [0];

			generateTreasures ();

		}

		private void spawnPlayer (int playerColor) {

			Player newPlayer = new Player ();
			players.Add (newPlayer);
		}


		private void generateTreasures () {



		}


		public void startNextTurn() {

			if (currentRoundState == RoundStates.RoundEnded) {
				throw new System.Exception ("Cannot start turn, round ended");
			}

			if (currentTurnState != TurnStates.TurnEnded) {
				throw new System.Exception ("Cannot start turn, turn not over");
			}

			if (currentRoundState == RoundStates.RoundStarted) {

				currentPlayer = firstPlayer;
				currentRoundState = RoundStates.RoundInProgress;
			
			} else if (currentRoundState == RoundStates.RoundInProgress){

				currentPlayer++;
				if (currentPlayer >= players.Count)
					currentPlayer = 0;
			}

			currentTurnState = TurnStates.TurnStarted;


		}

		public void rollForCurrentPlayer() {
			if (currentTurnState != TurnStates.TurnStarted) {
				throw new System.Exception ("Rolling allowed at start of turn");
			}

			currentTurnState = TurnStates.PlayerRolling;
		}

		public void setCurrentPlayerRoll (int rollValue) {

			if (currentTurnState != TurnStates.PlayerRolling) {
				throw new System.Exception ("Player roll is not pending.");
			}

			if (rollValue < minRoll || rollValue > maxRoll) {
				throw new System.Exception ("Invalid Roll value");
			}

			currentPlayerRoll = rollValue;
			currentTurnState = TurnStates.PlayerRolled;

		}

		public void commitMovement(bool willDive) {

			if (currentTurnState != TurnStates.PlayerRolling) {
				throw new System.Exception ("Player roll is not pending.");
			}

			currentTurnState = TurnStates.PlayerMoving;

			applyMovementForCurrentPlayer ();
		}

		public void selectTreasure (bool willCollectTreasure) {
		
		}

		private void applyMovementForCurrentPlayer() {



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
