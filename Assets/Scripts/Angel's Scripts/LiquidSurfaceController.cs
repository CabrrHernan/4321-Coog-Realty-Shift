using UnityEngine;

public class LiquidSurfaceController : MonoBehaviour
{
    public Transform beaker; // Assign the beaker object
    public float maxFillHeight = 0.15f; // Maximum Y scale when full
    public float minFillHeight = 0.01f; // Minimum Y scale when nearly empty
    public float fillAmount = 1.0f; // 1 = full, 0 = empty

    void LateUpdate()
    {
        // Always keep the surface flat relative to the world
        transform.up = Vector3.up;

        // Re-center the liquid inside the beaker (optional)
        Vector3 localCenter = Vector3.zero;
        localCenter.y = transform.localScale.y / 2f;
        transform.localPosition = localCenter;

        // Adjust liquid height based on fill amount
        float clampedFill = Mathf.Clamp01(fillAmount);
        float newY = Mathf.Lerp(minFillHeight, maxFillHeight, clampedFill);

        Vector3 scale = transform.localScale;
        scale.y = newY;
        transform.localScale = scale;
    }

    public void SetFillAmount(float amount)
    {
        fillAmount = Mathf.Clamp01(amount);
    }

    public float GetFillAmount()
    {
        return fillAmount;
    }
}
