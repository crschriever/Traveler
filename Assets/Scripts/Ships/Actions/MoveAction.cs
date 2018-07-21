using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MoveAction : ShipAction
{

    public GameObject dotPrefab;
    public float dotDistance = .5f;
    public int maxDotCount = 50;

    public Color clearColor;
    public Color hitColor;
    public Color fantomShipColor;

    GameObject[] dots;
    GameObject shipOutline;

    private Vector3 direction;
    private float distance;
    private bool hit;

    public override void Start()
    {
        base.Start();
        dots = new GameObject[maxDotCount];
        shipOutline = GetShipOutline();
        shipOutline.SetActive(false);

    }

    public override void Drag(bool overShip)
    {
        Aim(BattleManager.input.InputPosition());
    }

    public override void DragEnded(bool overShip)
    {
        if (!overShip)
        {
            if (!hit)
            {
                Move();
            }
            Deselect();
        }
    }

    public override void Deselect()
    {
        HideAllDots();
        shipOutline.SetActive(false);
    }

    public void Aim(Vector3 point)
    {
        HideAllDots();

        Vector3 inputPosition = BattleManager.input.InputPosition();
        inputPosition.z = transform.position.z;

        Vector3 position = ship.transform.position;

        direction = inputPosition - position;
        distance = direction.magnitude;

        float shipHeight = ship.GetCollider2D().bounds.extents.y;
        hit = ship.DetectHit(direction, distance + shipHeight);

        // Dots
        for (int i = 0; i < (distance - shipHeight * 2) / dotDistance && i < maxDotCount; i++)
        {
            position = Vector3.MoveTowards(position, inputPosition, dotDistance);

            GameObject dot = GetDot(i);

            dot.GetComponent<SpriteRenderer>().color = hit ? hitColor : clearColor;

            dot.transform.position = position;
            dot.SetActive(true);
        }


        // Outline
        shipOutline.SetActive(true);
        shipOutline.transform.position = inputPosition;
        shipOutline.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    private GameObject GetDot(int i)
    {
        if (dots[i] == null)
        {
            dots[i] = Instantiate(dotPrefab);
        }

        return dots[i];
    }

    private void HideAllDots()
    {
        foreach (GameObject dot in dots)
        {
            if (dot != null)
            {
                dot.SetActive(false);
            }
        }
    }

    public void Move()
    {
        ship.Move(ship.transform.position + direction.normalized * distance, Quaternion.FromToRotation(Vector3.up, direction));
        ability.UseAbility();
    }

    public GameObject GetShipOutline()
    {
        GameObject sprGameObj = new GameObject();
        sprGameObj.name = "Outline";
        sprGameObj.AddComponent<SpriteRenderer>();
        SpriteRenderer sprRenderer = sprGameObj.GetComponent<SpriteRenderer>();
        sprRenderer.sprite = ship.GetBackgroundSprite();
        sprRenderer.color = fantomShipColor;
        return sprGameObj;
    }

}