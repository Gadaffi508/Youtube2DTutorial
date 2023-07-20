using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public float speed;
    public float facingValue;

    private void Start()
    {
         Destroy(gameObject,3f);
    }

    private void Update()
    {
        transform.Translate(speed * Time.deltaTime * facingValue,0,0);
    }
}
