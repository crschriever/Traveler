using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static float G = 1.5f;

    public string playerTeamName;
    public string enemyTeamName;

    public static BattleManager instance = null;
    public static InputManager input;

    public ShipPopulator shipPopulator;

    public GameObject map;

    // Team => Ship
    public Dictionary<string, List<Ship>> ships = new Dictionary<string, List<Ship>>();
    public GameObject[] gravitySources;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                input = new MobileInput();
                Screen.orientation = ScreenOrientation.Landscape;
            }
            else
            {
                input = new LaptopInput();
            }

            // Get ships as a Dictionary ship team => ship[]
            Dictionary<string, GameObject[]> shipGameObjects = shipPopulator.SpawnShips();
            foreach (KeyValuePair<string, GameObject[]> team in shipGameObjects)
            {
                List<Ship> shipsOnTeam = new List<Ship>();
                for (int i = 0; i < team.Value.Length; i++)
                {
                    Ship ship = team.Value[i].GetComponent<Ship>();
                    shipsOnTeam.Add(ship);
                    ship.SetTeamName(team.Key);
                    // Debug.Log(ship + ": " + team.Key);
                }
                ships[team.Key] = shipsOnTeam;
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Update()
    {
        input.Update();
    }

    public Vector3 FindTeamDesiredMiddle(string teamName)
    {
        List<Ship> teamShips = ships[teamName];
        Vector3 sum = Vector2.zero;

        foreach (Ship ship in teamShips)
        {
            sum += ship.GetDesiredPosition();
        }

        sum.z = 0;

        return sum / teamShips.Count;
    }

    public Vector3 FindPlayerShipsDesiredMiddle()
    {
        return FindTeamDesiredMiddle(playerTeamName);
    }

    public Vector3 FindTeamMiddle(string teamName)
    {
        List<Ship> teamShips = ships[teamName];
        Vector3 sum = Vector2.zero;

        foreach (Ship ship in teamShips)
        {
            sum += ship.transform.position;
        }

        sum.z = 0;

        return sum / teamShips.Count;
    }

    public Vector3 FindPlayerShipsMiddle()
    {
        return FindTeamMiddle(playerTeamName);
    }
}
