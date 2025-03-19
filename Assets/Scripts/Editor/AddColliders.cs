using UnityEngine;
using UnityEditor;

public class AddColliders : EditorWindow
{
    [MenuItem("Tools/Add Colliders to Environment")]
    public static void AddCollidersToEnvironment()
    {
        GameObject environmentParent = GameObject.Find("Environment");

        if (environmentParent == null)
        {
            Debug.LogError("No object named 'Environment' found in the Hierarchy!");
            return;
        }

        Transform[] allObjects = environmentParent.GetComponentsInChildren<Transform>();

        int objectsModified = 0;

        foreach (Transform obj in allObjects)
        {
            if (obj.gameObject.GetComponent<Collider>() == null) // Only add if no collider exists
            {
                // Try to determine the best collider type
                if (obj.gameObject.GetComponent<MeshFilter>() != null)
                {
                    obj.gameObject.AddComponent<MeshCollider>().convex = false; // For complex shapes
                }
                else
                {
                    obj.gameObject.AddComponent<BoxCollider>(); // For simple objects
                }

                objectsModified++;
            }
        }

        Debug.Log($"Added colliders to {objectsModified} objects in Environment.");
    }
}
