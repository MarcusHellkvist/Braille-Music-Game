using UnityEngine;
using System;
using System.Threading;

public class VibrationManager: MonoBehaviour  {
	private static AndroidJavaObject vibrationManager = null;
	
	static VibrationManager() {
		if (Application.platform == RuntimePlatform.Android)
			vibrationManager = new AndroidJavaObject("com.takohi.unity.plugins.vibrator.VibrationManager"); 
	}
	
	/// <summary>
	/// Determines whether this device has vibrator.
	/// </summary>
	/// <returns>
	/// <c>true</c> if this device has vibrator; otherwise, <c>false</c>.
	/// </returns>
	public static bool HasVibrator() {
		if (Application.platform == RuntimePlatform.Android)
			return vibrationManager.Call<bool>("hasVibrator");
		else
			return false;
	}
	
	/// <summary>
	/// Vibrate constantly for the specified period of time.
	/// </summary>
	/// <param name='milliseconds'>
	/// The number of milliseconds to vibrate.
	/// </param>
	public static void Vibrate(int milliseconds) {
		if (Application.platform == RuntimePlatform.Android)
			vibrationManager.Call("vibrate", milliseconds);
	}
		
	/// <summary>
	/// Vibrate with a given pattern.
	/// Pass in an array of ints that are the durations for which to turn on or off the vibrator in milliseconds. 
	/// The first value indicates the number of milliseconds to wait before turning the vibrator on. 
	/// The next value indicates the number of milliseconds for which to keep the vibrator on before turning it off. 
	/// Subsequent values alternate between durations in milliseconds to turn the vibrator off or to turn the vibrator on.
	///
	/// To cause the pattern to repeat, pass the index into the pattern array at which to start the repeat, or -1 to disable repeating.
	/// </summary>
	/// <param name='pattern'>
	/// An array of longs of times for which to turn the vibrator on or off.
	/// </param>
	/// <param name='repeat'>
	/// The index into pattern at which to repeat, or -1 if you don't want to repeat.
	/// </param>
	public static void Vibrate(long[] pattern, int repeat) {
		if (Application.platform == RuntimePlatform.Android)
			vibrationManager.Call("vibrate", pattern, repeat);
	}
	
	/// <summary>
	/// Vibrate with a given pattern.
	/// Pass in an array of ints that are the durations for which to turn on or off the vibrator in milliseconds. 
	/// The first value indicates the number of milliseconds to wait before turning the vibrator on. 
	/// The next value indicates the number of milliseconds for which to keep the vibrator on before turning it off. 
	/// Subsequent values alternate between durations in milliseconds to turn the vibrator off or to turn the vibrator on.
	///
	/// To cause the pattern to repeat, pass the index into the pattern array at which to start the repeat, or -1 to disable repeating.
	/// You can also defined the number of loops to execute before the vibrator definitively turn off.
	/// </summary>
	/// <param name='pattern'>
	/// An array of longs of times for which to turn the vibrator on or off.
	/// </param>
	/// <param name='repeat'>
	/// The index into pattern at which to repeat.
	/// </param>
	/// <param name='loop'>
	/// The number of loop. -1 if you want to repeat undefinitively.
	/// </param>
	public static void Vibrate(long[] pattern, int repeat, int loop) {
		if (Application.platform == RuntimePlatform.Android) {
			if(repeat < 0 || loop < 0)
				Vibrate (pattern, -1);
			else {
				int sum = 0;
				int repeatSum = 0;
				for(int i = 0; i < pattern.Length; ++i) {
					sum += (int) pattern[i];
					if(i >= repeat)
						repeatSum += (int) pattern[i];
				}
				sum += repeatSum * loop;
					
				vibrationManager.Call("vibrate", pattern, repeat);
				
				GameObject gameObject = new GameObject("Vibrator");
				VibrationManager v = gameObject.AddComponent<VibrationManager>() as VibrationManager;
				v.CancelMono(sum / 1000f);
			}
		}
	}
	
	/// <summary>
	/// Turn the vibrator off. .
	/// </summary>
	public static void Cancel() {
		if (Application.platform == RuntimePlatform.Android)
			vibrationManager.Call("cancel");
	}
	
	private void CancelMono (float time) {
		Invoke("CancelMono", time);
	}
	
	private void CancelMono() {
		VibrationManager.Cancel();
		Destroy(gameObject);
	}
}

