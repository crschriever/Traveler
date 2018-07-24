using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPlannerMissile : MissileBase
{

    public class AIPlannerMissileResult
    {
        public Ship shipHit;
        public Vector2 shipPositionOnHit;
        public Vector2 shipPositionOnFire;
        public float fireAngle;

        public bool hitPlayerShip;

        public bool DidHitShip()
        {
            return shipHit != null;
        }

    }

    private MissileFire aiBehavior;

    AIPlannerMissileResult result;

    protected override void OnTimeEnd()
    {
        result.shipHit = null;
        result.hitPlayerShip = false;
        Destroy(gameObject);
        aiBehavior.OnMissileEnd(result);
    }

    protected override void OnImpact(GameObject hitObject)
    {
        result.shipHit = hitObject.GetComponent<Ship>();
        if (result.DidHitShip())
        {
            result.shipPositionOnHit = result.shipHit.transform.position;
            result.hitPlayerShip = !result.shipHit.GetTeamName().Equals(parentShip.GetTeamName());
        }
        else
        {
            result.hitPlayerShip = false;
        }
        Destroy(gameObject);
        aiBehavior.OnMissileEnd(result);
    }

    public void Shoot(float angle, MissileFire aiBehavior)
    {
        result = new AIPlannerMissileResult();
        this.aiBehavior = aiBehavior;
        result.fireAngle = angle;
        result.shipPositionOnFire = aiBehavior.GetShip().transform.position;
        parentShip.ShootInstantiatedMissile(angle, gameObject);
    }

}
