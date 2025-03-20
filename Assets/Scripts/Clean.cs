using UnityEngine;

public class Clean : MonoBehaviour
{
    private bool clean = true;
    private int triggerCount = 0;

    public GameObject targetPrefab;
    public GameObject objectToReplace;
    public GameObject newObject;

    private void OnTriggerEnter(Collider other)
    {
        // Apply to only to this specfic collection of objects
        if (IsChildOfPrefab(other.gameObject))
        {
            triggerCount++;

            Debug.Log($"Object {other.gameObject.name} entered the trigger. Count: {triggerCount}");

            // Deleting what is in contact
            Destroy(other.gameObject);
            if (triggerCount >= 4 && clean == true)
            {
                clean = false;
                Destroy(objectToReplace);
                Instantiate(newObject, transform.position, transform.rotation);
            }
        }
    }

    // Check if the object is a child of the target prefab
    private bool IsChildOfPrefab(GameObject obj)
    {
        return obj.transform.IsChildOf(targetPrefab.transform);
    }
}
