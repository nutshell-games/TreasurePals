using UnityEngine;
using System.Collections;

public class BackGroundMusicManager : MonoBehaviour {
	public enum BGMType{title =0, game};
	public static BackGroundMusicManager instance;
	public AudioSource s;

	[SerializeField]
	private AudioClip titleBGM;

	[SerializeField]
	private AudioClip GameBGM;

	void Awake(){
		if (instance == null) {
			s = GetComponent<AudioSource> ();
			instance = this;
		} else
			Destroy (this);
	}


	public void SetBGM(BGMType type){
		if (type == BGMType.title) {
			s.clip = titleBGM;
		} else if (type == BGMType.game) {
			s.clip = GameBGM;
		}

		s.Play ();
	}
}
