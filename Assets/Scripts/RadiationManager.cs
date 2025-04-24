using UnityEngine;

public class RadiationManager : MonoBehaviour
{
    public RadiationMeter radiationMeter;
    public float updateInterval = 3f;

    private float timer = 0f;
    private int accumulatedHits = 0;

    public void AddHits(int count)
    {
        accumulatedHits += count;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= updateInterval)
        {
            radiationMeter.UpdateMeter(accumulatedHits);
            accumulatedHits = 0;
            timer = 0f;
        }
    }
}
