using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GestureRecognizer;
using UnityEngine.UI;
using System.Globalization;
using System.Runtime.Remoting.Lifetime;
using System.Security.Policy;
using UnityEngine.SceneManagement;
using System;


public class GestureDemoEvent : MonoBehaviour {
	[Tooltip("Messages will show up here")]
	public Text messageArea;
	[Tooltip("Score will show up here")]
	public Text scoreText;
	[Tooltip("Loose text will show up here")]
	public Text loseText;
	[Tooltip("Use for resset button")]
	public GameObject RestBtn;
	GameObject[] Life;
	GameObject[] figGesture;

	float timer = 60.0f;
	public Text timerText;

	int score = 0;
	int life = 3;
	bool lose = false;
	List<Vector2> points = new List<Vector2>();  
	private GestureLibrary gl; 
	int stage = 0;
	Scene scene;

	void Start() 
	{     
		
		gl = new GestureLibrary ("gestures");
		Life = new GameObject[3];
		figGesture = new GameObject[5];
		scene = SceneManager.GetActiveScene(); 
		figGesture[0] = GameObject.FindGameObjectWithTag ("infinity");
		figGesture [0].SetActive (false);
		figGesture[1] = GameObject.FindGameObjectWithTag ("rectangle");
		figGesture [1].SetActive (false);
		figGesture[2] = GameObject.FindGameObjectWithTag ("circle");
		figGesture [2].SetActive (false);
		figGesture[3] = GameObject.FindGameObjectWithTag ("zig zag");
		figGesture [3].SetActive (false);
		figGesture[4] = GameObject.FindGameObjectWithTag ("star");
		figGesture [4].SetActive (false);
		Life [0] = GameObject.FindGameObjectWithTag ("Life 3");
		Life [1] = GameObject.FindGameObjectWithTag ("Life 2");
		Life [2] = GameObject.FindGameObjectWithTag ("Life 1");

	}  


	void Update()
	{
		string recGestTemp = "none";
		float tempScore = 0.0f;

		TimerDown ();// Start timer when press Start button
		LifeShow ();// Show remaine life 

			if (Input.GetMouseButtonDown(0)) {     
				points.Clear(); 

			}   
			if (Input.GetMouseButton(0)) {    
				points.Add(new Vector2(Input.mousePosition.x, -Input.mousePosition.y)); 
			}
		// Recognize Gesture inputed in to Vector2 points
			if (Input.GetMouseButtonUp(0))
			{
				Gesture g = new Gesture(points);
				Result r = g.Recognize(gl);
				recGestTemp = r.Name;
				tempScore = r.Score;
				SetMessage("Gesture is recognized as <color=#ff0000>'" + r.Name + "'</color> with a accuracy of " + r.Score);
				if (life != 0 || lose == false) 
				{
					// Count score from recognized gusture 
					ScoreCount (tempScore);
				}
			}else if (lose == true) 
					{
						loseText.text = "Game over";
						RestBtn.gameObject.SetActive(lose);
					}

	}

	//Life 
	void LifeShow(){
		switch(life){
		case 2:
			if (Life [0].activeInHierarchy == true) {
				Life [0].SetActive (false);
			} 
			break;
		case 1:
			if (Life [1].activeInHierarchy == true) {
				Life [1].SetActive (false);
			}
			break;
		case 0:
			Life [2].SetActive (false);
			//loseText.text = "You Lose";
			lose = true;
			break;
		}
	}

	//Restart method on button "Restart"
	public void Restart(){
		SceneManager.LoadScene(scene.name);
	}

    /// <summary>
    /// Shows a message at the bottom of the screen
    /// </summary>
    /// <param name="text">Text to show</param>
	/// 
    public void SetMessage(string text) {
        messageArea.text = text;
    }

	//Timer function
	void TimerDown(){
		if (timer > 0.0f && lose != true) {
			timer -= Time.smoothDeltaTime;
			timerText.text = String.Format ("{0:0.00}", Convert.ToString (timer));
		} else if (timer <= 0) {
			lose = true;
			timerText.text = "Time is end";
		}
	}

	// Funciton for calculate score
	void ScoreCount(float res){
			if (res >= 0.9f)
			{
				score += 100;
				scoreText.text = score.ToString();
			}
			else if (res >= 0.8f)
			{
				score += 50;
				scoreText.text = score.ToString();
			}
			else if (res <= 0.79f)
			{
				life = life - 1;
			}
		}
}
