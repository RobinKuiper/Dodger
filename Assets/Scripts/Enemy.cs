using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform[] prefabs;

    void Start()
    {
        Transform ship = prefabs[Random.Range(0, (prefabs.Length - 1))];

        Transform shipObj = Instantiate(ship, transform.position, Quaternion.identity);
        shipObj.parent = this.transform;
        shipObj.tag = this.tag;
    }
}
