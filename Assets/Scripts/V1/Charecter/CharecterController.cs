using System.Collections;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    public float runSpeed = 20.0f;

    float horizontal;
    float vertical;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        rb.velocity =ForceVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            CurrentGame("Finished", "You Lost in Game");
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            CurrentGame("Win", "You King");
        }
    }

    private Vector2 ForceVelocity()
    {
        return new Vector2(rb.velocity.x + 0.1f ,rb.velocity.y + vertical);
    }

    private void CurrentGame(string currentT,string currentW)
    {
        Debug.Log(currentT);
        GameManager.instance.CurrentTextWrite(currentW);
        Time.timeScale = 0;
    }
}
