using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform explosion;

    private AudioSource audio;
    private Animator anim;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (anim.GetBool("flicker") && GetComponent<ShipAddon>().lifetime - Time.time > 3)
        {
            anim.SetBool("flicker", false);
        }

        if (!anim.GetBool("flicker") && GetComponent<ShipAddon>().lifetime - Time.time <= 3)
        {
            Debug.Log("Animation Started.");
            anim.SetBool("flicker", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Enemy" && other.tag != "Meteor") return;

        if(other.tag == "Enemy") GameManager.instance.planesDestroyed++;
        if (other.tag == "Meteor") GameManager.instance.meteorsDestroyed++;

        Instantiate(explosion, other.transform.position, Quaternion.identity);
        audio.Play();
        Destroy(other.gameObject);
    }
}
