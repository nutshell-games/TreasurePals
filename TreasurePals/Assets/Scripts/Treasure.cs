using UnityEngine;
using System.Collections;

namespace Tabletop{
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

}