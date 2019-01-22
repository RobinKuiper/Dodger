using System.Collections;
using UnityEngine;

public class GoldCoin : MonoBehaviour {

    private AudioSource audio;

    void Start()
    {
        audio = GameManager.instance.GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag != "Player") return;

        audio.Play(0);

        Destroy(gameObject);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.15f);
        Destroy(gameObject);
    }
}
