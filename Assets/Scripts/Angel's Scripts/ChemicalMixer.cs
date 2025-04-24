using UnityEngine;

public class ChemicalMixer : MonoBehaviour
{
    [Header("Liquid Visual")]
    public Transform liquidInBeaker; // Assign the child liquid object

    private float currentLevel = 0f; // 0 = empty, 1 = full
    private Color currentColor = Color.clear;
    private Vector3 initialScale;
    private float initialYPosOffset;

    void Start()
    {
        if (liquidInBeaker != null)
        {
            initialScale = liquidInBeaker.localScale;
            initialYPosOffset = liquidInBeaker.localPosition.y;

            Renderer r = liquidInBeaker.GetComponent<Renderer>();
            if (r != null)
            {
                currentColor = r.material.color;
            }
        }
    }

    public void IncreaseLiquidLevel(float amount, Color addedColor)
    {
        currentLevel = Mathf.Clamp01(currentLevel + amount);
        currentColor = Color.Lerp(currentColor, addedColor, 0.5f);

        if (liquidInBeaker != null)
        {
            // Scale up Y only
            float newY = Mathf.Lerp(0.01f, initialScale.y, currentLevel);
            Vector3 newScale = liquidInBeaker.localScale;
            newScale.y = newY;
            liquidInBeaker.localScale = newScale;

            // Position offset (stay seated if pivot is centered)
            Vector3 newPos = liquidInBeaker.localPosition;
            newPos.y = initialYPosOffset * (newY / initialScale.y);
            liquidInBeaker.localPosition = newPos;

            // Update color
            Renderer r = liquidInBeaker.GetComponent<Renderer>();
            if (r != null)
            {
                r.material.color = currentColor;
            }
        }
    }
}
