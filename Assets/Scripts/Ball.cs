using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

    Rigidbody2D rb;

    public float Speed = 10f;
    public Transform Paddle;
    PerlinShake shake;

    public ParticleSystem collisionEffect;

    public AudioClip shootSound;
    private AudioSource source;
    private float minVol = 0.01f;
    private float maxVol = 0.08f;

    // Use this for initialization
    void Start () {
        Reset();
        shake = GetComponent<PerlinShake>();

        source = GetComponent<AudioSource>();
    }

    void Reset()
    { 
        rb = GetComponent<Rigidbody2D>();
        transform.position = Paddle.position + new Vector3(0, 1, 0);
        rb.velocity = new Vector2(Random.Range(Speed - 2, Speed + 3), Random.Range(Speed - 2, Speed + 3));
    }
	
	// Update is called once per frame
	void Update () {
        transform.up = rb.velocity;
    }

    void SetSpeed(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject collisionObject = col.gameObject;
        if (collisionObject.tag == "Block")
        {
            Block block = collisionObject.GetComponent<Block>();
            block.Hit();
        }

        if (collisionObject.tag == "Wall")
        {
            shake.PlayShake();
        }

        Destroy(Instantiate(collisionEffect.gameObject, col.contacts[0].point, Quaternion.FromToRotation(Vector3.forward, col.contacts[0].normal)) as GameObject, collisionEffect.startLifetime);
        source.PlayOneShot(shootSound, Random.Range(minVol, maxVol));
    }
}
