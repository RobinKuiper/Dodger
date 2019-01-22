using System.Collections;
using UnityEngine;

public class Gem : MonoBehaviour {

    private GameManager gm;
    public Sprite[] sprites;
    private AudioSource audio;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        audio = gm.GetComponent<AudioSource>();
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, (sprites.Length - 1))];
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
