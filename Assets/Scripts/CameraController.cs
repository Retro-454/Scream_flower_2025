using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;       // player to follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public bool lockMovement = false;   // lock camera during waves
    public float lockX = 0f;            // x position to lock at

    void LateUpdate()
    {
        Vector3 targetPos = player.position + offset;

        if (lockMovement)
        {
            // Lock X or Y axis depending on your game
            targetPos.x = lockX;
        }

        Vector3 smoothedPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        transform.position = smoothedPos;
    }
}

