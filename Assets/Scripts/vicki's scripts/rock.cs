using UnityEngine;

public class Rock : MonoBehaviour
{
    public string rockType; // Type of rock (e.g., hematite, magnetite)
    public float weight;   // Weight of the rock
    private Color streakColor; // Color produced on the streak plate
    private string correctBin; // Correct bin for the rock ("5 and under" or "6 and up")

    // Custom colors for streaks
    public static readonly Color Brown = new Color(0.459f, 0.165f, 0.090f);
    public static readonly Color LightBlack = new Color(0.173f, 0.063f, 0.039f);

    void Start()
    {
        // Set the correct bin based on the rock's weight
        correctBin = weight <= 5 ? "Small" : "Big";
    }

    public Color GetStreakColor()
    {
        return streakColor;
    }

    public string GetCorrectBin()
    {
        return correctBin;
    }

    // This function will be used to set the streak color for a group of tagged rocks
    public static void SetStreakColor(string tag, Color newColor)
    {
        // Find all rocks with the specified tag
        GameObject[] rocks = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject rock in rocks)
        {
            // Check if the rock has a Rock component
            Rock rockProps = rock.GetComponent<Rock>();
            if (rockProps != null)
            {
                // Set the streak color for each rock in the group
                rockProps.streakColor = newColor;
            }
        }
    }

}