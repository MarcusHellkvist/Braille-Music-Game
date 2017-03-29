using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FeedbackScript : MonoBehaviour {

	public static bool tactileFeedback;
	public static bool audioFeedback;

	// Use this for initialization
	void Start () {
		tactileFeedback = false;
		audioFeedback = false;

	}

	public void TactileFeedback () {
		tactileFeedback = true;
		audioFeedback = false;
		SceneManager.LoadScene("StartMenu");
	}

	public void AudioFeedback () {
		tactileFeedback = false;
		audioFeedback = true;
		SceneManager.LoadScene("StartMenu");
	}

	public void BothFeedback () {
		tactileFeedback = true;
		audioFeedback = true;
		SceneManager.LoadScene("StartMenu");
	}

}
