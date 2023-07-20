using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    float horizontal;
    bool jump = true;
    public bool Facing;
    [SerializeField] private float speed = 4;
    [SerializeField] private float JumpForce = 200;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && jump == true)
        {
            rb.AddForce(new Vector3(0, transform.position.y + JumpForce, 0), ForceMode2D.Force);
            anim.SetBool("Jump",true);
            jump = false;
        }

        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1,1);
            Facing = false;
        }
        else if(horizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            Facing = true;
        }

        anim.SetFloat("Run", Mathf.Abs(horizontal));
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            anim.SetBool("Jump", false);
            jump = true;
        }
    }
}
