using UnityEngine;

public class CharecterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    float horizontal,Vertical;
    [SerializeField] private float speed = 4;
    [SerializeField] private float JumpForce = 200;
    [SerializeField] private int XP;

    [Header("Attack")]
    [SerializeField] private float ARadius;
    [SerializeField] private Transform APos;
    [SerializeField] private LayerMask ELayer;
    [SerializeField] private int Damage;

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

        if (horizontal > 0) transform.localScale = new Vector3(-1f, 1f, 1);
        else if (horizontal < 0) transform.localScale = new Vector3(1f, 1f, 1);

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
    }

    public void Shoot()
    {
        Collider2D[] enemyC = Physics2D.OverlapCircleAll(APos.position,ARadius, ELayer,float.MinValue,float.MaxValue);
        foreach (var enemy in enemyC)
        {
            enemy.GetComponent<EnemyC>().TakeDamage(Damage);
            XP += 5;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(APos.position,ARadius);
    }
}
