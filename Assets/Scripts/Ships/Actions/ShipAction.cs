using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipAction : MonoBehaviour
{
    protected Ship ship;
    public Ability ability;

    public virtual void Start()
    {
        ship = GetComponentInParent<Ship>();
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        if (ability.IsReady())
        {
            gameObject.SetActive(true);
        }
    }

    public abstract void Drag(bool overShip);

    public abstract void DragEnded(bool overShip);

    public abstract void Deselect();

}
