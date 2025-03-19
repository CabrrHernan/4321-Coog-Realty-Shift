using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChemicalMixer : MonoBehaviour
{
    public List<string> addedChemicals = new List<string>(); // Stores all added chemicals
    public GameObject liquidInBeaker; // The visible liquid inside the larger beaker
    public float maxLiquidHeight = 1.5f; // The highest the liquid can go
    public float pourAmount = 0.2f; // How much liquid height increases per pour
    private Vector3 originalLiquidScale; // Stores original scale of liquid

    void Start()
    {
        originalLiquidScale = liquidInBeaker.transform.localScale; // Save original scale
    }

    public void AddChemical(string chemical)
    {
        if (!addedChemicals.Contains(chemical))
        {
            addedChemicals.Add(chemical);
            Debug.Log(chemical + " added to the mix!");

            UpdateLiquidLevel();
        }
    }

    void UpdateLiquidLevel()
    {
        // Ensure we don’t exceed max height
        if (liquidInBeaker.transform.localScale.y < maxLiquidHeight)
        {
            StartCoroutine(SmoothLiquidIncrease());
        }
    }

    IEnumerator SmoothLiquidIncrease()
    {
        float targetHeight = liquidInBeaker.transform.localScale.y + pourAmount;
        targetHeight = Mathf.Min(targetHeight, maxLiquidHeight); // Clamp to max height

        Vector3 targetScale = new Vector3(
            originalLiquidScale.x,
            targetHeight,
            originalLiquidScale.z
        );

        float duration = 0.5f; // Animation duration
        float elapsedTime = 0f;
        Vector3 startScale = liquidInBeaker.transform.localScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            liquidInBeaker.transform.localScale = Vector3.Lerp(startScale, targetScale, elapsedTime / duration);
            yield return null;
        }

        liquidInBeaker.transform.localScale = targetScale; // Ensure final scale is exact
    }
}
