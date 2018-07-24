using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDecisionMaker : DecisionMaker
{
    public AIBehavior[] behaviors;

    public override void Start()
    {
        base.Start();
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
            AIBehavior behavior = behaviors[i];

            if (!behavior.AbilityIsReady())
            {
                continue;
            }

            actionTaken = behavior.TakeAction();
        }
    }
}
