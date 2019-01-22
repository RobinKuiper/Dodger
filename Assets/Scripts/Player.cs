using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public Transform bonusParticles;
    public Transform explosion;

    public float speed = 15f;
    public float mapWidth = 5f;

    Rigidbody2D rb;

    public bool cantDie = false;

    private ApplicationManager am;
    private GameManager gm;

    void Start()
    {
        am = ApplicationManager.instance;
        gm = GameManager.instance;

        rb = GetComponent<Rigidbody2D>();

        GetComponent<SpriteRenderer>().sprite = am.ship.sprite;

        StartCoroutine(doBoosters());
    }

    void FixedUpdate()
    {
        if (GameManager.instance.gameEnded || GameManager.instance.gamePaused) return;

        float direction = 0;

        // Touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            direction = (touch.position.x > Screen.width / 2) ? 1 : -1;
        }
        else // Keyboard
        {
            direction = Input.GetAxisRaw("Horizontal");
        }

        float x = direction * speed * Time.fixedDeltaTime;
        Vector2 newPosition = rb.position + Vector2.right * x;
        newPosition.x = Mathf.Clamp(newPosition.x, -mapWidth, mapWidth);

        rb.MovePosition(newPosition);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.collider.tag)
        {
            case "Enemy":
            case "Meteor":
                if (cantDie) Destroy(other.gameObject);
                else
                {
                    Instantiate(explosion, transform.position, Quaternion.identity);
                    GameManager.instance.ShowContinuePanel();
                    Destroy(other.gameObject);
                }
                break;

            case "Points":
                GameManager.instance.gold++;
                Instantiate(bonusParticles, transform.position, Quaternion.identity);
                break;
        }
    }

    IEnumerator doBoosters()
    {
        yield return new WaitForSeconds(3f);

        am.boosters.ForEach(boosterId =>
        {
            GameObject t = Instantiate(gm.GetComponent<Boosters>().list[boosterId].prefab, transform.position, Quaternion.identity);
            t.transform.SetParent(this.transform, false);
        });

        am.boosters.Clear();
    }
}
