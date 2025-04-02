using UnityEngine;

public class HoseController : MonoBehaviour
{
    public ParticleSystem waterParticles; // Assign in the Inspector

    void Start()
    {
        waterParticles.Stop();
    }

    public void playParticles()
    {
        waterParticles.Play(); // Start the particle effect
    }

    public void stopParticles()
    {
        waterParticles.Stop();
    }
}
