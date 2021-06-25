using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureInteraction : MonoBehaviour
{
	void OnEnable()
	{
		Lean.Touch.LeanTouch.OnFingerTap += HandleFingerTap;
	}

	void OnDisable()
	{
		Lean.Touch.LeanTouch.OnFingerTap -= HandleFingerTap;
	}

	void HandleFingerTap(Lean.Touch.LeanFinger finger)
	{
		Debug.Log("You just tapped the screen with finger " + finger.Index + " at Screen coordinate" + finger.ScreenPosition);
		Debug.Log("You just tapped the screen with finger " + finger.Index + " at World coordinate" + finger.ScreenPosition);

	}
}
