using System.Collections;
using System.IO;
using UnityEngine;

public class CharecterController : MonoBehaviour
{
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
    [SerializeField] private LayerMask ALayer;

    private void Start()
    {
        //SaveData();

        anim = GetComponent<Animator>();

        //LoadData();
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
            StartCoroutine(UpgradeD());
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * horizontal * speed * Time.deltaTime);
    }

    public void Shoot()
    {
        Collider2D[] enemyC = Physics2D.OverlapCircleAll(APos.position,ARadius, ALayer);
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

    public void SaveData()
    {
        CharecterData data = new CharecterData();
        data.Health = Health;
        data.speed = speed;
        data.damage = Damage;

        string json = JsonUtility.ToJson(data,true);
        File.WriteAllText(Application.dataPath + "/CharecterDataFile.json",json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/CharecterDataFile.json");
        CharecterData data = JsonUtility.FromJson<CharecterData>(json);

        Health = data.Health;
        speed = data.speed;
        Damage = data.damage;
    }

    public void Upgrade(string UpgradeLevel)
    {
        Time.timeScale = 1;

        switch (UpgradeLevel)
        {
            case "speed":
                speed += 1;
                break;
            case "health":
                Health += 10;
                break;
            case "damage":
                Damage += 5;
                break;
        }
    }

    IEnumerator UpgradeD()
    {
        XP = 0;
        yield return new WaitForSeconds(.7f);
        UpgradePanel.SetActive(true);
        Time.timeScale = 0;
    }


}
