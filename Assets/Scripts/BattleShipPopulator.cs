using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleShipPopulator : ShipPopulator
{

    public GameObject playerShip;
    public GameObject enemyShip;

    public override Dictionary<string, GameObject[]> SpawnShips()
    {
        Dictionary<string, GameObject[]> ships = new Dictionary<string, GameObject[]>();
        ships[BattleManager.instance.playerTeamName] = new GameObject[] { playerShip };
        ships[BattleManager.instance.enemyTeamName] = new GameObject[] { enemyShip };

        return ships;
    }
}
