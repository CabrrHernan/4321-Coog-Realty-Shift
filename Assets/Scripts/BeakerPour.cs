using UnityEngine;

public class BeakerPour : MonoBehaviour
{
    public GameObject liquid; // Assign the "Beaker Liquid" object
    public float pourAngle = 60f; // Tilt threshold for pouring
    public LayerMask containerLayer; // Detects valid containers
    public Material redMaterial, blueMaterial, greenMaterial, defaultMaterial; // Assign in Inspector
    private bool isPouring = false;

    void Update()
    {
        // Check if the beaker is tilted enough to pour
        if (Vector3.Angle(transform.up, Vector3.up) > pourAngle)
        {
            if (!isPouring)
            {
                isPouring = true;
                StartPouring();
            }
        }
        else
        {
            if (isPouring)
            {
                isPouring = false;
                StopPouring();
            }
        }
    }

    void StartPouring()
    {
        Debug.Log(gameObject.name + " started pouring!");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, containerLayer))
        {
            if (hit.collider.CompareTag("LargerBeaker"))
            {
                string chemicalType = GetChemicalType(); // Determine which chemical this beaker contains
                hit.collider.gameObject.GetComponent<ChemicalMixer>().AddChemical(chemicalType);
                Debug.Log(chemicalType + " poured into " + hit.collider.gameObject.name);
            }
        }
    }

    void StopPouring()
    {
        Debug.Log(gameObject.name + " stopped pouring!");
    }

    string GetChemicalType()
    {
        // Check material and determine chemical type
        Material liquidMaterial = liquid.GetComponent<MeshRenderer>().material;

        if (liquidMaterial == redMaterial)
            return "Red";
        else if (liquidMaterial == blueMaterial)
            return "Blue";
        else if (liquidMaterial == greenMaterial)
            return "Green";
        else
            return "Water"; // Default to water if no special chemical
    }

    public void ChangeLiquidColor(string chemical)
    {
        if (chemical == "Red")
            liquid.GetComponent<MeshRenderer>().material = redMaterial;
        else if (chemical == "Blue")
            liquid.GetComponent<MeshRenderer>().material = blueMaterial;
        else if (chemical == "Green")
            liquid.GetComponent<MeshRenderer>().material = greenMaterial;
        else
            liquid.GetComponent<MeshRenderer>().material = defaultMaterial; // Reset to default water
    }
}
