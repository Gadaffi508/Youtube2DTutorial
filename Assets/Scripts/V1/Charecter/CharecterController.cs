using System.Collections;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    float horizontal,Vertical;
    [SerializeField] private float JumpForce = 200;
    [SerializeField] private int XP;
    [SerializeField] private GameObject UpgradePanel;
    [Header("Upgrade")]
    [SerializeField] private int Health = 100;
    [SerializeField] private int speed = 4;
    [SerializeField] private int Damage;
    [Space]
    [Header("Attack")]
    [SerializeField] private float ARadius;
    [SerializeField] private Transform APos;
    [SerializeField] private LayerMask ELayer;

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

        if (XP % 10 == 0 && XP != 0)
        {
            StartCoroutine(DelayUpgrade());
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

    public void Upgrade(string property)
    {
        Time.timeScale = 1;
        switch (property)
        {
            case "speed":
                speed += 1;
                break;
            case "health":
                Health += 20;
                break;
            case "strong":
                Damage += 10;
                break;
        }
    }

    IEnumerator DelayUpgrade()
    {
        XP = 0;
        yield return new WaitForSeconds(.7f);
        UpgradePanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }


}
