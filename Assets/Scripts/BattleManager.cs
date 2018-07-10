using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{

    public static BattleManager instance = null;

    public static InputManager input = new LaptopInput();

    public Ship[] Ships;
    public GravitySource[] gravitySources;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (input.DragEnded())
        {
            Debug.Log("End");
        }
    }
}
