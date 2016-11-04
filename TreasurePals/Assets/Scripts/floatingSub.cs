using UnityEngine;
using System.Collections;

public class floatingSub : MonoBehaviour {
	public float floatSpan = 2.0f;
	public float speed = 1.0f;
	public float startY;
	public float man1StartY;
	public float man2StartY;
	public float man3StartY;
	public float man4StartY;

	public GameObject man1;
	public GameObject man2;
	public GameObject man3;
	public GameObject man4;

	[SerializeField]
	Transform rightPosition;

	[SerializeField]
	Transform leftPosition;

	// Use this for initialization
	void Start () {
		SetDefaultY ();
	}
	
	// Update is called once per frame
	void Update () {
		float floatY = Mathf.Sin (Time.time * speed) * floatSpan / 2.0f;
		transform.localPosition = new Vector2(transform.localPosition.x, startY + floatY);
		if (man1 != null) {
			man1.transform.localPosition = new Vector2 (man1.transform.localPosition.x, man1StartY + floatY);
		}
		if (man2 != null) {
			man2.transform.localPosition = new Vector2 (man2.transform.localPosition.x, man2StartY + floatY);
		}
		if (man3 != null) {
			man3.transform.localPosition = new Vector2 (man3.transform.localPosition.x, man3StartY + floatY);
		}
		if (man4 != null) {
			man4.transform.localPosition = new Vector2 (man4.transform.localPosition.x, man4StartY + floatY);
		}
	}

	public void SetDefaultY(){
		startY = transform.localPosition.y;
		man1StartY = man1.transform.localPosition.y;
		man2StartY = man2.transform.localPosition.y;
		man3StartY = man3.transform.localPosition.y;
		man4StartY = man4.transform.localPosition.y;
	}

	public void SetDiverY(){
		man1StartY = leftPosition.localPosition.y;
		man2StartY = rightPosition.localPosition.y;
		man3StartY = rightPosition.localPosition.y;
		man4StartY = leftPosition.localPosition.y;
	}

}
