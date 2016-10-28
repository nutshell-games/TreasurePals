using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIDG : MonoBehaviour 
{
	public Button spinButton;

	public SpinnerDG spinner;
	
	void Start () 
	{
		spinButton.onClick.AddListener(spinner.Spin);
	}
}
