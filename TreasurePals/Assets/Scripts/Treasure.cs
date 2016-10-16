using UnityEngine;
using System.Collections;

namespace Tabletop{
	
	public class Treasure {

		public TreasureType type;
		public int value;
		public TreasureStates state = TreasureStates.Neutral;
		public Player owner = null;
		public int comboCount = 0;

		public Treasure (TreasureType type, int value) {
			this.type = type;
			this.value = value;
		}

		public Treasure(TreasureType type, int value, int comboCount)
		{
			this.type = type;
			this.value = value;
			this.comboCount = comboCount;
		}

		public void collect (Player collectingPlayer) {

			if (state != TreasureStates.Neutral) {
				throw new System.Exception ("Treasure already collected.");
			}

			this.state = TreasureStates.Collected;
			this.owner = collectingPlayer;
		}

		public void capture()
		{
			this.state = TreasureStates.Captured;
		}

		public void release()
		{
			this.state = TreasureStates.Neutral;
			this.owner = null;
		}
	}
}