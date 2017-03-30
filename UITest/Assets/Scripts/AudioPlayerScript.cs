using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayerScript : MonoBehaviour {

	public AudioClip[] buttonMusic;
	private AudioSource source;
	private bool checkSound = false;

	void Awake()
	{
		source = GetComponent<AudioSource>();
	}

	void Update ()
	{
		if (checkSound == true) {
			source.Stop();
		}
	}

	public void PlayMusicLevelOne()
	{
		checkSound = false;
		source.PlayOneShot(buttonMusic[0]);
	}

	public void PlayMusicLevelTwo()
	{	
		checkSound = false;
		source.PlayOneShot(buttonMusic[1]);
	}
	public void StopAllSounds()
	{
		checkSound = true;
	}
}
