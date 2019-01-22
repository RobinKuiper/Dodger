using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 2f;
    public Transform explosion;

    private AudioSource audio;

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        float y = -1 * speed * Time.fixedDeltaTime;
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + Vector2.down * y;

        transform.position = newPosition;

        if (transform.position.y > 15f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy" || other.tag == "Meteor")
        {
            GameManager.instance.gold++;
            if(other.tag == "Enemy") GameManager.instance.planesDestroyed++;
            if (other.tag == "Meteor") GameManager.instance.meteorsDestroyed++;
            audio.Play();
            Destroy(other.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
        //Destroy(gameObject);
    }
}
