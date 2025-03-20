using UnityEngine;

public class ChemicalMixer : MonoBehaviour
{
    public Transform liquidInBeaker; // The liquid object inside the mixing beaker
    public float maxLiquidHeight = 1.5f;
    private Vector3 originalLiquidScale;
    private Color currentLiquidColor = Color.clear; // Start as empty
    private int liquidCount = 0; // Tracks how many liquids were added

    void Start()
    {
        if (liquidInBeaker != null)
            originalLiquidScale = liquidInBeaker.localScale;

        // Set initial color (clear/empty)
        UpdateLiquidColor();
    }

    public void IncreaseLiquidLevel(float amount, Color newLiquidColor)
    {
        if (liquidInBeaker.localScale.y < maxLiquidHeight)
        {
            liquidInBeaker.localScale += new Vector3(0, amount, 0);

            // Blend colors using an average mixing approach
            BlendNewColor(newLiquidColor);
        }
    }

    private void BlendNewColor(Color newColor)
    {
        if (liquidCount == 0)
        {
            currentLiquidColor = newColor; // First liquid sets the initial color
        }
        else
        {
            // Blend current color with new color (weighted average)
            currentLiquidColor = Color.Lerp(currentLiquidColor, newColor, 0.5f);
        }

        liquidCount++;

        UpdateLiquidColor();
    }

    private void UpdateLiquidColor()
    {
        Renderer liquidRenderer = liquidInBeaker.GetComponent<Renderer>();
        if (liquidRenderer != null)
        {
            liquidRenderer.material.color = currentLiquidColor;
        }
    }
}
