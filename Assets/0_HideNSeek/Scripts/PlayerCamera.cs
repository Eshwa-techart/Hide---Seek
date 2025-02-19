using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Target;
    public Transform camTransform;
    public Vector3 Offset;
    public float SmoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    bool isSetTarget;
    private void Start()
    {
        Invoke(nameof(SetTarget), 0.2f);
    }
    void SetTarget()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        isSetTarget = true;
    }
    private void FixedUpdate()
    {
        if (isSetTarget)
        {
            
            Vector3 targetPosition = Target.position + Offset;
            camTransform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, SmoothTime);

           
            transform.LookAt(Target);
        }
    }
}