using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScript : MonoBehaviour {

	public GameObject a,b,c,d,e,f,g,h,i,j,k,l,m;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= 3.0) {
			a.SetActive (true);
		} 
		if (Time.time >= 4.0){
			b.SetActive (true);
		}
		if (Time.time >= 5.0){
			c.SetActive (true);
		}
		 if (Time.time >= 6.0){
			d.SetActive (true);
		}
		 if (Time.time >= 7.0){
			e.SetActive (true);
		}
		if (Time.time >= 8.0){
			f.SetActive (true);
		}
		if (Time.time >= 9.0){
			g.SetActive (true);
		}
		if (Time.time >= 10.0){
			h.SetActive (true);
		}
		if (Time.time >= 11.0){
			i.SetActive (true);
		}
		if (Time.time >= 12.0){
			j.SetActive (true);
		}
		if (Time.time >= 13.0){
			k.SetActive (true);
		}
		if (Time.time >= 14.0){
			l.SetActive (true);
		}
		if (Time.time >= 15.0){
			m.SetActive (true);
		}

	}
}
