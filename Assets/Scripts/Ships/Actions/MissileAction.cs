using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileAction : ShipAction
{

    public GameObject aim;

    private float aimAngle;

    public override void Start()
    {
        base.Start();
        aim.SetActive(false);
    }

    public override void Drag(bool overShip)
    {
        aim.SetActive(!overShip);
        Aim(BattleManager.input.InputPosition());
    }

    public override void DragEnded(bool overShip)
    {
        if (!overShip)
        {
            Shoot();
            Deselect();
        }
    }

    public override void Deselect()
    {
        aim.SetActive(false);
    }

    public void Aim(Vector3 point)
    {
        Vector3 absoluteDirection = point - transform.position;
        aimAngle = ship.FindAimAngle(absoluteDirection);

        Quaternion absoluteQuaternion = Quaternion.Euler(0, 0, aimAngle);
        aim.transform.rotation = absoluteQuaternion;

        Vector3 relativeDirection = aim.transform.localRotation * Vector2.up;

        // Don't account for z when normalizing
        relativeDirection.z = 0;

        aim.transform.localPosition = relativeDirection.normalized * ship.aimDistance;
    }

    public void Shoot()
    {
        ship.Shoot(aimAngle);
    }

}