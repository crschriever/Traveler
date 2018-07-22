using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire : AIBehavior
{
    public float maxFlightTime;
    public float fireWaitTime;

    // Amount of sample missiles to fire
    public int sampleAmount;

    private float timeSinceLastFire = 0;

    void Start()
    {
        base.Start();
    }

    public override bool TakeAction()
    {

        if (timeSinceLastFire > 0)
        {
            return false;
        }

        Vector3 playerShipsMidPoint = BattleManager.instance.FindPlayerShipsDesiredMiddle();
        Vector3 displacement = ship.transform.position - playerShipsMidPoint;



        return true;
    }

    void Update()
    {
        timeSinceLastFire -= Time.deltaTime;
    }

    public override Ability GetAbility()
    {
        return ship.GetAbilityOfType(Ability.Type.Missile);
    }
}