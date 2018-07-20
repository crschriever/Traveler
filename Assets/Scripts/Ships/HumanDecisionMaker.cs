using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HumanDecisionMaker : DecisionMaker
{
    public GameObject[] actionSelectors;
    private ShipAction selectedAction = null;

    private Collider2D myCollider;

    public override void Start()
    {
        base.Start();

        myCollider = GetComponent<Collider2D>();
    }

    void Update()
    {
        if (GetSelectedAction() == null)
        {
            ShowSelectors();
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

    private bool InputOverSelf()
    {
        return myCollider.OverlapPoint(BattleManager.input.InputPosition());
    }

    private void HideSelectors()
    {
        actionSelectors[0].SetActive(false);
        actionSelectors[1].SetActive(false);
    }

    private void ShowSelectors()
    {
        actionSelectors[0].SetActive(true);
        actionSelectors[1].SetActive(true);
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
        ShowSelectors();
    }
}
