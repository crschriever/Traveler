using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopInput : InputManager
{

    private Vector3 startingMousePosition;

    private Vector3 moveDistance;

    private bool movingCamera = false;

    public override void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startingMousePosition = Input.mousePosition;

            movingCamera = Input.GetKey(KeyCode.LeftShift);
        }

        if (Input.GetMouseButton(0))
        {
            moveDistance = Camera.main.ScreenToWorldPoint(startingMousePosition) - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            movingCamera = false;
        }
    }

    public override bool IsDragging()
    {
        // Mouse not down
        if (!Input.GetMouseButton(0) || movingCamera)
        {
            return false;
        }

        // If click just started or hasn't moved yet
        if (Input.GetMouseButtonDown(0) || Input.mousePosition.Equals(startingMousePosition))
        {
            return false;
        }

        return true;
    }

    public override bool DragEnded()
    {
        return !movingCamera && !Input.mousePosition.Equals(startingMousePosition) && Input.GetMouseButtonUp(0);
    }

    public override bool TapEnded()
    {
        return !movingCamera && Input.mousePosition.Equals(startingMousePosition) && Input.GetMouseButtonUp(0);
    }

    public override Vector3 InputPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public override float ZoomSpeed()
    {
        return Input.GetAxis("Mouse ScrollWheel");
    }

    public override Vector3 CameraMove()
    {
        if (Input.GetMouseButton(0) && movingCamera)
        {
            Vector3 worldDistance = moveDistance;
            worldDistance.z = 0;
            return worldDistance;
        }

        return Vector3.zero;
    }

    public override bool CameraMoveEnded()
    {
        return Input.GetMouseButtonUp(0) && movingCamera;
    }
}
