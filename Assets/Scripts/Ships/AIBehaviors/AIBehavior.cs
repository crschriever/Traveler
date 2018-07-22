using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBehavior : MonoBehaviour
{
    protected Ship ship;

    public virtual void Start()
    {
        ship = GetComponent<Ship>();
    }

    public abstract bool TakeAction();
}
