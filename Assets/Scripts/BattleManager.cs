using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static float G = 1f;

    public static int mapWidth;
    public static int mapHeight;

    public static BattleManager instance = null;
    public static InputManager input = new LaptopInput();
    public StateMachine stateMachine;
    public ShipPopulator shipPopulator;

    public List<Ship> ships;
    public GameObject[] gravitySources;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject[] shipGameObjects = shipPopulator.SpawnShips();
            foreach (GameObject shipGameObject in shipGameObjects)
            {
                ships.Add(shipGameObject.GetComponent<Ship>());
            }

            BattleStateMachine.instance.Init(ships);
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
}
