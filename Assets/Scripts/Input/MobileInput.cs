using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : InputManager
{

    public override bool IsDragging()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
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

    public override bool DragEnded()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                case TouchPhase.Stationary:
                case TouchPhase.Moved:
                    return false;
                    break;
                default:
                    return true;
            }
        }

        return true;
    }

    public override Vector3 InputPosition()
    {
        Vector3 touchPosition = Input.GetTouch(0).position;
        return Camera.main.ScreenToWorldPoint(touchPosition);
    }
}
