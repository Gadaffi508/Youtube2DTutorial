using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public float speed;
    public Transform target;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;

            Vector3 movePos = transform.position + direction * speed * Time.deltaTime;

            transform.position = movePos;
        }
    }
}
