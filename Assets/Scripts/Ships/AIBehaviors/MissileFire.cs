using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileFire : AIBehavior
{
    public GameObject plannerMissilePrefab;

    public float fireWaitTime;

    // Amount of sample missiles to fire
    public int sampleAmount;

    public float hitRate;

    public float missOffset;

    private int currentSampleAmount;

    private float timeSinceLastFire = 0;

    Dictionary<Ship, List<AIPlannerMissile.AIPlannerMissileResult>> successTestDictionary = new Dictionary<Ship, List<AIPlannerMissile.AIPlannerMissileResult>>();

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

        List<Ship> ships = Enumerable.ToList(successTestDictionary.Keys);

        // Haven't hit a ship
        if (ships.Count <= 0)
        {
            return false;
        }

        Ship target = ships[Random.Range(0, ships.Count)];

        List<AIPlannerMissile.AIPlannerMissileResult> resultList = successTestDictionary[target];

        bool acceptableResult = false;
        for (int i = 0; i < resultList.Count; i++)
        {
            AIPlannerMissile.AIPlannerMissileResult result = resultList[i];

            // Convert positions to vector2s
            Vector2 targetPos = target.transform.position;
            Vector2 shipPos = ship.transform.position;
            if (result.shipPositionOnHit.Equals(targetPos) && result.shipPositionOnFire.Equals(shipPos))
            {
                acceptableResult = true;
            }
            else
            {
                // Debug.Log("Not acceptable anymore target then: " + result.shipPositionOnHit + ", target now: " + targetPos);
                // Not acceptable anymore
                resultList.Remove(result);
                i--;
            }

        }

        if (acceptableResult)
        {

            AIPlannerMissile.AIPlannerMissileResult chosen = resultList[Random.Range(0, resultList.Count)];
            float fireAngle = chosen.fireAngle;

            if (Random.Range(0, 100) / 100.0 > hitRate)
            {
                Debug.Log("Ai misses on purpose");
                fireAngle += Random.Range(-missOffset, missOffset);
            }

            ship.Shoot(fireAngle, ((WeaponAbility)ability).weaponPrefab);
            ability.UseAbility();

            return true;
        }
        else
        {
            return false;
        }

    }

    void Update()
    {
        timeSinceLastFire -= Time.deltaTime;

        // Fire more missiles
        if (currentSampleAmount < sampleAmount)
        {
            FireSampleMissile();
        }
    }

    private void FireSampleMissile()
    {
        currentSampleAmount++;
        GameObject newMissile = Instantiate(plannerMissilePrefab);
        AIPlannerMissile plannerMissile = newMissile.GetComponent<AIPlannerMissile>();
        plannerMissile.GetComponent<MissileBase>().SetParentShip(ship);

        plannerMissile.Shoot(Random.Range(0, 360), this);
    }

    public void OnMissileEnd(AIPlannerMissile.AIPlannerMissileResult result)
    {
        if (result.hitPlayerShip)
        {
            AddShipHit(result);
        }
        currentSampleAmount--;
    }

    public override Ability GetAbility()
    {
        return ship.GetAbilityOfType(Ability.Type.Missile);
    }

    public void AddShipHit(AIPlannerMissile.AIPlannerMissileResult result)
    {
        Ship hitShip = result.shipHit;
        if (!successTestDictionary.ContainsKey(hitShip))
        {
            successTestDictionary.Add(hitShip, new List<AIPlannerMissile.AIPlannerMissileResult>());
        }

        successTestDictionary[hitShip].Add(result);
    }
}