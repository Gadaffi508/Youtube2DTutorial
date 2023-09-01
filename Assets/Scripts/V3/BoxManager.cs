using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private GameObject[] boxes;
    public int objeValue;
    [SerializeField] private GameObject deadParticle;

    public void ObjectActiveManager()
    {
        objeValue++;
        boxes[objeValue].SetActive(true);
        boxes[objeValue - 1].SetActive(false);

        if (objeValue == 3)
        {
            Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
