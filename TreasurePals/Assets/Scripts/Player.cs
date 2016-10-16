using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Tabletop
{
	public class Player
	{

		public int currentPosition = 0;
		public PlayerColors color;
		//public bool isDiving = true;
		public PlayerStates state = PlayerStates.LeavingShip;

		public List<Treasure> collectedTreasures;
		public List<Treasure> capturedTreasures;

		public Player(PlayerColors color)
		{
			this.color = color;
			collectedTreasures = new List<Treasure>();
		}

		// the player returned safely to the ship!
		public void placeInShip()
		{
			Debug.Log("PlaceInShip: " + color);
			currentPosition = -1;
			state = PlayerStates.ReturnedToShip;

			capturedTreasures = new List<Treasure>(collectedTreasures);
			collectedTreasures.Clear();
		}

		public void returnToShip()
		{

			if (state != PlayerStates.Diving)
			{
				throw new System.Exception("player is not diving.");
			}

			if (state == PlayerStates.Diving)
			{
				state = PlayerStates.Ascending;
			}
		}

		public void reset()
		{
			currentPosition = 0;
			state = PlayerStates.LeavingShip;
		}
	}
}
