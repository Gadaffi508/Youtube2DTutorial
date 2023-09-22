using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyC : MonoBehaviour
{
    [SerializeField] private int Health;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
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
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}
