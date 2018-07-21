using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HumanDecisionMaker : DecisionMaker
{
    public GameObject[] actionSelectors;
    private ShipAction[] actions;
    private ShipAction selectedAction = null;

    private Collider2D myCollider;

    public override void Start()
    {
        base.Start();

        myCollider = GetComponent<Collider2D>();

        actions = new ShipAction[actionSelectors.Length];
        for (int i = 0; i < actionSelectors.Length; i++)
        {
            actions[i] = actionSelectors[i].GetComponent<ShipAction>();
        }
    }

    void Update()
    {
        if (!ship.CanTakeAction())
        {
            HideSelectors();
        }
        else
        {

            if (GetSelectedAction() == null)
            {
                // Should happen every update that an action can be shown
                TryShowSelectors();
                return;
            }

            if (BattleManager.input.IsDragging())
            {
                HideSelectors();
                GetSelectedAction().Drag(InputOverSelf());
            }
            else if (BattleManager.input.DragEnded())
            {
                GetSelectedAction().DragEnded(InputOverSelf());
                Deselect();
            }
            else if (BattleManager.input.TapEnded())
            {
                Deselect();
            }

        }
    }

    private bool InputOverSelf()
    {
        return myCollider.OverlapPoint(BattleManager.input.InputPosition());
    }

    private void HideSelectors()
    {
        foreach (ShipAction action in actions)
        {
            action.Hide();
        }
    }

    private void TryShowSelectors()
    {
        foreach (ShipAction action in actions)
        {
            action.Show();
        }
    }

    public void SetSelectedAction(ShipAction selectedAction)
    {
        this.selectedAction = selectedAction;
    }

    public ShipAction GetSelectedAction()
    {
        return selectedAction;
    }

    public void Deselect()
    {
        GetSelectedAction().Deselect();
        SetSelectedAction(null);
    }
}
