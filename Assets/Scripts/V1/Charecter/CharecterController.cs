using System.Collections;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float JumpForce = 200;
    [SerializeField] private Transform FirePos;

    LineRenderer lineRenderer;
    Animator anim;
    Rigidbody2D rb;
    float horizontal, Vertical;
    bool jump;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        anim.SetFloat("speed", Mathf.Abs((horizontal + Vertical)));

        if (horizontal > 0) transform.localScale = new Vector3(.7f, .7f, .7f);
        else if (horizontal < 0) transform.localScale = new Vector3(-.7f, .7f, .7f);

        if (jump && Input.GetKeyDown(KeyCode.Space)) Jump();

        if (Input.GetMouseButtonDown(0)) anim.SetTrigger("Shoot");
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
    }

    private void Jump()
    {
        anim.SetBool("jump", true);
        jump = false;
        rb.AddForce(new Vector2(0f, JumpForce));
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("ground"))
        {
            jump = true;
            anim.SetBool("jump", false);
        }
    }

    public void ShootLineRenderer()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(FirePos.position, FirePos.right);

        lineRenderer.enabled = true;

        if (hitInfo)
        {
            lineRenderer.SetPosition(0, FirePos.position);
            lineRenderer.SetPosition(1, hitInfo.point);
        }
        else
        {
            lineRenderer.SetPosition(0, FirePos.position);

            lineRenderer.SetPosition(1, FirePos.position + FirePos.right * 100);
        }
        StartCoroutine(CloseLine());
    }

    IEnumerator CloseLine()
    {
        yield return new WaitForSeconds(.5f);
        lineRenderer.enabled = false;
    }

}
