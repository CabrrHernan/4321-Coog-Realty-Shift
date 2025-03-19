using UnityEngine;
using UnityEngine.UI;

public class Scale : MonoBehaviour
{
    public Text weightText;
    private float currentWeight;
    void Start()
    {
        weightText.text = "0.0 grams";
    }

    void OnTriggerEnter(Collider other)
    {
        Rock rock = other.GetComponent<Rock>();
        if (rock != null)
        {
            currentWeight = rock.weight;
            weightText.text = currentWeight.ToString("F2") + " grams";
        }
    }

    void OnTriggerExit(Collider other)
    {
        weightText.text = "0.0 grams";
    }
}