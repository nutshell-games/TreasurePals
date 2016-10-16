using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class TopDownScroll : MonoBehaviour {
	[SerializeField]
	private GameObject TopMenu;
	[SerializeField]
	private GameObject BottomMenu;
	// Use this for initialization
	public RectTransform contentPanel;
	public ScrollRect scrollRect;

	void Update(){
	}

	public void SnapTo(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();

		contentPanel.anchoredPosition =
			(Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
			- (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
	}

}
