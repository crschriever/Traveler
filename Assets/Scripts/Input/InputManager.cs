using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class InputManager
{
    public abstract void Update();

    public abstract bool IsDragging();

    public abstract bool DragEnded();

    public abstract bool TapEnded();

    public abstract Vector3 InputPosition();

    public abstract float ZoomSpeed();

    public abstract Vector3 CameraMove();

    public abstract bool CameraMoveEnded();
}
