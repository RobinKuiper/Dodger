using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour
{
    float horizontalSpeed;
    float verticalSpeed;
    public float mapWidth = 5f;

    private int direction;
    private Boosters boosters;
    private Boosters.Booster booster;

    void Start()
    {
        direction = (Random.Range(0, 2) == 1) ? 1 : -1;
        horizontalSpeed = Random.Range(1.5f, 4);
        verticalSpeed = Random.Range(0.7f, 1.7f);

        boosters = GameManager.instance.GetComponent<Boosters>();
        booster = boosters.list[Random.Range(0, boosters.list.Count)];

        GetComponent<SpriteRenderer>().sprite = booster.sprite;
    }

    void Update()
    {
        if (transform.position.x >= mapWidth || transform.position.x <= -mapWidth)
            direction *= -1;

        float x = direction * horizontalSpeed * Time.fixedDeltaTime;
        float y = 1 * verticalSpeed * Time.fixedDeltaTime;
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + Vector2.right * x + Vector2.down * y;

        transform.position = newPosition;

        if (transform.position.y < -2f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player") return;

        GameObject[] objs = GameObject.FindGameObjectsWithTag(booster.prefab.gameObject.tag);

        if (objs.Length > 0)
        {
            objs[0].GetComponent<ShipAddon>().lifetime += 10f;
        }
        else
        {
            GameObject O = Instantiate(booster.prefab, other.transform.position, Quaternion.identity);
            O.transform.parent = other.transform;
        }

        Destroy(gameObject);
    }
}
