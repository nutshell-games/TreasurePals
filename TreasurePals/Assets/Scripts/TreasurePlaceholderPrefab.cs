using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Tabletop;
public class TreasurePlaceholderPrefab : MonoBehaviour {
	
	[SerializeField]
	private Sprite Tier1Sprite;
	[SerializeField]
	private Sprite Tier2Sprite;
	[SerializeField]
	private Sprite Tier3Sprite;
	[SerializeField]
	private Sprite Tier4Sprite;
	[SerializeField]
	private Sprite Tier5Sprite;
	[SerializeField]
	private Sprite Tier6Sprite;

	public Image myImage;


	public void SetTreasurePrefabTo(TreasureType type){
		switch (type) {
		case TreasureType.A:
			myImage.sprite = Tier1Sprite;
			break;
		case TreasureType.B:
			myImage.sprite = Tier2Sprite;
			
			break;
		case TreasureType.C:
			myImage.sprite = Tier3Sprite;
			
			break;
		case TreasureType.D:
			myImage.sprite = Tier4Sprite;
			
			break;
		case TreasureType.E:
			myImage.sprite = Tier5Sprite;
			
			break;
		case TreasureType.F:
			myImage.sprite = Tier6Sprite;

			break;
		default:
			break;
		}	
			
	}

}
