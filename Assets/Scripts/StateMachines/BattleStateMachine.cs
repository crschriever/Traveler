using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    private Ship playerShip;

    private Ship enemyShip;

    public override void Init(List<Ship> ships)
    {
        playerShip = ships[0];
        enemyShip = ships[0];

        playerShip.SetSelected(true);
    }

    public override void ShipTookAim(Ship ship)
    {
        ship.Shoot();
        ship.SetSelected(false);
    }

    public override void MissileExploded(Missile missile)
    {
        missile.GetParentShip().SetSelected(true);
    }

    public override bool IsMoveState()
    {
        return false;
    }

    public override bool IsAimState()
    {
        return true;
    }
}