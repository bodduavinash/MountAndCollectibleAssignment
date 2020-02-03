using UnityEngine;
using Zenject;

public class HoverCraftMovementHandler : IResetToDefaults
{
    private GameObject hoverCarModel;
    private Transform raycastPoint;
    private float hoverHeight = 1.0f;
    private float speed = 20.0f;
    private float terrainHeight;
    private float rotationAmount;
    private RaycastHit hit;
    private Vector3 pos;
    private Vector3 forwardDirection;

    [Inject] private PlayerMountHandler playerMountHandler;

    private HoverCraftMovementHandler()
    {
        ResetToDefaults();
    }

    public void ResetToDefaults()
    {
        Camera.main.transform.localEulerAngles = Vector3.zero;
    }

    public void UpdateHoverCraftMovement(Transform transform)
    {
        //return if the player is not mounted with type D.
        if(playerMountHandler == null || !playerMountHandler.isPlayerMountedForType_D)
        {
            return;    
        }

        hoverCarModel = transform.gameObject;
        raycastPoint = transform;

        // Keep at specific height above terrain
        pos = transform.position;
        float terrainHeight = Terrain.activeTerrain.SampleHeight(pos);
        transform.position = new Vector3(pos.x,
                                         terrainHeight + hoverHeight,
                                         pos.z);

        // Rotate to align with terrain
        Physics.Raycast(raycastPoint.position, Vector3.down, out hit);
        transform.up -= (transform.up - hit.normal) * 0.1f;

        // Rotate with input
        rotationAmount = Input.GetAxis("Horizontal") * 120.0f;
        rotationAmount *= Time.deltaTime;
        hoverCarModel.transform.Rotate(0.0f, rotationAmount, 0.0f);

        // Move forward
        forwardDirection = hoverCarModel.transform.forward * Input.GetAxis("Vertical");
        transform.position += forwardDirection * Time.deltaTime * speed;
    }
}