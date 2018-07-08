using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : InputManager {
	
	public override bool IsDragging() {
		if (Input.touchCount > 0) {
			switch(Input.GetTouch(0).phase) {
				case TouchPhase.Began:
				case TouchPhase.Stationary:
				case TouchPhase.Moved:
					return true;
					break;
				default:
					return false;
			}
		}

		return true;
	}

	public override Vector2 InputPosition() {
		Vector3 touchPosition = Input.GetTouch(0).position;
		touchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
		return new Vector2(touchPosition.x, touchPosition.y);
	}
}
