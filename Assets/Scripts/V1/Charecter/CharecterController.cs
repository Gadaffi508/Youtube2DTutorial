using System.Collections;
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

    [Space]
    [Header("Dash property")]
    [SerializeField] private float dashSpeed = 25;
    [Range(0, 1)]
    [SerializeField] private float dashDuration = 0.2f;
    private bool isDashing = false;
    [SerializeField] private GameObject particleDash;

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
            jump = false;
        }

        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1, 1);
            Facing = false;
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            Facing = true;
        }

        if (Input.GetKeyDown(KeyCode.F) && !isDashing)
        {
            StartCoroutine(Dash());
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("click",true);
        }

        if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("click", false);
        }

    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            jump = true;
        }
    }

    IEnumerator Dash()
    {
        isDashing = true;

        Vector2 oldVelocity = rb.velocity;

        GameObject particle = Instantiate(particleDash,transform.position,Quaternion.identity);
        Destroy(particle,2f);

        rb.velocity = new Vector2(dashSpeed * transform.localScale.x, rb.velocity.y);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = oldVelocity;

        isDashing = false;
    }
}
