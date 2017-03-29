using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OnEnterObject : MonoBehaviour {

	private AudioSource audioPlayer;
	public AudioClip lowDotSound;
	public AudioClip raiseDotSound;

	void Awake()
	{
		audioPlayer = GetComponent<AudioSource>();
	}

	public void CheckStatus (Toggle toggle)
	{
		if (toggle.isOn == true) {
			print ("ACTIVE");

			if (FeedbackScript.tactileFeedback == true && FeedbackScript.audioFeedback == false) {
				VibrationManager.Vibrate (400);
			} else if (FeedbackScript.tactileFeedback == false && FeedbackScript.audioFeedback == true) {
				audioPlayer.PlayOneShot (raiseDotSound, 0.60f);
				audioPlayer.PlayOneShot (lowDotSound, 0.50f);
			} else {
				VibrationManager.Vibrate (400);
				audioPlayer.PlayOneShot (raiseDotSound, 0.60f);
				audioPlayer.PlayOneShot (lowDotSound, 0.50f);
			}

		} else {
			print ("NOT ACTIVE");

			if (FeedbackScript.tactileFeedback == true && FeedbackScript.audioFeedback == false) {
				VibrationManager.Vibrate (100);
			} else if (FeedbackScript.tactileFeedback == false && FeedbackScript.audioFeedback == true) {
				audioPlayer.PlayOneShot (lowDotSound, 0.50f);
			} else {
				VibrationManager.Vibrate (100);
				audioPlayer.PlayOneShot (lowDotSound, 0.50f);
			}
		}
	}

	public void PressToggle ()
	{
		print ("PRESSED!");
		VibrationManager.Vibrate(50);
	}

}
