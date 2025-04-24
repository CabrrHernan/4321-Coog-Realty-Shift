using UnityEngine;

public class BeakerPour : MonoBehaviour
{
    [Header("Liquid Settings")]
    public Transform liquid; // Assign the liquid object inside the small beaker
    public float minPourAngle = 20f;
    public float maxPourAngle = 80f;
    public float pourRate = 0.2f;

    [Header("Receiving Beaker")]
    public ChemicalMixer mixingBeaker; // Drag the large beaker's ChemicalMixer script here

    private float currentFill = 1.0f;
    private Vector3 initialLiquidScale;
    private float initialYPosOffset;
    private Color liquidColor;
    private bool isPouring = false;

    void Start()
    {
        if (liquid != null)
        {
            initialLiquidScale = liquid.localScale;
            initialYPosOffset = liquid.localPosition.y;

            Renderer liquidRenderer = liquid.GetComponent<Renderer>();
            if (liquidRenderer != null)
            {
                liquidColor = liquidRenderer.material.color;
            }
        }
    }

    void Update()
    {
        if (liquid == null || currentFill <= 0f) return;

        float tiltAngle = Vector3.Angle(transform.up, Vector3.up);
        float dynamicThreshold = Mathf.Lerp(minPourAngle, maxPourAngle, 1f - currentFill);

        if (tiltAngle > dynamicThreshold)
        {
            if (!isPouring)
            {
                isPouring = true;
            }

            DrainLiquid();
        }
        else
        {
            if (isPouring)
            {
                isPouring = false;
            }
        }
    }

    void DrainLiquid()
    {
        float drainAmount = pourRate * Time.deltaTime;
        currentFill = Mathf.Max(0f, currentFill - drainAmount);

        // Scale the liquid height visually
        float newY = Mathf.Lerp(0.01f, initialLiquidScale.y, currentFill);
        Vector3 newScale = liquid.localScale;
        newScale.y = newY;
        liquid.localScale = newScale;

        Vector3 newPos = liquid.localPosition;
        newPos.y = initialYPosOffset * (newY / initialLiquidScale.y);
        liquid.localPosition = newPos;

        // Transfer to mixing beaker
        if (mixingBeaker != null)
        {
            mixingBeaker.IncreaseLiquidLevel(drainAmount, liquidColor);
        }
    }

    public float GetFillAmount()
    {
        return currentFill;
    }

    public void SetFillAmount(float amount)
    {
        currentFill = Mathf.Clamp01(amount);
    }
}
