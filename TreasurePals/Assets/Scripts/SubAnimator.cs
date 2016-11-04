using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubAnimator : MonoBehaviour {
	public GameObject diver1;
	public GameObject diver2;
	public GameObject diver3;
	public GameObject diver4;
	[SerializeField]
	private floatingSub subScript;

	[SerializeField]
	public GameObject divingBubbles;
	[SerializeField]
	private Animation skyAnim;

	[SerializeField]
	private GameObject subTopPosition;

	public void AllAboard(){		
		diver1.GetComponent<Animation> ().Play ();
		diver2.GetComponent<Animation> ().Play ();
		diver3.GetComponent<Animation> ().Play ();
		diver4.GetComponent<Animation> ().Play ();
		StartCoroutine (boardingSequence ());
	}



	IEnumerator boardingSequence(){
		yield return new WaitForSeconds (diver1.GetComponent<Animation> ().clip.length);
		Destroy (diver1);
		Destroy (diver2);
		Destroy (diver3);
		Destroy (diver4);
		subScript.gameObject.GetComponent<Animation> ().Play ("subBounce");//bounce sub
		subScript.SetDiverY ();//set new diver float values
		divingBubbles.SetActive (true); // start bubbles particles
		skyAnim.Play (); // move the sky away.

	}

	public void AllExit(List<GameObject> divers, List<GameObject> locations){
		StartCoroutine (exitingSequence (divers, locations));
		//play exit sequence
		//
	}

	IEnumerator exitingSequence(List<GameObject> divers, List<GameObject> locations){
		subScript.gameObject.GetComponent<Animation> ().Play ("moveUp");
		subScript.startY = subTopPosition.transform.localPosition.y;
		yield return new WaitForSeconds (subScript.gameObject.GetComponent<Animation> ().GetClip ("moveUp").length);
		divingBubbles.SetActive (false);
		for (int i = 0; i < divers.Count; i++) {
			divers [i].transform.position = subScript.transform.position;
			iTween.MoveTo (divers [i], locations[i].transform.position,1f);
		}

	}

}
