using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipAction : MonoBehaviour
{
    protected Ship ship;

    public virtual void Start()
    {
        ship = GetComponentInParent<Ship>();
    }

    public abstract void Drag(bool overShip);

    public abstract void DragEnded(bool overShip);

    public abstract void Deselect();

}
