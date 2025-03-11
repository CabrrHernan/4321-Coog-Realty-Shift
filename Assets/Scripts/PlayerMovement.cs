using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public float lookSpeed = 2.0f;

    void Update()
    {
        // Player movement with WASD
        float moveX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float moveZ = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(moveX, 0, moveZ);

        // Mouse look
        float rotateX = Input.GetAxis("Mouse X") * lookSpeed;
        transform.Rotate(0, rotateX, 0);
    }
}
