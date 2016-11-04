﻿using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {
	[SerializeField]
	private Animator numPlayerAnim;
	[SerializeField]
	private Animator playerOrderAnim;
	[SerializeField]
	private Animator settingAnim;
	[SerializeField]
	private Animator treasureAnim;

	private Animator currentAnim;


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
}
