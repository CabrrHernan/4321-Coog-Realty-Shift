using UnityEngine;

public class LiquidParticle : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("MixingBeaker"))
        {
            var mixer = other.GetComponent<ChemicalMixer>();
            if (mixer != null)
            {
                mixer.IncreaseLiquidLevel(0.01f, Color.blue); // Replace with actual color if needed
            }
        }
    }
}
