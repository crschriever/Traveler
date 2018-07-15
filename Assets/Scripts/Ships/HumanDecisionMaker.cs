using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDecisionMaker : DecisionMaker
{
    public GameObject[] actionSelectors;


    private Collider2D myCollider;
    public LineRenderer lineRenderer;

    private const int NOT_SELECTED = 0;
    private const int MOVING = 1;
    private const int AIMING = 2;

    private int selectedAction = NOT_SELECTED;

    public override void Start()
    {
        base.Start();

        actionSelectors[0].GetComponent<Selector>().action = MOVING;
        actionSelectors[1].GetComponent<Selector>().action = AIMING;
        actionSelectors[2].SetActive(false);

        for (int i = 0; i < actionSelectors.Length; i++)
        {
            actionSelectors[i].GetComponent<Selector>().decisionMaker = this;
        }

        myCollider = GetComponent<Collider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        bool overSelf = myCollider.OverlapPoint(BattleManager.input.InputPosition());
        if (BattleManager.input.TapEnded() && overSelf)
        {
            Deselect();
        }

        if (!IsSelected())
        {
            ShowSelectors();
            return;
        }

        if (IsMoving())
        {
            UpdateMove();
        }
        else if (IsAiming())
        {
            ShowAim();
            UpdateAim();
        }
    }

    private void UpdateMove()
    {
        Vector3 inputPosition = BattleManager.input.InputPosition();
        inputPosition.z = transform.position.z;

        Vector3[] positions = new Vector3[] { transform.position, inputPosition };
        lineRenderer.SetPositions(positions);
    }

    private void UpdateAim()
    {
        if (BattleManager.input.TapEnded())
        {
            ship.Aim(BattleManager.input.InputPosition());
            ship.Shoot();
            Deselect();
        }
        else if (BattleManager.input.IsDragging())
        {
            ship.Aim(BattleManager.input.InputPosition());
        }
        else if (BattleManager.input.DragEnded())
        {
            ship.Shoot();
        }
    }

    private void ShowAim()
    {
        HideSelectors();
        ship.SetAimActive(true);
    }

    private void HideSelectors()
    {
        actionSelectors[0].SetActive(false);
        actionSelectors[1].SetActive(false);
    }

    private void ShowSelectors()
    {
        ship.SetAimActive(false);

        actionSelectors[0].SetActive(true);
        actionSelectors[1].SetActive(true);
    }

    public void SetSelectedAction(int selectedAction)
    {
        this.selectedAction = selectedAction;
    }

    public int GetSelectedAction()
    {
        return selectedAction;
    }

    public void Deselect()
    {
        SetSelectedAction(NOT_SELECTED);
    }

    public bool IsSelected()
    {
        return GetSelectedAction() != NOT_SELECTED;
    }

    public bool IsMoving()
    {
        return GetSelectedAction() == MOVING;
    }

    public void SetMoving()
    {
        SetSelectedAction(MOVING);
    }

    public bool IsAiming()
    {
        return GetSelectedAction() == AIMING;
    }

    public void SetAiming()
    {
        SetSelectedAction(AIMING);
    }
}
