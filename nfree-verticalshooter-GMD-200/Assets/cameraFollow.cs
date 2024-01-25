using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float damping;

    private Vector3 velocity = Vector3.zero;
    private Vector3 roundedPosition = Vector3.zero;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 movePosition = target.position + offset;
        roundedPosition.x = Mathf.Round(movePosition.x / 0.125f) * 0.125f;
        roundedPosition.y = Mathf.Round(movePosition.y / 0.125f) * 0.125f;
        roundedPosition.z = movePosition.z;
        transform.position = Vector3.SmoothDamp(transform.position, roundedPosition, ref velocity, damping);
    }
}
