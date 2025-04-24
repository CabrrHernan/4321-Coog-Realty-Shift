using UnityEngine;
using TMPro;

public class HotPlateController : MonoBehaviour
{
    [Header("Temperature Presets")]
    [SerializeField] private float[] temperaturePresets = 
        { 22f, 50f, 85f, 105f, 160f, 192f, 230f }; // Index 0 = Off, 1-6 = Settings 0-5

    [Header("References")]
    [SerializeField] private KnobRotation knob;
    [SerializeField] private TextMeshProUGUI temperatureDisplay;
    
    private float currentTemp;
    private float targetTemp;
    
    void OnEnable()
    {
        knob.onLevelChanged.AddListener(HandleLevelChange);
        HandleLevelChange(knob.CurrentLevel); // Initialize temperature
    }
    
    void OnDisable()
    {
        knob.onLevelChanged.RemoveListener(HandleLevelChange);
    }
    
    void Update()
    {
        UpdateTemperature();
        UpdateDisplay();
    }
    
    private void HandleLevelChange(int newLevel)
    {
        targetTemp = temperaturePresets[newLevel];
    }
    
    private void UpdateTemperature()
    {
        currentTemp = Mathf.Lerp(currentTemp, targetTemp, Time.deltaTime * 0.5f);
    }
    
    private void UpdateDisplay()
    {
        if (temperatureDisplay != null)
        {
            // Format with ° symbol and alignment
            temperatureDisplay.text = 
                $"Heat setting: {GetSettingName(knob.CurrentLevel)}\n" +
                $"Temp: {currentTemp:F1}°C";
        }
    }
    
    private string GetSettingName(int level)
    {
        return level == 0 ? "Off" : $"{level-1}";
    }
    
    public float GetCurrentTemperature() => currentTemp;
}