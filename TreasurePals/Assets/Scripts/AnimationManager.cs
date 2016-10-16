using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class AnimationManager : MonoBehaviour {

	public Animator waterTransistion;
	public Animator title;
	public Animator submergingTransition;
	public Animator selectNumPlayers;
	public Animator selectPlayerOrder;
	public Animator mainGame;
	public Animator gameSettings;
	public Animator treasure;
	public GameObject bubbles;


	private IEnumerator WaitForAnimation(){
		title.SetTrigger ("palsIn");
		while (title.GetCurrentAnimatorStateInfo (0).fullPathHash != -375914427) {
			yield return null;
		}
		yield return new WaitForSeconds (1.0f);
		title.SetTrigger ("AwayUp");
		selectNumPlayers.SetTrigger ("EnterDown");
		BackGroundMusicManager.instance.SetBGM (BackGroundMusicManager.BGMType.setting);
	}
	void Update(){
		//Debug.LogError(title.

	}
	public void StartGame(){
		StartCoroutine(WaitForAnimation());
		//title.SetTrigger ("awayUp");
		//waterTransistion.SetTrigger ("waterAwayUp");
		//submergingTransition.SetTrigger ("enterDown");
		//bubbles.SetActive (true);

		//button goes away
		//mariners jump on ship
		//blue screen goes up
		//next panel goes up behind blue screen
		//blue screen goes away

		//show next panel
	
	}

	public void BackToTitle(){
		//title.SetTrigger ("enterUp");
		//waterTransistion.SetTrigger ("waterEnterUp");
		//submergingTransition.SetTrigger ("awayDown");
		//bubbles.SetActive (false);
	}



}
