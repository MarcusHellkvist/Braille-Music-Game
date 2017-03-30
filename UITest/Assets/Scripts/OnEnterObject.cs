using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OnEnterObject : MonoBehaviour {

	//private AudioSource audioPlayer;
	//public AudioClip lowDotSound;
	//public AudioClip raiseDotSound;

	int lowButton;
	int highButton;
	int SoundID;

	void Start ()
	{
		// Set up Android Native Audio
		AndroidNativeAudio.makePool ();
		lowButton = AndroidNativeAudio.load ("Android Native Audio/General Button.wav");
		highButton = AndroidNativeAudio.load ("Android Native Audio/Positive Arcade Game Item Sound.wav");

	}

	void Awake()
	{
		//audioPlayer = GetComponent<AudioSource>();

	}

	public void CheckStatus (Toggle toggle)
	{
		if (toggle.isOn == true) {
			print ("ACTIVE");

			if (FeedbackScript.tactileFeedback == true && FeedbackScript.audioFeedback == false) {
				VibrationManager.Vibrate (400);
			} else if (FeedbackScript.tactileFeedback == false && FeedbackScript.audioFeedback == true) {
				//audioPlayer.PlayOneShot (raiseDotSound, 0.60f);
				//audioPlayer.PlayOneShot (lowDotSound, 0.50f);
				SoundID = AndroidNativeAudio.play(lowButton);
				SoundID = AndroidNativeAudio.play(highButton);


			} else {
				VibrationManager.Vibrate (400);
				//audioPlayer.PlayOneShot (raiseDotSound, 0.60f);
				//audioPlayer.PlayOneShot (lowDotSound, 0.50f);
				SoundID = AndroidNativeAudio.play(lowButton);
				SoundID = AndroidNativeAudio.play(highButton);
	
			}

		} else {
			print ("NOT ACTIVE");

			if (FeedbackScript.tactileFeedback == true && FeedbackScript.audioFeedback == false) {
				VibrationManager.Vibrate (100);
			} else if (FeedbackScript.tactileFeedback == false && FeedbackScript.audioFeedback == true) {
				SoundID = AndroidNativeAudio.play(lowButton);
				//audioPlayer.PlayOneShot (lowDotSound, 0.50f);
			} else {
				VibrationManager.Vibrate (100);
				SoundID = AndroidNativeAudio.play(lowButton);
				//audioPlayer.PlayOneShot (lowDotSound, 0.50f);
			}
		}
	}

	public void PressToggle ()
	{
		print ("PRESSED!");
		VibrationManager.Vibrate(50);
	}

	void OnApplicationQuit()
	{
		AndroidNativeAudio.unload(lowButton);
		AndroidNativeAudio.unload(highButton);
		AndroidNativeAudio.releasePool();
	}

}
