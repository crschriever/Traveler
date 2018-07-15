using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    private Ship playerShip;

    private Ship enemyShip;

    private bool selection = false;

    public override void Init(List<Ship> ships)
    {
        playerShip = ships[0];
        enemyShip = ships[0];
    }

    public override void ShipTookAim(Ship ship)
    {
        ship.Shoot();
        ship.SetSelected(false);
    }

    public override void MissileExploded(Missile missile)
    {
    }

    public override bool IsMoveState()
    {
        return false;
    }

    public override bool IsAimState()
    {
        return true;
    }

    public override void ShipSelected(Ship ship)
    {
        selection = true;
    }

    public override void ShipDeselected(Ship ship)
    {
        selection = false;
    }

    public override bool IsSelection()
    {
        return selection;
    }

}