using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipAction : MonoBehaviour
{
    protected Ship ship;
    protected Ability ability;

    public virtual void Start()
    {
        ship = GetComponentInParent<Ship>();
        ability = GetAbility();
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

    public abstract Ability GetAbility();

}
