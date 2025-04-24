using UnityEngine;

public class LiquidBeaker : MonoBehaviour
{
    [Header("Settings")]
    public float boilingPoint = 100f;
    public float hotPlateDetectionRadius = 0.15f; // Adjust based on hot plate size
    
    [Header("References")]
    [SerializeField] private HotPlateController hotPlate; // Drag in Inspector
    [SerializeField] private ParticleSystem bubbles;
    [SerializeField] private ParticleSystem steam;
    
    private bool isBoiling;
    private bool isOnHotPlate;
    private Collider hotPlateCollider;

    void Start()
    {
        if (hotPlate != null)
        {
            hotPlateCollider = hotPlate.GetComponent<Collider>();
        }
        else
        {
            hotPlate = FindFirstObjectByType<HotPlateController>();
            if (hotPlate != null) hotPlateCollider = hotPlate.GetComponent<Collider>();
        }
        
        StopEffects();
    }

    void Update()
    {
        if (hotPlate == null) return;
        
        // Check if beaker is on/near the hot plate
        isOnHotPlate = CheckIfOnHotPlate();
        
        float currentTemp = isOnHotPlate ? hotPlate.GetCurrentTemperature() : 22f; // Room temp if not on plate
        
        if (currentTemp >= boilingPoint && !isBoiling)
        {
            StartBoiling();
        }
        else if (currentTemp < boilingPoint && isBoiling)
        {
            StopBoiling();
        }
    }

    bool CheckIfOnHotPlate()
    {
        if (hotPlateCollider == null) return false;

        Vector3 checkPosition = new Vector3(transform.position.x, hotPlateCollider.bounds.center.y + 0.01f, transform.position.z);
        Collider[] hits = Physics.OverlapSphere(checkPosition, hotPlateDetectionRadius);
        foreach (var hit in hits)
        {
            if (hit == hotPlateCollider)
                return true;
        }

        return false;
    }

    void StartBoiling()
    {
        isBoiling = true;
        if (bubbles != null) bubbles.Play();
        if (steam != null) steam.Play();
    }

    void StopBoiling()
    {
        isBoiling = false;
        if (bubbles != null) bubbles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        if (steam != null) steam.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    void StopEffects()
    {
        if (bubbles != null) 
        {
            bubbles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
        if (steam != null) 
        {
            steam.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    void OnDrawGizmosSelected()
    {
    if (hotPlate == null) return;

    // Visualize the check position
    Vector3 checkPosition = new Vector3(
        transform.position.x,
        hotPlate.transform.position.y + 0.01f,
        transform.position.z
    );

    Gizmos.color = Color.cyan;
    Gizmos.DrawWireSphere(checkPosition, hotPlateDetectionRadius);
    }

}