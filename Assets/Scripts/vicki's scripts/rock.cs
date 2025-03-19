using UnityEngine;

public class Rock : MonoBehaviour
{
    public string rockType; // Type of rock (e.g., hematite, magnetite)
    public float weight;   // Weight of the rock
    private Color streakColor; // Color produced on the streak plate
    private string correctBin; // Correct bin for the rock ("5 and under" or "6 and up")

    // Custom colors for streaks
    public static readonly Color Brown = new Color(117f / 255f, 42f / 255f, 23f / 255f);
    public static readonly Color LightBlack = new Color(44f / 255f, 16f / 255f, 10f / 255f);

    void Start()
    {
        // Set the correct bin based on the rock's weight
        correctBin = weight <= 5 ? "Small" : "Big";
        streakColor = rockType == "hematite" ? Brown : LightBlack;
    }

    public Color GetStreakColor()
    {
        return streakColor;
    }

    public string GetCorrectBin()
    {
        return correctBin;
    }

}