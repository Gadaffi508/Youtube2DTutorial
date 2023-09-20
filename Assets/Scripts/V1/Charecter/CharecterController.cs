using UnityEngine;

public class CharecterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    float horizontal,Vertical;
    [SerializeField] private float speed = 4;
    [SerializeField] private float JumpForce = 200;

    [Header("Shoot Options")]
    [SerializeField] private float force;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform attackPos;

    Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        anim.SetFloat("speed", Mathf.Abs((horizontal + Vertical)));

        if (horizontal > 0) transform.localScale = new Vector3(1f, 1f, 1);
        else if (horizontal < 0) transform.localScale = new Vector3(-1f, 1f, 1);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack");
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
        transform.Translate(Vector3.up * Vertical * speed * Time.deltaTime);
    }

    public void Shoot()
    {
        GameObject _arrow = Instantiate(arrow,attackPos.position,Quaternion.identity);
        if (transform.localScale.x == 1) direction = transform.right * force;
        else if(transform.localScale.x == -1) direction = -transform.right * force;
        _arrow.GetComponent<Rigidbody2D>().velocity = direction;
    }
}
