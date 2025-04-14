using UnityEngine;

public static class PeriodicTableLoader
{
    public static Element[] LoadElements()
    {
        TextAsset json = Resources.Load<TextAsset>("PeriodicTableData");
        string wrappedJson = "{\"elements\":" + json.text + "}";
        return JsonUtility.FromJson<ElementList>(wrappedJson).elements;
    }
}
