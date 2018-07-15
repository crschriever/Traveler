﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject aim;
    public GameObject missilePrefab;

    public LineRenderer lineRenderer;

    private Collider2D collider;

    public float aimDistance;

    private bool selected = false;

    // Use this for initialization
    void Start()
    {
        collider = GetComponent<Collider2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        bool overSelf = collider.OverlapPoint(BattleManager.input.InputPosition());
        if (BattleManager.input.TapEnded() && overSelf)
        {
            SetSelected(!selected);
        }

        if (!selected)
        {
            return;
        }


        if (!overSelf)
        {
            if (StateMachine.instance.IsMoveState())
            {
                UpdateMove();
            }
            else if (StateMachine.instance.IsAimState())
            {
                UpdateAim();
            }
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
            Aim(BattleManager.input.InputPosition());
            BattleManager.instance.stateMachine.ShipTookAim(this);
        }
        else if (BattleManager.input.IsDragging())
        {
            Aim(BattleManager.input.InputPosition());
        }
        else if (BattleManager.input.DragEnded())
        {
            BattleManager.instance.stateMachine.ShipTookAim(this);
        }
    }

    public void Aim(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        direction.z = 0;
        float angle = FindAimAngle(direction);

        Vector3 position = new Vector3(transform.position.x, transform.position.y, aim.transform.position.z);
        aim.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        aim.transform.position = position + (direction.normalized * aimDistance);
    }

    public void Shoot()
    {
        Shoot(BattleManager.input.InputPosition());
    }

    public void Shoot(Vector3 point)
    {
        Vector3 direction = point - transform.position;
        direction.z = 0;
        float angle = FindAimAngle(direction);

        GameObject newMissile = Instantiate(missilePrefab);
        newMissile.GetComponent<Missile>().SetParentShip(this);

        Vector3 position = new Vector3(transform.position.x, transform.position.y, newMissile.transform.position.z);
        newMissile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        newMissile.transform.position = position + (direction.normalized * aimDistance);
    }

    private float FindAimAngle(Vector2 direction)
    {
        return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
    }

    public void Move()
    {
        transform.Translate(Vector2.right * Time.deltaTime);
    }

    public void SetSelected(bool selected)
    {
        if (selected)
        {
            StateMachine.instance.ShipSelected(this);
        }
        else
        {
            StateMachine.instance.ShipDeselected(this);
        }
        this.selected = selected;
    }

    public bool IsSelected()
    {
        return selected;
    }
}
