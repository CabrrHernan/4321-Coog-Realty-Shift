using UnityEngine;
using TMPro;

public class PeriodicTableGenerator : MonoBehaviour
{
    public GameObject elementPrefab;
    public float spacing = 0.25f;
    void Start()
    {
        Element[] elements = PeriodicTableLoader.LoadElements();

        foreach (Element element in elements)
        {
            Vector3 position = new Vector3((element.column - 1) * spacing, -(element.row - 1) * spacing, 0);
            GameObject tile = Instantiate(elementPrefab, position, Quaternion.identity, transform);
            tile.name = element.name;

            TextMeshPro[] textComponents = tile.GetComponentsInChildren<TextMeshPro>();

            foreach (TextMeshPro tmp in textComponents)
            {
                Debug.Log($"Found TMP: {tmp.name}");

                if (tmp.name.Contains("Symbol"))
                {
                    tmp.text = element.symbol;
                    Debug.Log($"Assigned symbol: {element.symbol} to {tmp.name}");
                }
                else if (tmp.name.Contains("Number")) 
                {
                    string atomicStr = element.atomicNumber.ToString();
                    tmp.text = atomicStr;
                    Debug.Log($"Assigned atomic number: {atomicStr} to {tmp.name}");
                }
                else if (tmp.name.Contains("Name"))
                {
                    tmp.text = element.name;
                    Debug.Log($"Assigned name: {element.name} to {tmp.name}");
                }
                
                else if (tmp.name.Contains("Mass"))
                {
                    tmp.text = element.atomicMass.ToString("F3"); 
                    Debug.Log($"Assigned mass: {element.atomicMass} to {tmp.name}");
                }

            }

            Renderer rend = tile.GetComponent<Renderer>();
            if (rend != null)
                rend.material.color = GroupToColor(element.group);
        }
    }

    Color GroupToColor(string group)
    {
        switch (group)
        {
            case "Alkali Metal": return new Color(1.0f, 0.502f, 0.502f); // peach
            case "Alkaline Earth Metal": return new Color(1.0f, 0.6f, 0.4f); // orange
            case "Transition Metal": return new Color(0.902f, 0.902f, 0.0f); // yellow
            case "Lanthanides": return new Color(0.702f, 1.0f, 0.702f); // light green
            case "Actinides": return new Color(0.6f, 0.8f, 1.0f); // light blue
            case "Metalloid": return new Color(0.624f, 0.875f, 0.749f); // darker light green
            case "Post-transition Metal": return new Color(1.0f, 0.8f, 0.0f); // darker yellow
            case "Nonmetal": return new Color(0.702f, 0.702f, 1.0f); // purple
            case "Halogen": return new Color(1.0f, 0.702f, 1.0f); // pink
            case "Noble Gas": return new Color(0.502f, 0.667f, 1.0f); // blue
            case "Unknown Properties": return new Color(0.702f, 0.702f, 0.8f); // purple grey
            // Outside Cases
            default: return Color.gray;
        }
    }
}
