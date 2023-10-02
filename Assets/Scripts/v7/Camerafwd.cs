using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafwd : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform Player;

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 offset = new Vector3(Player.position.x,Player.position.y + 3,-10);
        transform.position = Vector3.Lerp(transform.position, offset, speed);
    }
}
