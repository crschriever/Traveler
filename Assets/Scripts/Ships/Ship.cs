using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject aim;
    public GameObject missilePrefab;

    public float aimDistance;

    // Use this for initialization
    void Start()
    {
        aim.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
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

    public void SetAimActive(bool active)
    {
        aim.SetActive(active);
    }
}

