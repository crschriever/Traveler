using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInput : InputManager
{
    public override bool IsDragging()
    {
        return Input.GetMouseButton(0);
    }

    public override bool DragEnded()
    {
        return Input.GetMouseButtonUp(0);
    }

    public override Vector3 InputPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
