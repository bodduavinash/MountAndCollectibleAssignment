using UnityEngine;

public class UserInputManager
{
    public float horizontalDirection;
    public float verticalDirection;
    public Vector3 moveDirection;

    private bool inputKeyPressed;

    public bool InputKeyPressed { get => inputKeyPressed; set => inputKeyPressed = value; }

    public bool CheckPlayerInput(Transform transform)
    {
        InputKeyPressed = true;

        if (Input.GetKey(KeyCode.W)) // move front
        {
            verticalDirection = 1.0f;
            moveDirection = transform.TransformDirection(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.S)) // move back
        {
            verticalDirection = -1.0f;
            moveDirection = transform.TransformDirection(-Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)) // move right
        {
            horizontalDirection = 1.0f;
            moveDirection = transform.TransformDirection(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.A)) // move left
        {
            horizontalDirection = -1.0f;
            moveDirection = transform.TransformDirection(Vector3.left);
        }
        else
        {
            verticalDirection = 0;
            horizontalDirection = 0;
            moveDirection = Vector3.zero;
            InputKeyPressed = false;
        }

        return InputKeyPressed;
    }
}