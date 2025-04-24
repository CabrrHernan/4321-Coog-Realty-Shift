using UnityEngine;

public class allposcheck : MonoBehaviour
{
    // References to the other blocks as GameObjects
    public GameObject water;
    public GameObject oil;
    public GameObject glass;

    // Materials to assign based on conditions
    public Material allTrueMaterial;
    public Material oneFalseMaterial;

    private Renderer blockRenderer;

    void Start()
    {
        // Get the Renderer component of the current block
        blockRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Access the scripts dynamically using GetComponent
        var waterScript = water.GetComponent<waterposcheck>();
        var oilScript = oil.GetComponent<oilposcheck>();
        var glassScript = glass.GetComponent<glassposcheck>();

        // Check the bool values from the other blocks
        if (waterScript.watercheck && oilScript.oilcheck && glassScript.glasscheck)
        {
            // All bool values are true
            blockRenderer.material = allTrueMaterial;
        }
        else
        {
            // At least one bool value is false
            blockRenderer.material = oneFalseMaterial;
        }
    }
}
