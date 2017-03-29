using UnityEngine;
using UnityEngine.UI;

namespace Lean.Touch
{
	// This script will tell you which direction you swiped in
	public class LeanSwipeDirection4 : MonoBehaviour
	{
		public GameManager other;

		protected virtual void OnEnable()
		{
			// Hook into the events we need
			LeanTouch.OnFingerSwipe += OnFingerSwipe;
		}
	
		protected virtual void OnDisable()
		{
			// Unhook the events
			LeanTouch.OnFingerSwipe -= OnFingerSwipe;
		}
	
		public void OnFingerSwipe(LeanFinger finger)
		{
			// Make sure the info text exists

				// Store the swipe delta in a temp variable
				var swipe = finger.SwipeScreenDelta;
			
				if (swipe.x < -Mathf.Abs(swipe.y))
				{
					print("You swiped left!");
				}
			
				if (swipe.x > Mathf.Abs(swipe.y))
				{
					print("You swiped right!");
				}
			
				if (swipe.y < -Mathf.Abs(swipe.x))
				{
					print("You swiped down!");
					other.ResetPlayerBox();

				}
			
				if (swipe.y > Mathf.Abs(swipe.x))
				{
					print("You swiped up!");
				}
			
		}
	}
}