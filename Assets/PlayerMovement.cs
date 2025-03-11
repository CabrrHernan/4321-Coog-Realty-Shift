using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    private Vector2 movementInput;

    void Update()
    {
        // Get movement input from the new Input System
        movementInput = Keyboard.current != null 
            ? new Vector2(Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0,
                          Keyboard.current.sKey.isPressed ? -1 : Keyboard.current.wKey.isPressed ? 1 : 0)
            : Vector2.zero;

        Vector3 move = transform.right * movementInput.x + transform.forward * movementInput.y;
        controller.Move(move * speed * Time.deltaTime);
    }
}
