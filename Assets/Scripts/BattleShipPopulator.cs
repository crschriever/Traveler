using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipPopulator : ShipPopulator
{

    public GameObject playerShip;
    public GameObject enemyShip;

    public override GameObject[] SpawnShips()
    {
        return new GameObject[] { playerShip, enemyShip };
    }
}
