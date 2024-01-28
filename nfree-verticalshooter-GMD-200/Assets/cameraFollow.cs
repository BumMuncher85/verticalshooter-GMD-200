using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;
    public float rightBound = 2.5f;
    public float leftBound = -1.5f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 roundedPosition = Vector3.zero;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        roundedPosition.x = Mathf.Round(movePosition.x / 0.125f) * 0.125f;
        roundedPosition.y = Mathf.Round(movePosition.y / 0.125f) * 0.125f;
        roundedPosition.z = movePosition.z;
        if(roundedPosition.x > rightBound) { roundedPosition.x = rightBound; }
        if(roundedPosition.x < leftBound) { roundedPosition.x = leftBound;}
        transform.position = Vector3.SmoothDamp(transform.position, roundedPosition, ref velocity, damping);
    }
}
