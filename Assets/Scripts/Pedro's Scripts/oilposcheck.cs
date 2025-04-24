using UnityEngine;

public class oilposcheck : MonoBehaviour
{
    // Assign the specific cube this contact should detect in the Inspector
    public GameObject targetCube; // The cube this contact should interact with
    public bool oilcheck = false; // Flag to check if the object is in water

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    // Called when this object collides with another object
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected!");
        // Check if the collided object is the assigned target cube
        if (collision.gameObject == targetCube)
        {
            Debug.Log($"{gameObject.name} successfully collided with {targetCube.name}.");
            oilcheck = true; // Set the oilcheck flag to true
            // Perform any additional logic here, such as triggering events or updating game state
        }
        else
        {
            Debug.Log($"{gameObject.name} collided with {collision.gameObject.name}, but it is not the correct target.");
            oilcheck = false; // Set the oilcheck flag to true
        }
    }
}
