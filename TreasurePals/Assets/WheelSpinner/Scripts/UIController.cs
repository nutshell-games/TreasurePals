using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour 
{

	public Button spinButton;
	public Button backButton;

	public Spinner spinner;
	
	void Start () {
		spinButton.onClick.AddListener(spinner.Spin);
		backButton.onClick.AddListener(BackToMenu);
	}

	public void BackToMenu()
	{
		Application.LoadLevel("MainMenu");
	}
}
