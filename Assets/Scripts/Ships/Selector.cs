using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HumanDecisionMaker))]
[RequireComponent(typeof(Collider2D))]
public class Selector : MonoBehaviour
{

    private HumanDecisionMaker decisionMaker;

    private ShipAction action;

    private Collider2D myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        decisionMaker = GetComponentInParent<HumanDecisionMaker>();
        action = GetComponent<ShipAction>();
        if (action == null)
        {
            gameObject.SetActive(false);
        }
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
