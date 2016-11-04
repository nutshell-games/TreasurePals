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

	public GameObject oceanBubbles;
		
	[SerializeField]
	private Animation skyAnim;

	[SerializeField]
	private GameObject subTopPosition;


	#region title Start - all divers board submarine and dives
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
	#endregion






	#region after game setup - submarine comes to stop.
	public IEnumerator ComingToStop(){
		subScript.gameObject.GetComponent<Animation> ().Play ("moveUp");
		subScript.startY = subTopPosition.transform.localPosition.y;
		yield return new WaitForSeconds (subScript.gameObject.GetComponent<Animation> ().GetClip ("moveUp").length);		
		divingBubbles.SetActive (false);
		oceanBubbles.SetActive (true);
	}
	#endregion






	#region Starting round - deploy characters from submarine 
	public void AllExit(List<GameObject> divers, List<GameObject> locations){
		
		for (int i = 0; i < divers.Count; i++) {
			divers [i].transform.position = subScript.transform.position;
			iTween.MoveTo (divers [i], locations[i].transform.position,1f);
		}
	}
	#endregion

	#region diverMovements

	public void MoveDiverTo(GameObject diver, Transform location){
		Debug.LogError ("Destination is " + location.gameObject);
			iTween.MoveTo (diver, location.transform.position, 0.2f);
	}
	#endregion
}
