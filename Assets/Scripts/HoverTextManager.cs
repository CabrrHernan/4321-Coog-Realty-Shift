using UnityEngine;
using TMPro;

public class HoverTextManager : MonoBehaviour
{
    public static HoverTextManager instance;
    public TextMeshProUGUI hoverText;
    public Transform cameraTransform; // Assign Main Camera in Inspector
    public float textDistance = 2f; // Distance in front of player
    private string currentText = "No Object"; // Default text

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        hoverText.text = currentText;
    }

    void Update()
    {
        // Ensure the hover text is always visible in front of the player
        transform.position = cameraTransform.position + cameraTransform.forward * textDistance;
        transform.LookAt(cameraTransform);
        transform.Rotate(0, 180, 0); // Flip text to face the player

        // Keep showing the last valid hover text or the default
        if (string.IsNullOrEmpty(hoverText.text))
        {
            hoverText.text = currentText;
        }
    }

    public void ShowText(string text)
    {
        currentText = text;
        hoverText.text = text;
    }

    public void HideText()
    {
        currentText = "No Object";
        hoverText.text = "No Object";
    }
}
