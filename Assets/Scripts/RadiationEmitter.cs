using UnityEngine;

public class RadiationEmitter : MonoBehaviour
{
    public int raysPerFrame = 10;
    public float rayRange = 10f;
    public RadiationManager radiationManager;

    void Update()
    {
        int frameHits = 0;

        for (int i = 0; i < raysPerFrame; i++)
        {
            Vector3 direction = Random.onUnitSphere;
            Ray ray = new Ray(transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, rayRange))
            {
                if (hit.collider.CompareTag("Wand"))
                {
                    frameHits++;
                }
            }

            Debug.DrawRay(ray.origin, ray.direction * rayRange, Color.green, 0.1f);
        }

        radiationManager.AddHits(frameHits);
    }
}
