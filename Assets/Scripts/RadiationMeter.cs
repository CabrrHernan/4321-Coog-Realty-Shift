using UnityEngine;
using UnityEngine.UI;

public class RadiationMeter : MonoBehaviour
{
    public Text radiationText;

    public void UpdateMeter(int hitCount)
    {
        radiationText.text = $"Radioactivity: {hitCount:0} µSv";
    }
}
