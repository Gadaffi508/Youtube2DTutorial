using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    [SerializeField] private GameObject deadParticle;
    [SerializeField] private GameObject[] boxes;
    public int boxValue;

    public void BoxDamage()
    {
        if (boxValue == 3)
        {
            GameObject particle = Instantiate(deadParticle, transform.position, Quaternion.identity);
            Destroy(particle, 2f);
            Destroy(gameObject);
        }
        else
        {
            boxValue++;
        }

        boxes[boxValue].SetActive(true);
        boxes[boxValue - 1].SetActive(false);
    }
}
