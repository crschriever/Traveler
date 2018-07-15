using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class InputManager
{

    protected Vector3 startingMousePosition;
    protected Vector3 moveDistance;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startingMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            moveDistance = Camera.main.ScreenToWorldPoint(Input.mousePosition) - Camera.main.ScreenToWorldPoint(startingMousePosition);
        }
    }

    public bool IsDragging()
    {
        // Mouse not down or zooming
        if (!Input.GetMouseButton(0) || IsZooming())
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

    public bool DragEnded()
    {
        return !IsZooming() && !Input.mousePosition.Equals(startingMousePosition) && Input.GetMouseButtonUp(0);
    }

    public Vector3 DragDistance()
    {
        moveDistance.z = 0;
        return -moveDistance;
    }

    public bool TapEnded()
    {
        return !IsZooming() && Input.mousePosition.Equals(startingMousePosition) && Input.GetMouseButtonUp(0);
    }

    public Vector3 InputPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public abstract float ZoomSpeed();

    protected abstract bool IsZooming();
}
