using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static float G = 1f;

    public static BattleManager instance = null;
    public static InputManager input;
    public StateMachine stateMachine;
    public ShipPopulator shipPopulator;

    public GameObject map;

    public List<Ship> ships;
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
            }
            else
            {
                input = new LaptopInput();
            }

            GameObject[] shipGameObjects = shipPopulator.SpawnShips();
            foreach (GameObject shipGameObject in shipGameObjects)
            {
                ships.Add(shipGameObject.GetComponent<Ship>());
            }
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        BattleStateMachine.instance.Init(ships);
    }
    void Update()
    {
        input.Update();
    }
}
