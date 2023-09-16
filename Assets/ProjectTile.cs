using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public float speed;
    public CharecterController target;
    Vector3 _mousePos;

    private void Start()
    {
        Destroy(gameObject, 3f);

        target = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CharecterController>();

        _mousePos = target.mousePos;
    }

    private void Update()
    {
        Vector3 direction = (_mousePos - transform.position).normalized;

        Vector3 movePos = transform.position + direction * speed * Time.deltaTime;

        transform.position = movePos;
    }
}
