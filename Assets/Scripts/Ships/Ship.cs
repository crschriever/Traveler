using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public GameObject aim;
    public InputManager input;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (BattleManager.input.IsDragging())
        {
            Vector2 direction = BattleManager.input.InputPosition() - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void Move()
    {
        transform.Translate(Vector2.right * Time.deltaTime);
    }
}
