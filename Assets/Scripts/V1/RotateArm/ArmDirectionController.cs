using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDirectionController : MonoBehaviour
{
    public float OverlapRadius;
    public Transform nearestEnemy;
    private int enemyLayer;
    public Transform rotateFire;
    public GameObject Bullet;
    public Transform FirePos;
    public Transform FireArea;
    public Transform BulletRotation;
    public float nextFireTimer;
    public float FireTime;
    public GameObject Gun;

    private void Start()
    {
        enemyLayer = LayerMask.NameToLayer("Enemy");
    }

    private void Update()
    {
        Collider2D[] hitColiiders = Physics2D.OverlapCircleAll(FireArea.position,OverlapRadius,1 << enemyLayer);
        float minimumDistance = Mathf.Infinity;
        Transform closestEnemy = null;

        foreach (Collider2D collider in hitColiiders)
        {
            float distance = Vector3.Distance(transform.position, collider.transform.position);
            if (distance < minimumDistance)
            {
                minimumDistance = distance;
                closestEnemy = collider.transform;
            }
        }

        nearestEnemy = closestEnemy;

        nextFireTimer += Time.deltaTime;
        if (nearestEnemy != null)
        {
            Transform enemy = nearestEnemy.GetComponent<Transform>();
            Facing(enemy);

            if (nextFireTimer >= FireTime)
            {
                ProjectTileFire();
                nextFireTimer = 0;
            }
        }
    }

    public void ProjectTileFire()
    {
        GameObject bullet = Instantiate(Bullet, BulletRotation.position,Quaternion.identity);

        if (GetComponentInParent<CharecterController>().Facing == false)
        {
            bullet.GetComponent<ProjectTile>().facingValue = 1;
        }
        else
        {
            bullet.GetComponent<ProjectTile>().facingValue = -1;
        }
    }

    public void Facing(Transform enemy)
    {
        Vector3 barrelPoisiton = rotateFire.transform.position;
        Vector3 targetDirection = enemy.position - barrelPoisiton;
        float targetAngel = Mathf.Atan2(targetDirection.y,targetDirection.x) * Mathf.Rad2Deg;

        rotateFire.rotation = Quaternion.Euler(0f,0f,targetAngel);

        if (GetComponentInParent<CharecterController>().Facing == false)
        {
            transform.localScale = new Vector2(1f,1f);
            Gun.transform.localScale = new Vector2(1f,1f);
        }
        else
        {
            transform.localScale = new Vector2(-1f, 1f);
            Gun.transform.localScale = new Vector2(1f, -1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(FireArea.position,OverlapRadius);
    }

}
