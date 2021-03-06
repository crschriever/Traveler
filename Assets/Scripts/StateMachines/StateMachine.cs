using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public static StateMachine instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public abstract void Init(List<Ship> ships);
    public abstract void ShipTookAim(Ship ship);
    public abstract void MissileExploded(Missile missile);
    public abstract bool IsMoveState();
    public abstract bool IsAimState();

    public abstract void ShipSelected(Ship ship);
    public abstract void ShipDeselected(Ship ship);
    public abstract bool IsSelection();

}