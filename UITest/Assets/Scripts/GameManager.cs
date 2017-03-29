using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour {

	public Toggle[] playerBox;

	// MUSIC VARIABLES
	public AudioMixer masterMixer;
	private float maxVol = 0f;
	private float minVol = -80f;
	private float volTimer = 1.5f;
	bool correctMusicIsPlaying = true;

	// SOUND EFFECT VARIABLES
	private AudioSource audioPlayer;
	public AudioClip[] letterSound;
	public AudioClip correctAnswer;
	public AudioClip wrongAnswer;

	private GameObject imageOverlay;

	Scene activeSceneName;

	private int firstLetter = 0; 
	private int lastLetter = 0;
	private int randomNumber = 0;
	private int previousNumber = 0;
	public static int playerScore;
	public static int maxScore;

	private float timeLeftToSpawn = 8f;
	public float timeLeftOnTrack;
	public float countdown = 3f;

	private int[,] brailleAlphabet = new int[26,6] {
		{ 1, 0, 0, 0, 0, 0 }, // Bokstaven A
	    { 1, 1, 0, 0, 0, 0 }, // Bokstaven B
    	{ 1, 0, 0, 1, 0, 0 }, // Bokstaven C
	    { 1, 0, 0, 1, 1, 0 }, // Bokstaven D
	    { 1, 0, 0, 0, 1, 0 }, // Bokstaven E
		{ 1, 1, 0, 1, 0, 0 }, // Bokstaven F
		{ 1, 1, 0, 1, 1, 0 }, // Bokstaven G
		{ 1, 1, 0, 0, 1, 0 }, // Bokstaven H
		{ 0, 1, 0, 1, 0, 0 }, // Bokstaven I
		{ 0, 1, 0, 1, 1, 0 }, // Bokstaven J
		{ 1, 0, 1, 0, 0, 0 }, // Bokstaven K
		{ 1, 1, 1, 0, 0, 0 }, // Bokstaven L
		{ 1, 0, 1, 1, 0, 0 }, // Bokstaven M
		{ 1, 0, 1, 1, 1, 0 }, // Bokstaven N
		{ 1, 0, 1, 0, 1, 0 }, // Bokstaven O
		{ 1, 1, 1, 1, 0, 0 }, // Bokstaven P
		{ 1, 1, 1, 1, 1, 0 }, // Bokstaven Q
		{ 1, 1, 1, 0, 1, 0 }, // Bokstaven R
		{ 0, 1, 1, 1, 0, 0 }, // Bokstaven S
		{ 0, 1, 1, 1, 1, 0 }, // Bokstaven T
		{ 1, 0, 1, 0, 0, 1 }, // Bokstaven U
		{ 1, 1, 1, 0, 0, 1 }, // Bokstaven V
		{ 0, 1, 0, 1, 1, 1 }, // Bokstaven W
		{ 1, 0, 1, 1, 0, 1 }, // Bokstaven X
		{ 1, 0, 1, 1, 1, 1 }, // Bokstaven Y
		{ 1, 0, 1, 0, 1, 1 } // Bokstaven Z
	};

	private string[] brailleAlphabetLetter = new string[26] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
	private int[] playerTemp = new int[6];

	private Text activeBrailleText;
	private Text scoreText;
	private Text spawnText;
	private Text countDownText;

	// Use this for initialization

	void Start () {

		playerScore = 0;
		maxScore = -1;

		activeSceneName = SceneManager.GetActiveScene ();
		if (activeSceneName.name == "LevelOne") {
			timeLeftOnTrack = 57f;
		} else if (activeSceneName.name == "LevelTwo") {
			timeLeftOnTrack = 73f;
		}

		audioPlayer = GetComponent<AudioSource>();
		
		activeBrailleText = GameObject.Find("activeBrailleCharacter").GetComponent<Text>();
		scoreText = GameObject.Find("scoreText").GetComponent<Text>();
		spawnText = GameObject.Find("spawnText").GetComponent<Text>();
		countDownText = GameObject.Find("countDownText").GetComponent<Text>();
		imageOverlay = GameObject.Find("imageOverlay");

		setCorrectMusicVol();


		Invoke ("SpawnNewLetter", 3);

		
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (correctMusicIsPlaying == true) {
			setCorrectMusicVol ();
		} else {
			setFalseMusicVol();
		}

		CountDownTime();
		GameCountDown();
		FillPlayerTemp();
		SpawnTimer();
		scoreText.text = "Score: " + playerScore;
		spawnText.text = "Time: " + System.Math.Round(timeLeftToSpawn, 1);
		activeBrailleText.text = brailleAlphabetLetter[randomNumber];

	}

	void GameCountDown ()
	{
		timeLeftOnTrack -= Time.deltaTime;
		if (timeLeftOnTrack <= 0) {
			SceneManager.LoadScene("ScoreScreen");
		}
	}

	void CountDownTime ()
	{
		//countDownText.text = "" + System.Math.Round (countdown, 0);
	
		countdown -= Time.deltaTime;
		if (countdown <= 3 && countdown > 2)
			countDownText.text = "GET READY!";
		else if (countdown <= 2 && countdown > 1)
			countDownText.text = "SET!";
		else if (countdown <= 1 && countdown > 0)
			countDownText.text = "GO!";
		else
			imageOverlay.SetActive(false);
	}

	void SpawnTimer ()
	{
		timeLeftToSpawn -= Time.deltaTime;

		if (timeLeftToSpawn <= 0) {
			Compare();
			ResetPlayerBox();
			SpawnNewLetter();
			timeLeftToSpawn = 5f;
		}
	}

	void SpawnNewLetter ()
	{
		if (activeSceneName.name == "LevelOne") {
			firstLetter = 0;
			lastLetter = 4;
		} else if (activeSceneName.name == "LevelTwo") {
			firstLetter = 4;
			lastLetter = 8;
		}


		while (previousNumber == randomNumber) {
			randomNumber = Random.Range (firstLetter, lastLetter);
		}

		previousNumber = randomNumber;
		maxScore++;
		audioPlayer.PlayOneShot(letterSound[randomNumber]);
	}

	void FillPlayerTemp ()
	{
		
		for (int i = 0; i < playerBox.Length; i++) {
			if (playerBox[i].isOn == true) {
				//print (playerBox [i] + " is checked");
				playerTemp[i] = 1;
			}
			else {
				//print (playerBox [i] + " is unchecked");
				playerTemp[i] = 0;
			}
		}
	}

	void Compare ()
	{
		for (int i = 0; i < 6; i++) {
			if (brailleAlphabet [randomNumber, i] == playerTemp [i]) {
				print ("Correct!");
			}
			else {
				print ("Wrong");
				audioPlayer.PlayOneShot(wrongAnswer, 0.6f);
				correctMusicIsPlaying = false;
				return;
			}
		}
		audioPlayer.PlayOneShot(correctAnswer, 0.4f);
		playerScore++;
	}

	public void ResetPlayerBox ()
	{
		for (int i = 0; i < playerBox.Length; i++) {
			playerBox[i].isOn = false;
		}
	}

	void setCorrectMusicVol ()
	{
		masterMixer.SetFloat("correctMusic", maxVol);
		masterMixer.SetFloat("falseMusic", minVol);
	}

	void setFalseMusicVol ()
	{
		volTimer -= Time.deltaTime;
		masterMixer.SetFloat ("correctMusic", minVol);
		masterMixer.SetFloat ("falseMusic", maxVol);

		if (volTimer < 0) {
			correctMusicIsPlaying = true;
			volTimer = 1.5f;
		}
	}
}
