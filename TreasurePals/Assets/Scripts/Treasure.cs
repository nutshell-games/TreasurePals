using UnityEngine;
using System.Collections;

namespace Tabletop{
	//[AddComponentMenu("TreasurePals/Treasure")]
	//public class Treasure : MonoBehaviour  {
	public class Treasure {
		public TreasureType type;
		public int value;
		public TreasureStates state = TreasureStates.Neutral;
		public Player owner = null;

		public Treasure (TreasureType type, int value) {
			this.type = type;
			this.value = value;
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

	//public class TreasureCombo : Treasure 
	//{

	//	//public TreasureCombo() : base()
	//	//{
	//	//}

	//	public static TreasureCombo FactoryMethod()
	//	{
	//		//whatever you want to do with your string before passing it in
	//		return new Treasure(TreasureType.F, 0);
	//	}

	//	//private static TreasureComboConstructor()
	//	//{
	//	//	Treasure(TreasureType.F, 0);
	//	//}
	//}



	//public MyExceptionClass(string message, string extraInfo) : 
 //        base(ModifyMessage(message, extraInfo))
 //    {
	//}

	//private static string ModifyMessage(string message, string extraInfo)
	//{
	//	Trace.WriteLine("message was " + message);
	//	return message.ToLowerInvariant() + Environment.NewLine + extraInfo;
	//}
}
}