using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {

    public int Health = 1;
    public ParticleSystem deathEffect;

    public Material DamageMaterial;
    private Material NormalMaterial;


	// Use this for initialization
	void Start () {
        NormalMaterial = GetComponent<Renderer>().sharedMaterial;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Hit()
    {
        Health--;
        StartCoroutine("Flasher");
        if (Health <= 0)
        {
            ScoreManager.score += 10;
            Destroy(Instantiate(deathEffect.gameObject, transform.position, deathEffect.gameObject.transform.rotation) as GameObject, deathEffect.startLifetime);
            Destroy(this.gameObject);
        }
    }

    IEnumerator Flasher()
    {
        Renderer renderer = GetComponent<Renderer>();
        for (int i = 0; i < 2; i++)
        {
            renderer.sharedMaterial = DamageMaterial;
            yield return new WaitForSeconds(.1f);
            renderer.sharedMaterial = NormalMaterial;
            yield return new WaitForSeconds(.1f);
        }
        renderer.sharedMaterial = NormalMaterial;
    }

}
