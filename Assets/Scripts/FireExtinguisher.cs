using UnityEngine;

public class FireCollision : MonoBehaviour
{
    public ParticleSystem fireSystem;  // Reference to the fire particle system
    private int hitCount = 0;
    public int hitsToExtinguish = 10;

    public void OnParticleCollision(GameObject other)
    {
            hitCount++;
            Debug.Log("Particle Collision Detected! Hits: " + hitCount);

            // Reduce fire emission gradually
            var emission = fireSystem.emission;
           
            if (hitCount >= hitsToExtinguish)
            {
                fireSystem.Stop();
                Debug.Log("Fire extinguished!");
            }
    }
}
