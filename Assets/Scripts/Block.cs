using UnityEngine;

public class Block : MonoBehaviour {

    public float gravityScaleMultiplier = 20f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale += Time.timeSinceLevelLoad / gravityScaleMultiplier;
    }

    void Update () {
        if (transform.position.y < -2f)
            Destroy(gameObject);
	}

}
