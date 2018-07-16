using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDecisionMaker : DecisionMaker
{
    public GameObject[] actionSelectors;


    private Collider2D myCollider;
    public LineRenderer lineRenderer;

    public GameObject aim;
    private float aimAngle;

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
        aim.SetActive(false);

    }

    void Update()
    {
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

    private bool InputOverSelf()
    {
        return myCollider.OverlapPoint(BattleManager.input.InputPosition());
    }

    private void UpdateMove()
    {

    }

    private void AimMove()
    {
        Vector3 inputPosition = BattleManager.input.InputPosition();
        inputPosition.z = transform.position.z;

        Vector3[] positions = new Vector3[] { transform.position, inputPosition };
        lineRenderer.SetPositions(positions);
    }

    public void Move()
    {
        //ship.Move();
    }

    private void UpdateAim()
    {
        if (InputOverSelf())
        {
            SetAimActive(false);
        }
        else
        {
            SetAimActive(true);
        }

        if (BattleManager.input.IsDragging())
        {
            Aim(BattleManager.input.InputPosition());
        }
        else if (BattleManager.input.DragEnded())
        {
            Deselect();
            if (!InputOverSelf())
            {
                Shoot();
            }
        }
        else if (BattleManager.input.TapEnded())
        {
            Deselect();
        }
    }

    public void Aim(Vector3 point)
    {
        Vector3 absoluteDirection = point - transform.position;
        aimAngle = FindAimAngle(absoluteDirection);

        Quaternion absoluteQuaternion = Quaternion.Euler(0, 0, aimAngle);
        aim.transform.rotation = absoluteQuaternion;

        Vector3 relativeDirection = aim.transform.localRotation * Vector2.up;

        // Don't account for z when normalizing
        relativeDirection.z = 0;

        aim.transform.localPosition = relativeDirection.normalized * ship.aimDistance;
    }

    public void Shoot()
    {
        ship.Shoot(aimAngle);
    }

    public float FindAimAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }

    private void ShowAim()
    {
        HideSelectors();
        SetAimActive(true);
    }

    public void SetAimActive(bool active)
    {
        aim.SetActive(active);
    }

    private void HideSelectors()
    {
        actionSelectors[0].SetActive(false);
        actionSelectors[1].SetActive(false);
    }

    private void ShowSelectors()
    {
        SetAimActive(false);

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
