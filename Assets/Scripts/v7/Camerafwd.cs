using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafwd : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
