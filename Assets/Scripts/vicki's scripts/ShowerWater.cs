using UnityEngine;

public class ShowerWater : MonoBehaviour
{
    public ParticleSystem particles;
    public Collider targetCollider; 

    private void OnTriggerEnter(Collider other)
    {
        if (other == targetCollider)
        {
            particles.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == targetCollider)
        {
            particles.Stop();
        }
    }
}
