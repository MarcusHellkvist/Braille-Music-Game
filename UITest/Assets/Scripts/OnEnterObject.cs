using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnEnterObject : MonoBehaviour {

	public void CheckStatus (Toggle toggle)
	{
		if (toggle.isOn == true) {
			print ("ACTIVE");
  			VibrationManager.Vibrate(500);
		} else {
			print ("NOT ACTIVE");
			VibrationManager.Vibrate(100);
		}
	}

	public void PressToggle ()
	{
		print ("PRESSED!");
		VibrationManager.Vibrate(50);
	}

}
