using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionToCenterOfEnemy : AIBehavior
{

    public float minDistance;
    public float maxDistance;
    public float wantedDistance;
    public float moveWaitTime;

    // Don't bother moving if not greater than this distance
    public float minMoveDistance;

    public float minAngleOffset;
    public float maxAngleOffset;
    public int randomAngleSamples;

    public float collisionPadding;

    private float timeSinceLastMove = 0;

    void Start()
    {
        base.Start();
    }

    public override bool TakeAction()
    {

        if (timeSinceLastMove > 0)
        {
            return false;
        }

        Vector3 playerShipsMidPoint = BattleManager.instance.FindPlayerShipsDesiredMiddle();
        Vector3 displacement = ship.transform.position - playerShipsMidPoint;
        if (Mathf.Abs(displacement.magnitude) < minDistance)
        {
            // Debug.Log(displacement.magnitude);
            // Debug.Log("Away");

            TryMoveAway(playerShipsMidPoint);
            timeSinceLastMove = moveWaitTime;
        }
        else if (Mathf.Abs(displacement.magnitude) > maxDistance)
        {
            // Debug.Log(displacement.magnitude);
            // Debug.Log("To");

            TryMoveTowards(playerShipsMidPoint);
            timeSinceLastMove = moveWaitTime;
        }
        else
        {
            return false;
        }

        return true;
    }

    void Update()
    {
        timeSinceLastMove -= Time.deltaTime;
    }

    private void TryMoveAway(Vector3 playerPoint)
    {
        Vector3 direction = ship.transform.position - playerPoint;
        float distance = wantedDistance - direction.magnitude;

        // Move as far away as we can from the player
        TryMove(direction, distance);
    }

    private void TryMoveTowards(Vector3 playerPoint)
    {
        Vector3 direction = playerPoint - ship.transform.position;
        float distance = direction.magnitude - wantedDistance;

        // Move as close as we can to the player
        TryMove(direction, distance);
    }

    private void TryMove(Vector2 direction, float distance)
    {
        // If distance is less than minMoveDistance then don't move
        if (distance < minMoveDistance)
        {
            return;
        }

        Vector2 bestDirection = Vector2.zero;
        float bestDistance = 0;

        // Try some random move angles
        for (int i = 0; i < randomAngleSamples; i++)
        {
            float angle = Random.Range(minAngleOffset, maxAngleOffset);

            Vector2 offsetDirection = Quaternion.Euler(0, 0, angle) * direction;

            // Try moving distance in that direction
            float offsetDistance = ship.PossibleMoveDistance(offsetDirection, distance);
            offsetDistance -= ship.GetCollider2D().bounds.extents.y + collisionPadding;

            // Better distance so lets pick it for now
            if (offsetDistance > bestDistance)
            {
                bestDirection = offsetDirection;
                bestDistance = offsetDistance;
            }

        }

        // Debug.Log("Ship Distance: " + offsetDirection.magnitude);
        // Debug.Log("Move Distance: " + offsetDistance);

        ship.Move(bestDirection, bestDistance);
    }

    public override Ability GetAbility()
    {
        return ship.GetAbilityOfType(Ability.Type.Move);
    }

}