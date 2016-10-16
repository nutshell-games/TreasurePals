using UnityEngine;
using System.Collections;

namespace Tabletop{

	public class TreasureLocation {

		public Player player = null;
		public Treasure treasure = null;
		public bool active = true;
		public GameObject physicalLocation;

		public TreasureLocation () {
		}
	}
}
