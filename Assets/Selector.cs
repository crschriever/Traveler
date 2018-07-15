using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour
{

    public HumanDecisionMaker decisionMaker;

    public int action;

    Collider2D myCollider;

    void Start()
    {
        decisionMaker = GetComponent<HumanDecisionMaker>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.input.DragStarted() && myCollider.OverlapPoint(BattleManager.input.InputPosition()))
        {
            decisionMaker.SetSelectedAction(action);
        }
    }
}
