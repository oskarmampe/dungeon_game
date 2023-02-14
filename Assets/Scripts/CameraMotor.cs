using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public float boundX = 0.15f;
    public float boundY = 0.05f;


    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // This tracks how far the thing we're looking at moved away from the camera.
        float deltaX = lookAt.position.x - transform.position.x;
        // Give some leeway to the camera so it doesn't move too much.
        if (deltaX > boundX || deltaX < -boundX)
        {
            // Check which side the thing we're looking at is on.
            // Give a bound to both sides
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        // This tracks how far the thing we're looking at moved away from the camera.
        float deltaY = lookAt.position.y - transform.position.y;
        // Give some leeway to the camera so it doesn't move too much.
        if(deltaY > boundY || deltaY < -boundY)
        {
            // Check which side the thing we're looking at is on.
            // Give a bound to both sides
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
