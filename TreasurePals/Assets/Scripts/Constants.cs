using System;
namespace Tabletop
{
	public enum PlayerColors
	{
		Red, Green, Blue, Yellow, Purple, Orange
	}

	public enum PlayerStates
	{
		LeavingShip, Diving, Ascending, ReturnedToShip, LostAtSea
	}

	//A-D tier 1,2,3,4 
	//E is empty treasure
	//F is combo treasure
	public enum TreasureType
	{
		A, B, C, D, E, F
	}

	public enum TreasureStates
	{
		Neutral, Collected, Captured
	}

	public enum GameStates
	{
		SetupInProgress, GameInProgress, GameEnded,
		ScoringInProgress, ScoringComplete,
		GameHasWinner, GameIsDraw
	}

	public enum RoundStates
	{
		RoundStarted, RoundInProgress, RoundEnded,
		AirDeducted, AirEmpty,
		TreasureScoring
	}

	public enum TurnStates
	{
		TurnStarted, TurnEnded,
		PlayerRolling, PlayerRolled, PlayerMoving, PlayerMoved,
		TreasureAvailable, TreasureUnavailable,
		TreasureCollected, TreasurePassed
	}

}
