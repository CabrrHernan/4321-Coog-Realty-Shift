using UnityEngine;
using UnityEditor;

public class FindMissingScripts : EditorWindow
{
    [MenuItem("Tools/Find Missing Scripts in Scene")]
    public static void FindMissingScriptsInScene()
    {
        GameObject[] objects = GameObject.FindObjectsByType<GameObject>(FindObjectsSortMode.None);

        foreach (GameObject obj in objects)
        {
            Component[] components = obj.GetComponents<Component>();

            foreach (Component c in components)
            {
                if (c == null)
                {
                    Debug.LogWarning($"Missing script found on {obj.name}", obj);
                }
            }
        }
    }
}
