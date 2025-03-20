using UnityEngine;
using System.Collections;

public class BeakerPour : MonoBehaviour
{
    public Transform liquid; // Assign the Beaker Liquid (inside the small beaker)
    public ParticleSystem pourEffect; // Assign a single liquid particle effect
    public float pourAngleThreshold = 60f; // Angle required to start pouring
    public float pourRate = 0.2f; // How fast the liquid moves out
    public Transform mixingBeaker; // The larger beaker
    private bool isPouring = false;
    private Vector3 initialLiquidScale;
    private Color liquidColor; // Stores the liquid's color

    void Start()
    {
        if (liquid != null)
        {
            initialLiquidScale = liquid.localScale;

            // Try to get the color from the Beaker Liquid
            Renderer liquidRenderer = liquid.GetComponent<Renderer>();
            if (liquidRenderer != null)
            {
                liquidColor = liquidRenderer.material.color;
            }
        }

        if (pourEffect != null)
        {
            pourEffect.Stop(); // Make sure the effect starts off
            UpdatePourEffectColor(); // Set the correct color
        }
    }

    void Update()
    {
        if (IsTilted() && IsAboveMixingBeaker())
        {
            if (!isPouring)
            {
                StartCoroutine(PourLiquid());
            }
        }
        else
        {
            StopPourEffect();
        }
    }

    private bool IsTilted()
    {
        float currentAngle = Vector3.Angle(transform.up, Vector3.up);
        return currentAngle > pourAngleThreshold;
    }

    private bool IsAboveMixingBeaker()
    {
        if (mixingBeaker == null) return false;

        float heightDifference = transform.position.y - mixingBeaker.position.y;
        return heightDifference > 0.2f; // Ensures it's high enough above the mixing beaker
    }

    IEnumerator PourLiquid()
    {
        isPouring = true;

        while (IsTilted() && IsAboveMixingBeaker() && liquid.localScale.y > 0.1f)
        {
            // Reduce liquid level in small beaker
            liquid.localScale -= new Vector3(0, pourRate * Time.deltaTime, 0);

            // Send color data along with liquid amount
            Color pouringColor = liquid.GetComponent<Renderer>().material.color;
            mixingBeaker.GetComponent<ChemicalMixer>().IncreaseLiquidLevel(pourRate * Time.deltaTime, pouringColor);

            UpdatePourEffect();

            yield return null;
        }

        StopPourEffect();
        isPouring = false;
    }


    void UpdatePourEffect()
    {
        if (pourEffect != null)
        {
            // Find the lowest point on the rim
            Vector3 lowestPoint = FindLowestRimPoint();
            pourEffect.transform.position = lowestPoint;

            if (!pourEffect.isPlaying)
                pourEffect.Play();
        }
    }

    void StopPourEffect()
    {
        if (pourEffect != null && pourEffect.isPlaying)
            pourEffect.Stop();
    }

    void UpdatePourEffectColor()
    {
        if (pourEffect != null)
        {
            var main = pourEffect.main;
            main.startColor = liquidColor; // Update particle effect color dynamically
        }
    }

    Vector3 FindLowestRimPoint()
    {
        // Cast rays downward from multiple points around the beaker rim to find the lowest point
        Vector3 lowestPoint = transform.position;
        float lowestY = float.MaxValue;

        foreach (Transform child in transform)
        {
            if (child.name.Contains("Rim")) // Ensure the child is a part of the rim
            {
                if (child.position.y < lowestY)
                {
                    lowestY = child.position.y;
                    lowestPoint = child.position;
                }
            }
        }
        return lowestPoint;
    }
}
