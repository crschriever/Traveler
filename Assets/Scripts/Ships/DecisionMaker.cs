using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DecisionMaker : MonoBehaviour
{
    protected Ship ship;

    public virtual void Start()
    {
        ship = GetComponent<Ship>();
    }

}
