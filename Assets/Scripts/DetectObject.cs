using UnityEngine;

public class DetectObject : MonoBehaviour
{

    public GameObject targetPrefab;
    public ParticleSystem particles;

    // This function is called when another object enters the trigger collider
    private void OnTriggerEnter(Collider other)
    {
        if (targetPrefab == null)
        {
            targetPrefab = GameObject.Find("brokenGlass");
        }

        if (IsChildOfPrefab(other.gameObject))
        {
            // Counts number of broken pieces thrown in
            particles.Play();

            // Delete the object
            Destroy(other.gameObject);
        }
    }

    // Check if the object is a child of the target prefab
    private bool IsChildOfPrefab(GameObject obj)
    {
        return obj.transform.IsChildOf(targetPrefab.transform);
    }
}
