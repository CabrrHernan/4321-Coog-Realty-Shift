using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class RenameWallsAndRoofs : EditorWindow
{
    [MenuItem("Tools/Rename Walls and Roofs")]
    public static void RenameObjects()
    {
        // Find the parent objects
        GameObject wallsParent = GameObject.Find("Walls");
        GameObject roofsParent = GameObject.Find("Roofs");

        if (wallsParent != null)
        {
            RenameChildren(wallsParent, "Wall");
        }
        else
        {
            Debug.LogWarning("No object named 'Walls' found in the Hierarchy!");
        }

        if (roofsParent != null)
        {
            RenameChildren(roofsParent, "Roof");
        }
        else
        {
            Debug.LogWarning("No object named 'Roofs' found in the Hierarchy!");
        }

        Debug.Log("Renaming complete!");
    }

    private static void RenameChildren(GameObject parent, string baseName)
    {
        int count = 0;
        foreach (Transform child in parent.transform)
        {
            if (child != null)
            {
                child.name = count == 0 ? baseName : $"{baseName} ({count})";
                count++;
            }
        }
    }
}
