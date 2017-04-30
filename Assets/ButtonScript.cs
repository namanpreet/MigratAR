using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ButtonScript : MonoBehaviour,IVirtualButtonEventHandler {
	private GameObject ButtonObject;

	// Use this for initialization
	void Start () {
		ButtonObject = GameObject.Find ("VirtualButton");
		GameObject.FindGameObjectWithTag ("StartBird").SetActive (true);
		GameObject.FindGameObjectWithTag ("VirtualButton").SetActive (true);
		ButtonObject.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);
	}

	public void OnButtonPressed(VirtualButtonAbstractBehaviour vb){
		Debug.Log ("Clicked");
		GameObject.FindGameObjectWithTag ("StartBird").SetActive (false);
		GameObject.FindGameObjectWithTag ("VirtualButton").SetActive (false);
		//GameObject.FindGameObjectWithTag ("EndBird").SetActive (true);
		//GameObject.FindGameObjectWithTag ("Cube").SetActive (true);

	}

	public void OnButtonReleased(VirtualButtonAbstractBehaviour vb) {
		Debug.Log ("Released");

	}
}
