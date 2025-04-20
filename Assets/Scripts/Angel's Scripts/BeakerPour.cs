using UnityEngine;
using System.Collections;

public class BeakerPour : MonoBehaviour
{
    public Transform liquid; // Assign the Beaker Liquid (inside the small beaker)
    public ParticleSystem pourEffect; // Assign a single liquid particle effect prefab
    public float pourAngleThreshold = 60f; // Angle required to start pouring
    public Transform mixingBeaker; // Assign the target beaker (if needed for position check)
    private bool isPouring = false;
    private Vector3 initialLiquidScale;
    private Color liquidColor;

    private ParticleSystem.MainModule pourMain;

    void Start()
    {
        if (liquid != null)
        {
            initialLiquidScale = liquid.localScale;

            // Get the color from the beaker's internal liquid material
            Renderer liquidRenderer = liquid.GetComponent<Renderer>();
            if (liquidRenderer != null)
            {
                liquidColor = liquidRenderer.material.color;
            }
        }

        if (pourEffect != null)
        {
            pourMain = pourEffect.main;
            pourMain.startColor = liquidColor;
            pourEffect.Stop();
        }
    }

    void Update()
    {
        if (IsTilted() && IsAboveMixingBeaker() && HasLiquid())
        {
            if (!isPouring)
            {
                StartPouring();
            }

            DrainLiquid();
        }
        else
        {
            if (isPouring)
            {
                StopPouring();
            }
        }
    }

    private void StartPouring()
    {
        isPouring = true;

        if (pourEffect != null)
        {
            // Position the pourEffect at the lowest rim point
            pourEffect.transform.position = FindLowestRimPoint();
            pourEffect.Play();
        }
    }

    private void StopPouring()
    {
        isPouring = false;

        if (pourEffect != null && pourEffect.isPlaying)
        {
            pourEffect.Stop();
        }
    }

    private void DrainLiquid()
    {
        if (liquid != null && liquid.localScale.y > 0.05f)
        {
            // Shrink the visual liquid
            liquid.localScale -= new Vector3(0, 0.2f * Time.deltaTime, 0);
        }
    }

    private bool HasLiquid()
    {
        return liquid != null && liquid.localScale.y > 0.05f;
    }

    private bool IsTilted()
    {
        float currentAngle = Vector3.Angle(transform.up, Vector3.up);
        return currentAngle > pourAngleThreshold;
    }

    private bool IsAboveMixingBeaker()
    {
        if (mixingBeaker == null) return true; // Optional: allow pouring anywhere
        float heightDiff = transform.position.y - mixingBeaker.position.y;
        return heightDiff > 0.2f;
    }

    private Vector3 FindLowestRimPoint()
    {
        Vector3 lowestPoint = transform.position;
        float lowestY = float.MaxValue;

        foreach (Transform child in transform)
        {
            if (child.name.ToLower().Contains("rim"))
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
