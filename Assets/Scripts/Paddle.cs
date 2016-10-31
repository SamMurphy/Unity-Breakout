using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

public class Paddle : MonoBehaviour {

    Rigidbody2D rb;
    public float speed;
    public float maxDist = 1.8f;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	    float x = CrossPlatformInputManager.GetAxis("Horizontal");
        rb.velocity = new Vector2(x * speed, rb.velocity.y);

        if (transform.position.x < -maxDist) transform.position = new Vector3(-maxDist, transform.position.y, transform.position.z);
        if (transform.position.x > maxDist) transform.position = new Vector3(maxDist, transform.position.y, transform.position.z);
    }
}
