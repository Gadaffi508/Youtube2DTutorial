using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateCmaeraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 5f; 
    public Vector3 offset;

    private void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
