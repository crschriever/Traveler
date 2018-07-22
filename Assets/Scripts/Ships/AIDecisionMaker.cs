using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionMaker : DecisionMaker
{
    private AIBehavior[] behaviors;

    public override void Start()
    {
        base.Start();

        behaviors = GetComponents<AIBehavior>();
    }

    void Update()
    {

        if (!ship.CanTakeAction())
        {
            return;
        }

        bool actionTaken = false;
        for (int i = 0; i < behaviors.Length && !actionTaken; i++)
        {
            actionTaken = behaviors[i].TakeAction();
        }
    }
}
