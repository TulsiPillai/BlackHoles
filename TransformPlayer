using UnityEngine;
using System.Collections;
using System;

public class TransformPlayer : MonoBehaviour {
	private GameObject player;
	private GameObject child;
	private Transform tran;
	private Vector3 rot;
	private Vector3 pos;
	private GameObject secondCanvas;
	private GameObject firstCanvas;
	private GameObject maleObject;
	private string currentButton;
	private float[] axisInput = new float[4];

	//private Vector3 child;
	// Use this for initialization
	void Start () {
		for(int i = 0; i < axisInput.Length; i++)
			axisInput[i] = 0.0f;
		player = GameObject.Find ("FirstPersonController");
		child = GameObject.Find ("FirstPersonCharacter");
		secondCanvas = GameObject.Find ("SecondMenu");
		firstCanvas = GameObject.Find ("MainMenu");
		firstCanvas.GetComponent<Canvas> ().enabled = true;
		maleObject = GameObject.Find("MaleCharacterNormal").gameObject;
		}
	
	// Update is called once per frame
	void Update () {

		// Get the Gamepad Analog stick’s axis data
		axisInput[0] = Input.GetAxisRaw("Axis 1"); //
		axisInput[1] = Input.GetAxisRaw("Axis 2");
		axisInput[2] = Input.GetAxisRaw("Axis 3");
		axisInput[3] = Input.GetAxisRaw("Axis 4");
		
		// Get the currently pressed Gamepad Button name
		var values = Enum.GetValues(typeof(KeyCode));
		for(int x = 0; x < values.Length; x++) {
			if(Input.GetKeyDown((KeyCode)values.GetValue(x))){
				currentButton = values.GetValue(x).ToString();
			}
		}
		
		// Transform the object.
		Vector3 pos = transform.position;
		pos.x += axisInput [2];		
		pos.z += axisInput [3];
		transform.position = new Vector3 (pos.x, 0, pos.z);

		if (Input.GetKey (KeyCode.Space)) {
			pos = new Vector3 (0, 0, 90);
			//rot = new Vector3 (0, 10, 0);
			player.transform.localPosition = pos;
			child.transform.localPosition = new Vector3 (0, 20, 0);
			//player.transform.localRotation = Quaternion.Euler (rot.x, rot.y, rot.z);

		} else if ((Input.GetKey (KeyCode.Alpha1)) || (Input.GetKey (KeyCode.Alpha2))) {
			pos = new Vector3 (0, 0, 600);
			//rot = new Vector3 (0,0,0);
			Vector3 childPos = new Vector3 (0, 200, 0);
			player.transform.localPosition = pos;	
			child.transform.localPosition = childPos;
			//player.transform.localRotation = Quaternion.Euler (rot.x, rot.y, rot.z);
			if (Input.GetKey (KeyCode.Alpha2)) {
				firstCanvas.GetComponent<Canvas> ().enabled = false;
				secondCanvas.GetComponent<Canvas> ().enabled = true;
			}
				
		} else if (Input.GetKey (KeyCode.Escape)) {
			secondCanvas.GetComponent<Canvas> ().enabled = false;
			firstCanvas.GetComponent<Canvas> ().enabled = false;
		}
	}
}
