using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour {

	public Toggle[] playerBox;

	private AudioSource audioPlayer;
	public AudioClip[] letterSound;

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

	private int sValue = 0;
	private Text activeBrailleText;

	void Start () {

		audioPlayer = GetComponent<AudioSource>();

		sValue = GameObject.Find("ScriptHolder").GetComponent<ScriptHolder>().startValue;
		print (sValue);
		activeBrailleText = GameObject.Find("activeBrailleCharacter").GetComponent<Text>();
		ShowLetter();
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		/*if (Input.GetKeyDown ("space")) {
			
			sValue++;

			if (sValue == 4) {
				SceneManager.LoadScene("LevelOne");
			}
			else if (sValue == 8) {
				SceneManager.LoadScene("LevelTwo");
			}

			ShowLetter ();
		}*/





	}

	public void NextCharacter ()
	{
		sValue++;

		if (sValue == 4) {
			SceneManager.LoadScene ("LevelOne");
		} else if (sValue == 8) {
			SceneManager.LoadScene ("LevelTwo");
		} else {
			ShowLetter ();
		}


	}

	void ShowLetter ()
	{
		audioPlayer.PlayOneShot(letterSound[sValue]);

		for (int i = 0; i < 6; i++) {
			if (brailleAlphabet [sValue, i] == 1) {
				playerBox [i].isOn = true;
			} else {
				playerBox [i].isOn = false;
			}
		}
		activeBrailleText.text = brailleAlphabetLetter[sValue];
	}
}
