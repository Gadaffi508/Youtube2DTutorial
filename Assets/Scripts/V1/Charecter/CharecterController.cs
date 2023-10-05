using System.Collections;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    public float runSpeed = 20.0f;
    public float rotationSpeed = 5.0f; // Dönme hýzý

    float horizontal;
    float vertical;
    float moveLimiter = 0.7f;
    float targetZ;

    Rigidbody2D body;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        anim.SetFloat("speed", body.velocity.magnitude);

        RotateC();
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0) // Check for diagonal movement
        {
            // limit movement speed diagonally, so you move at 70% speed
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }

    public void RotateC()
    {
        if (vertical > 0 && horizontal != 0) targetZ = 180;
        if (vertical < 0 && horizontal != 0) targetZ = 0;
        else if (vertical > 0) targetZ = 180;
        else if (vertical < 0) targetZ = 0;
        else if (horizontal > 0) targetZ = 90;
        else if (horizontal < 0) targetZ = -90;
        float currentZ = transform.rotation.eulerAngles.z;
        float newZ = Mathf.LerpAngle(currentZ, targetZ, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, 0, newZ);
    }
}
