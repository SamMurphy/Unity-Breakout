using UnityEngine;
using System.Collections;

public class PerlinShake : MonoBehaviour {
	
	public float duration = 0.5f;
	public float speed = 1.0f;
	public float magnitude = 0.1f;
	
	public bool test = false;
    public Transform objToShake;


    Vector3 originalPos;

    void Start()
    {
        originalPos = objToShake.transform.position;
    }

    // -------------------------------------------------------------------------
    public void PlayShake() {
		
		StopAllCoroutines();
		StartCoroutine("Shake");
	}
	
	// -------------------------------------------------------------------------
	void Update() {
		if (test) {
			test = false;
			PlayShake();
		}
	}
	
	// -------------------------------------------------------------------------
	IEnumerator Shake() {
		
		float elapsed = 0.0f;

        
		float randomStart = Random.Range(-1000.0f, 1000.0f);
		
		while (elapsed < duration) {
			
			elapsed += Time.deltaTime;			
			
			float percentComplete = elapsed / duration;			
			
			// We want to reduce the shake from full power to 0 starting half way through
			float damper = 1.0f - Mathf.Clamp(2.0f * percentComplete - 1.0f, 0.0f, 1.0f);
			
			// Calculate the noise parameter starting randomly and going as fast as speed allows
			float alpha = randomStart + speed * percentComplete;
			
			// map noise to [-1, 1]
            float x = Mathf.PerlinNoise(alpha, 0.0f) * 2.0f - 1.0f;
            float y = Mathf.PerlinNoise(0.0f, alpha) * 2.0f - 1.0f;
			
			x *= magnitude * damper;
			y *= magnitude * damper;

            objToShake.transform.position = new Vector3(x, y, originalPos.z);
				
			yield return null;
		}

        objToShake.transform.position = originalPos;
	}
}
