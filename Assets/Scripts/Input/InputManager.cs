using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputManager
{

    public abstract bool IsDragging();

    public abstract bool DragEnded();

    public abstract Vector3 InputPosition();

}
