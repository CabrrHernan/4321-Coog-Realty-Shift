using UnityEngine;

public class HandWashingDetector : MonoBehaviour
{
    public ParticleSystem waterParticles;
    public float requiredWashingTime = 10f;
    private float currentWashingTime = 0f;
    private bool isWashing = false;
    public bool washedHands { get; private set; } = false;

    // Reference to the left and right controller GameObjects
    public GameObject leftHand;
    public GameObject rightHand;

    // Reference to both hand/controller colliders
    public Collider leftHandCollider;
    public Collider rightHandCollider;

    private int handsInWater = 0;

    private void Start()
    {
        // Get colliders from controllers
        if (leftHand != null)
            leftHandCollider = leftHand.GetComponent<Collider>();
        if (rightHand != null)
            rightHandCollider = rightHand.GetComponent<Collider>();

        if (leftHandCollider == null || rightHandCollider == null)
        {
            Debug.LogWarning("Controller colliders not found! Make sure controllers have collider components.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other == leftHandCollider || other == rightHandCollider) && waterParticles.isPlaying)
        {
            handsInWater++;
            if (handsInWater >= 2) // Both hands in water
            {
                isWashing = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == leftHandCollider || other == rightHandCollider)
        {
            handsInWater--;
            if (handsInWater < 2)
            {
                isWashing = false;
                currentWashingTime = 0f; // Reset if hands leave
            }
        }
    }

    private void Update()
    {
        if (isWashing && waterParticles.isPlaying && !washedHands)
        {
            currentWashingTime += Time.deltaTime;
            if (currentWashingTime >= requiredWashingTime)
            {
                washedHands = true;
                Debug.Log("Hands successfully washed!");
            }
        }
    }
}