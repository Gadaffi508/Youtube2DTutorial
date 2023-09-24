using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharecterController : MonoBehaviour
{
    Animator anim;
    float horizontal,Vertical;
    [SerializeField] private float JumpForce = 200;
    [SerializeField] private int XP;
    [SerializeField] private GameObject UpgradePanel;
    [Header("Upgrade")]
    [SerializeField] private int Health;
    [SerializeField] private int speed;
    [SerializeField] private int Damage;
    [Space]
    [Header("Attack")]
    [SerializeField] private float ARadius;
    [SerializeField] private Transform APos;
    [SerializeField] private LayerMask ALayer;

    private void Start()
    {
        anim = GetComponent<Animator>();

        string filePath = Application.dataPath + "/CharecterDataFile.json";

        if (File.Exists(filePath))
        {
            LoadData();
        }
        else
        {
            SaveData();
        }
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
        data.Damage = Damage;

        string json = JsonUtility.ToJson(data,true);
        File.WriteAllText(Application.dataPath + "/CharecterDataFile.json",json);
    }

    public void LoadData()
    {
        string json = File.ReadAllText(Application.dataPath + "/CharecterDataFile.json");
        CharecterData data = JsonUtility.FromJson<CharecterData>(json);

        Health = data.Health;
        Damage = data.Damage;
        speed = data.speed;
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

        SaveData();

    }

    IEnumerator UpgradeD()
    {
        XP = 0;
        yield return new WaitForSeconds(.7f);
        UpgradePanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "NextScene")
        {
            SceneManager.LoadScene(1);
        }
    }


}
