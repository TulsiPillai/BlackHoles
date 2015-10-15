using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TypeInScript : MonoBehaviour {
	public string msg = "Replace";
	public float startDelay = 2.0f;
	public Text textComp;
	public float typeDelay = 0.01f;
	//private AudioSource putt;
	//private AudioClip Footstep;


	// Use this for initialization
	void Start () {
		StartCoroutine ("TypeIn");
		//putt = GetComponent<AudioSource> ();
	}
	void Awake () {
		textComp = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator TypeIn(){
		yield return new WaitForSeconds (startDelay);
		for (int i = 0; i<=msg.Length; i++) {
			textComp.text = msg.Substring(0,i);
			//Footstep = Resources.Load("Assets/SampleAssets/Characters/FirstPersonCharacter/Audio/Footstep01");
			//putt.PlayOneShot(Footstep01);
			yield return new WaitForSeconds(typeDelay);
		}
	
	}
	public IEnumerator TypeOfF(){
		for (int i = 0; i>=0; i--) {
			textComp.text = msg.Substring(0,i);
			yield return new WaitForSeconds(typeDelay);
		}
	}
}
