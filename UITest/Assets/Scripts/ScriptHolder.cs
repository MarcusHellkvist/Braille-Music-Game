using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScriptHolder : MonoBehaviour {

	// Use this for initialization
	public static int startValue;
	public Text feedbackInfoText;

	void Start ()
	{
		if (FeedbackScript.tactileFeedback == true && FeedbackScript.audioFeedback == true)
			feedbackInfoText.text = "Vibration: ON\nAudio: ON";
		else if (FeedbackScript.tactileFeedback == true && FeedbackScript.audioFeedback == false)
			feedbackInfoText.text = "Vibration: ON\nAudio: OFF";
		else if (FeedbackScript.tactileFeedback == false && FeedbackScript.audioFeedback == true)
			feedbackInfoText.text = "Vibration: OFF\nAudio: ON";
		else {
			feedbackInfoText.text = "ERROR!";
		}
	}

	public void LoadTutorial(int value) {
		startValue = value;
		SceneManager.LoadScene("Tutorial");
	}
}
