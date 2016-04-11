using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;
using UnityEngine.UI;
//using Gesture;

// !!!: Drag & drop a GestureRecognizer prefab on to the scene first from Prefabs folder!!!
//using UnityEditor;
using System.Globalization;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.Callbacks;

public class showGuest : MonoBehaviour {

	 
	// Use this for initialization
	void Start () {
		
		}
	
	// Update is called once per frame
	void Update () {
		ShowCube ();
	}

	void ShowCube(){
		float step = 35f * Time.time;
		Vector3[] Cube = new Vector3[4];
			Cube[0] = new Vector3 (-90,30,-50);
			Cube[1] = new Vector3 (-90, 30, 30);
			Cube[2] = new Vector3 (-90, -30, 30);
			Cube[3] = new Vector3 (-90, -30, -50);
		int posit = 0;
		while (posit < 4) {
			if (posit == 3) {
				continue;
			}else
				//print ("one to two");
				gameObject.transform.position = Vector3.MoveTowards (Cube[posit], Cube[posit+1], step);
				posit++;			 
		}
	}
}
