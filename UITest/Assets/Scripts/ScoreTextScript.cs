using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreTextScript : MonoBehaviour {

	public Text playerScoreText;
	public Text maxScoreText;

	private int pScore;
	private int mScore;

	// Use this for initialization
	void Start () {

		playerScoreText.text = "Player score: " + GameManager.playerScore;
		maxScoreText.text = "Max score: " + GameManager.maxScore;


	}

	public void LoadFirstLevel () {
		SceneManager.LoadScene("StartMenu");
	}
}
