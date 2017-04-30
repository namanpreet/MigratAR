using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggleobjects : MonoBehaviour {
	public GameObject[] objects;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnGUI(){
		foreach (GameObject go in objects) {
			GUILayout.Toggle (go.activeSelf, go.name);
		}
	}
}
