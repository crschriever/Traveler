using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject aim;
    public GameObject missilePrefab;

    public float aimDistance;

    private bool selected = false;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!selected)
        {
            return;
        }

        if (BattleManager.input.IsDragging())
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
        Vector2 direction = point - transform.position;
        float angle = FindAimAngle(direction);

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        aim.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        aim.transform.position = position + (direction.normalized * aimDistance);
    }

    public void Shoot()
    {
        Shoot(BattleManager.input.InputPosition());
    }

    public void Shoot(Vector3 point)
    {
        Vector2 direction = point - transform.position;
        float angle = FindAimAngle(direction);

        GameObject newMissile = Instantiate(missilePrefab);
        newMissile.GetComponent<Missile>().SetParentShip(this);

        Vector2 position = new Vector2(transform.position.x, transform.position.y);
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
        this.selected = selected;
    }

    public bool IsSelected()
    {
        return selected;
    }
}
