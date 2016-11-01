using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour {

    Rigidbody2D rb;

    public float Speed = 10f;
    public Transform Paddle;
    PerlinShake shake;

    public ParticleSystem collisionEffect;
    public ParticleSystem deathEffect;
    public ParticleSystem deathWallEffect;
    public ParticleSystem collisionWithBlockEffect;

    public AudioClip shootSound;
    private AudioSource source;
    private float minVol = 0.01f;
    private float maxVol = 0.08f;

    private bool dead = false;
    private float time = 2f;
    private int countdown = 3;
    public float TimeBetweenCountdown;
    public Text CountdownText;

    // Use this for initialization
    void Start () {

        Reset();

        rb.velocity = new Vector2(0, 0);
        GetComponent<Renderer>().enabled = false;
        dead = true;

        shake = GetComponent<PerlinShake>();
        source = GetComponent<AudioSource>();
    }

    void Reset()
    {
        time = TimeBetweenCountdown;
        countdown = 3;
        CountdownText.text = "";

        GetComponent<Renderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;

        rb = GetComponent<Rigidbody2D>();
        //Paddle.position = new Vector3(0, Paddle.position.y, Paddle.position.z);
        transform.position = Paddle.position + new Vector3(0, 1, 0);
        rb.velocity = new Vector2(Random.Range(Speed - 2, Speed + 3), Random.Range(Speed - 2, Speed + 3));
    }
	
	// Update is called once per frame
	void Update () {
        transform.up = rb.velocity;

        if (dead)
        {
            CountdownText.text = countdown.ToString();
            time -= Time.deltaTime;
            if (time <= 0)
            {
                countdown--;
                GetComponent<Flash>().StartFlash();
                time = TimeBetweenCountdown;
                if (countdown <= 0)
                {
                    dead = false;
                    Reset();
                }
            }
        }

        //Paddle.position = new Vector3(transform.position.x, Paddle.position.y, Paddle.position.z);
    }

    void SetSpeed(float x, float y)
    {
        rb.velocity = new Vector2(x, y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject collisionObject = col.gameObject;

        // Block Collision
        if (collisionObject.tag == "Block")
        {
            Destroy(Instantiate(collisionWithBlockEffect.gameObject, col.contacts[0].point, Quaternion.FromToRotation(Vector3.forward, col.contacts[0].normal)) as GameObject, collisionWithBlockEffect.startLifetime);
            Block block = collisionObject.GetComponent<Block>();
            block.Hit();
        }

        // Wall Collision
        else if (collisionObject.tag == "Wall")
        {
            shake.PlayShake();
        }

        // Lose life / Die
        else if (collisionObject.tag == "Floor")
        {
            ScoreManager.lives--;

            Destroy(Instantiate(deathEffect.gameObject, col.contacts[0].point, Quaternion.FromToRotation(Vector3.forward, col.contacts[0].normal)) as GameObject, deathEffect.startLifetime);
            Destroy(Instantiate(deathWallEffect.gameObject, new Vector3(0, -5f, 0), deathWallEffect.gameObject.transform.rotation) as GameObject, deathWallEffect.startLifetime);

            GetComponent<Flash>().StartFlash();

            if (ScoreManager.lives < 0)
            {
                ScoreManager.GameOver();
                rb.velocity = new Vector2(0, 0);
                GetComponent<Renderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
            }
            else
            {
                rb.velocity = new Vector2(0, 0);
                GetComponent<Renderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
                dead = true;
            }
        }

        // Collision particle dust and sound effect
        Destroy(Instantiate(collisionEffect.gameObject, col.contacts[0].point, Quaternion.FromToRotation(Vector3.forward, col.contacts[0].normal)) as GameObject, collisionEffect.startLifetime);
        source.PlayOneShot(shootSound, Random.Range(minVol, maxVol));
    }
}
