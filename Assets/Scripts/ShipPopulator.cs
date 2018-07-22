using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipPopulator : MonoBehaviour
{
    public abstract Dictionary<string, GameObject[]> SpawnShips();
}