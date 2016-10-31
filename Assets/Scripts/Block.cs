using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public int Health = 1;
    public ParticleSystem deathEffect;


	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Hit()
    {
        Health--;
        if (Health <= 0)
        {
            ScoreManager.score += 10;
            Destroy(Instantiate(deathEffect.gameObject, transform.position, deathEffect.gameObject.transform.rotation) as GameObject, deathEffect.startLifetime);
            Destroy(this.gameObject);
        }
    }
}
