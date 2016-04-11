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
		//cube.Name = "rectangle";
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

		TimerDown ();
			if (Input.GetMouseButtonDown(0)) {     
				points.Clear(); 

			}   
			if (Input.GetMouseButton(0)) {    
				points.Add(new Vector2(Input.mousePosition.x, -Input.mousePosition.y)); 
			}
			if (Input.GetMouseButtonUp(0))
			{
				Gesture g = new Gesture(points);
				Result r = g.Recognize(gl);
				recGestTemp = r.Name;
				tempScore = r.Score;
				SetMessage("Gesture is recognized as <color=#ff0000>'" + r.Name + "'</color> with a accuracy of " + r.Score);
				if (life != 0 || lose == false) 
				{
					LifeShow ();
					ScoreCount (tempScore);
				}
			}else if (life == 0 || lose == true) 
			{
				loseText.text = "Game over";
				RestBtn.gameObject.SetActive(lose);
			}
/*		while (stage < 3) 
		{	
			switch (stage) 
			{
			case 0:
				if (figGesture [0].activeInHierarchy == false) {
					figGesture [0].gameObject.SetActive (true);
					StartCoroutine (LateCall (figGesture [0]));
					if (recGestTemp != "infinity") {
						life = life - 1;
					}else stage++;
				}
				
				break;
			case 1:
				if (figGesture [1].activeInHierarchy == false) {
					figGesture [1].gameObject.SetActive (true);
					StartCoroutine (LateCall (figGesture [1]));
					if (recGestTemp!= "rectangle") {
						life = life - 1;
					} else stage++;
				}
				
				break;
			case 2:
				if (figGesture [2].activeInHierarchy == false) {
					figGesture [2].gameObject.SetActive (true);
					StartCoroutine (LateCall (figGesture [2]));
					if (recGestTemp != "circle") {
						life = life - 1;
					} else stage++;
				}
				break;
			default:
				break;
			}
		}
*/

	}

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
   /*
    // Subscribe your own method to OnRecognition event 
    void OnEnable() {
        GestureBehaviour.OnRecognition += OnGestureRecognition;
    }

    // Unsubscribe when this game object or monobehaviour is disabled.
    // If you don't unsubscribe, this will give an error.
    void OnDisable() {
        GestureBehaviour.OnRecognition -= OnGestureRecognition;
    }

    // Unsubscribe when this game object or monobehaviour is destroyed.
    // If you don't unsubscribe, this will give an error.
    void OnDestroy() {
        GestureBehaviour.OnRecognition -= OnGestureRecognition;
    }


    /// <summary>
    /// The method to be called on recognition event
    /// </summary>
    /// <param name="r">Recognition result</param>
    /// 
    /// <remarks>
    /// Implement your own method here. This method will be called by GestureBehaviour
    /// automatically when a gesture is recognized. You can write your own script
    /// in this method (kill enemies, shoot a fireball, or cast some spell etc.)
    /// This method's signature should match the signature of OnRecognition event,
    /// so your method should always have one parametre with the type of Result. Example:
    /// 
    /// void MyMethod(Result gestureResult) {
    ///     kill enemy,
    ///     shoot fireball,
    ///     cast spell etc.
    /// }
    /// 
    /// You can decide what to do depending on the name of the gesture and its score.
    /// For example, let's say, if the player draws the letter of "e" (let's name the 
    /// gesture "Fireball") and it is 50% similar to our "Fireball" gesture, shoot a fireball:
    /// 
    /// void MagicHandler(Result magicGesture) {
    /// 
    ///    if (magicGesture.Name == "Fireball" && magicGesture.Score >= 0.5f) {
    ///        Instantiate(fireball, transform.position, Quaternion.identity);
    ///    }
    /// 
    /// }
    /// 
    /// !: You can name this method whatever you want, but you should use the same name
    /// when subscribing and unsubscribing. If your method's name is MagicHandler like above,
    /// then:
    /// 
    /// void OnEnable() {
    ///   GestureBehaviour.OnRecognition += MagicHandler;
    /// }
    /// </remarks>
    void OnGestureRecognition(Result r) {
       SetMessage("Gesture is recognized as <color=#ff0000>'" + r.Name + "'</color> with a score of " + r.Score);
	
	}
	*/
	public void Restart(){
		SceneManager.LoadScene(scene.name);
	}
	/*
	void OnGUI() {
		if (lose == true) {
			if (GUI.Button (new Rect (500, 30, 400, 30), "Restart")) {
				Restart ();
			} 
		}
	}*/
    /// <summary>
    /// Shows a message at the bottom of the screen
    /// </summary>
    /// <param name="text">Text to show</param>
	/// 
    public void SetMessage(string text) {
        messageArea.text = text;
    }
	void TimerDown(){
		if (timer > 0.0f && lose != true) {
			timer -= Time.smoothDeltaTime;
			timerText.text = String.Format ("{0:0.00}", Convert.ToString (timer));
		} else if (timer <= 0) {
			lose = true;
			timerText.text = "Time is end";
		}
	}

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

	IEnumerator LateCall(GameObject gmobg)
	{

		yield return new WaitForSeconds(1.5f);

		gmobg.SetActive(false);

	}
}
