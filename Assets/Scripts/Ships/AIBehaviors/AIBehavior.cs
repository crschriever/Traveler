using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehavior : MonoBehaviour
{
    protected Ship ship;
    protected Ability ability;

    public virtual void Start()
    {
        ship = GetComponent<Ship>();
        ability = GetAbility();
    }

    public bool AbilityIsReady()
    {
        return ability.IsReady();
    }

    public Ship GetShip()
    {
        return ship;
    }

    public abstract bool TakeAction();

    public abstract Ability GetAbility();

}
