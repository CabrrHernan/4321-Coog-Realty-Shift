using UnityEngine;

public class Container : MonoBehaviour
{
    public string expectedRockType; // The type of rock this container expects
    public string expectedBin;     // The expected bin ("Small" or "Big")

    public bool CheckRock(Rock rock)
    {
        // Check if the rock matches the expected type and bin
        return rock.rockType == expectedRockType && rock.GetCorrectBin() == expectedBin;
    }
}