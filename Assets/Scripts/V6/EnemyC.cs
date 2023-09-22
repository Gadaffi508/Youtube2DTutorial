using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour
{
    [SerializeField] private int Health;
    Animator anim;
    BoxCollider2D boxCollider;
    private void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        anim.SetBool("Died",true);
        boxCollider.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
