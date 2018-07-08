using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInput : InputManager {
	public override bool IsDragging() {
		return Input.GetMouseButton(0);
	}

	public override Vector2 InputPosition() {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		return new Vector2(mousePosition.x, mousePosition.y);
	}
}
