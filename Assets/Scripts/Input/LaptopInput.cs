using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInput : InputManager
{
    public override float ZoomSpeed()
    {
        return Input.GetAxis("Mouse ScrollWheel") * 1.4f;
    }

    protected override bool IsZooming()
    {
        return Input.GetAxis("Mouse ScrollWheel") != 0;
    }

}
