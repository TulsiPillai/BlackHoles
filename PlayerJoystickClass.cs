using UnityEngine;
using System.Collections;
using System;

public class PlayerJoystickClass : MonoBehaviour {
	
	private Transform originalTransform;
	private string currentButton;
	private float[] axisInput = new float[4];
	
	// Use this for initialization
	void Start () {
		for(int i = 0; i < axisInput.Length; i++)
			axisInput[i] = 0.0f;
		
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
		pos.x += axisInput [0];		
		pos.z += axisInput [1];
		transform.position = new Vector3 (pos.x, pos.y, pos.z);

	}

}
