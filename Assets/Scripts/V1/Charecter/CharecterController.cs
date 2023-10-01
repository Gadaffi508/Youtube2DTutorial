using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharecterController : MonoBehaviour
{
    [SerializeField] private int speed;
    [SerializeField] private float JumpForce = 200;

    Animator anim;
    Rigidbody2D rb;
    float horizontal,Vertical;
    bool jump;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        anim.SetFloat("speed", Mathf.Abs((horizontal + Vertical)));

        if (horizontal > 0) transform.localScale = new Vector3(.7f, .7f, .7f);
        else if (horizontal < 0) transform.localScale = new Vector3(-.7f, .7f, .7f);

        if (jump && Input.GetKeyDown(KeyCode.Space)) Jump();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
    }

    private void Jump()
    {
        anim.SetBool("jump",true);
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


}
