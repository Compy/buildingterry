using UnityEngine;
using System.Collections;

public class GUIButtonController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
		if (GUI.Button (new Rect (Screen.width - 102,Screen.height - 42,100,40), "Exit")) {
			Application.LoadLevel("MainScene");
		}
	}
}
