using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    float horizontal;
    bool jump = true;
    [SerializeField] private float speed = 4;
    [SerializeField] private float JumpForce = 200;

    [Space]
    [Header("Sword Controller")]
    [SerializeField] private Transform radiusPos;
    [SerializeField] private float radiusValue;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        anim.SetFloat("speed", Mathf.Abs(horizontal));

        if (Input.GetKeyDown(KeyCode.Space) && jump == true)
        {
            rb.AddForce(new Vector3(0, transform.position.y + JumpForce, 0), ForceMode2D.Force);
            anim.SetBool("jump", true);
            jump = false;
        }

        if (horizontal > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jump = true;
            anim.SetBool("jump", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(radiusPos.position, radiusValue);
    }

    public void Attack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(radiusPos.position, radiusValue);

        foreach (Collider2D collision in colliders)
        {
            if (collision.name == "Box")
            {
                collision.GetComponent<BoxManager>().BoxDamage();
            }
        }
    }
}
