using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TreasureMenu : MonoBehaviour {
	[SerializeField]
	private RectTransform ContentTransform;
	[SerializeField]
	private Animator myAnim;

	public float BottomThreshold = 0.0f;

	[SerializeField]
	private string ShowMenuTrigger = "";

	[SerializeField]
	private string HideMenuTrigger = "";

	[SerializeField]
	private bool MenuOpen = false;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		UpdateMenu ();
	}

	void UpdateMenu(){
		Debug.LogError (ContentTransform.offsetMax);
		if (ContentTransform.offsetMax.y >= BottomThreshold) {
			Debug.LogError ("Show menu");
			HideMenu ();
		} else {
			ShowMenu ();
			Debug.LogError ("menu hidden");
		}
	}

	void ShowMenu(){
		if (!MenuOpen) {
			myAnim.SetTrigger (ShowMenuTrigger);
			MenuOpen = !MenuOpen;
		}
	}

	void HideMenu(){
		if (MenuOpen) {
			myAnim.SetTrigger (HideMenuTrigger);
			MenuOpen = !MenuOpen;
		}
	}
}
