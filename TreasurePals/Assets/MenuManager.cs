using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {
	[SerializeField]
	private Animator numPlayerAnim;
	[SerializeField]
	private Animator playerOrderAnim;
	[SerializeField]
	private Animator settingAnim;
	[SerializeField]
	private Animator treasureAnim;
	[SerializeField]
	private ScrollRect mainRect;
	private Animator currentAnim;

	public GameObject GoUpOrDownMenu;

	public GameObject YesOrNoTreasure;

	public GameObject DropTreasure;

	public GameObject EndGameMenu;


	public void OpenNumberOfPlayer(){
		if (currentAnim != null) {//if the menu is player order
			currentAnim.SetTrigger ("AwayDown"); // set player order back down
			currentAnim = numPlayerAnim;
			currentAnim.SetTrigger ("EnterUp");
			Debug.LogError("Opening select player menu, closing order menu");
		} else {//if opening menu for first time
			Debug.LogError("Opening menu for first time");
			currentAnim = numPlayerAnim;
			currentAnim.SetTrigger ("EnterDown");
		}
	}

	public void OpenPlayerOrder(){
		currentAnim.SetTrigger ("AwayUp");
		currentAnim = playerOrderAnim;
		currentAnim.SetTrigger ("EnterDown");	
	}

	public void CloseMenus(){
		currentAnim.SetTrigger ("AwayUp");
	}

	public void OpenSettings(){
		currentAnim.SetTrigger ("back");
	
	}

	public void OpenTreasure(){
		currentAnim.SetTrigger ("back");
	
	}

	public void OpenGoUpOrDown(){
		GoUpOrDownMenu.SetActive (true);
	}

	public void OpenDropTreasure(){
		DropTreasure.GetComponent<DropTreasure>().PopulateTreasuresButtons();
		DropTreasure.SetActive (true);
	}

	public void OpenYesNoTreasure(){
		
		YesOrNoTreasure.SetActive (true);
	}

	public void OpenEndGameMenu(){
		EndGameMenu.SetActive (true);
	}

	public void ToggleScrollControl(bool t){
		mainRect.vertical = t;
	}

}
