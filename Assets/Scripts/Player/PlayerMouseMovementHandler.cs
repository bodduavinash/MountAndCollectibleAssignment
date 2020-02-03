using UnityEngine;
using Zenject;

public class PlayerMouseMovementHandler : IResetToDefaults
{
    private float mouseSensitivity = 10.0f;
    private float clampAngle = 45.0f;

    private float rotationX = 0.0f; // rotation around the right/x axis
    private float rotationY = 0.0f; // rotation around the up/y axis

    [Inject] private PlayerMountHandler playerMountHandler;

    public void MouseLook(Transform transform)
    {
        //if the player is mounted with type D, do not use control and return.
        if (playerMountHandler != null && playerMountHandler.isPlayerMountedForType_D)
        {
            ResetToDefaults();
            return;
        }
        //move rotation vector from player gameobject for mouse horizontal direction
        transform.Rotate(0, Input.GetAxis("Mouse X") * mouseSensitivity, 0);
        rotationX -= Input.GetAxis("Mouse Y") * mouseSensitivity;

        //Clamps the vertical angle within the min and max limits (45 degrees)
        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);

        //move rotation vector from maincamera gameobject for mouse vertical direction
        rotationY = Camera.main.transform.localEulerAngles.y;

        Camera.main.transform.localEulerAngles = new Vector3(rotationX, rotationY, 0);
    }

    public void ResetToDefaults()
    {
        Camera.main.transform.localEulerAngles = Vector3.zero;
    }
}
