using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public string name;
    public float coolDownTime;
    private float coolDownTimeRemaining;

    // Update is called once per frame
    void Update()
    {
        if (coolDownTimeRemaining >= 0)
        {
            coolDownTimeRemaining -= Time.deltaTime;
        }
    }

    public void UseAbility()
    {
        coolDownTimeRemaining = coolDownTime;
    }

    public bool IsReady()
    {
        Debug.Log(coolDownTimeRemaining);
        return coolDownTimeRemaining <= 0;
    }
}
