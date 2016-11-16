using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OxygenMenu : MonoBehaviour {
	[SerializeField]
	private RectTransform ContentTransform;
	[SerializeField]
	private Animator myAnim;

	public float TopThreshold = 0.0f;

	[SerializeField]
	private string ShowMenuTrigger = "";

	[SerializeField]
	private string HideMenuTrigger = "";

	[SerializeField]
	private bool MenuOpen = false;
	// Use this for initialization

	public bool EnableAnimate;

	// Update is called once per frame
	void Update () {
		if (EnableAnimate) {
			UpdateMenu ();
		}
	}

	void UpdateMenu(){
		if (ContentTransform.offsetMax.y >= TopThreshold) {
			ShowMenu ();
		} else {
			HideMenu ();
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
