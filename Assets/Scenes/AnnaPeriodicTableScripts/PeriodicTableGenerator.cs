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

            // ?? This declaration must be inside this foreach loop
            TextMeshPro[] textComponents = tile.GetComponentsInChildren<TextMeshPro>();

            foreach (TextMeshPro tmp in textComponents)
            {
                Debug.Log($"Found TMP: {tmp.name}");

                if (tmp.name.Contains("Symbol"))
                {
                    tmp.text = element.symbol;
                    Debug.Log($"Assigned symbol: {element.symbol} to {tmp.name}");
                }
                else if (tmp.name.Contains("Number"))  // or exact: tmp.name == "ElementNumber"
                {
                    string atomicStr = element.atomicNumber.ToString();
                    tmp.text = atomicStr;
                    Debug.Log($"Assigned atomic number: {atomicStr} to {tmp.name}");
                }
            }

            // Optional color by group
            Renderer rend = tile.GetComponent<Renderer>();
            if (rend != null)
                rend.material.color = GroupToColor(element.group);
        }
    }

    Color GroupToColor(string group)
    {
        switch (group)
        {
            case "Lanthanides": return new Color(0.5f, 0.7f, 1f);
            case "Alkali Metals": return Color.red;
            case "Noble Gases": return Color.cyan;
            case "Metalloids": return Color.green;
            // Outside Cases
            default: return Color.gray;
        }
    }
}
