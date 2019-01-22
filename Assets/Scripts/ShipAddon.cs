using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipAddon : MonoBehaviour
{
    public float lifetime = 10f;

    void Start()
    {
        lifetime = Time.time + lifetime;
    }

    void Update()
    {
        if (Time.time >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}
