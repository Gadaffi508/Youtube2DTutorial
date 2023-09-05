using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    [SerializeField] private Transform teleportPos;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject effect;
    [SerializeField] private GameObject teleportEffect;
    [SerializeField] private TeleportManager teleportManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(Teleport());
        }
    }

    IEnumerator Teleport()
    {
        teleportManager.gameObject.SetActive(false);
        player.SetActive(false);
        effect.SetActive(true);
        transform.localScale = new Vector2(.1f,.1f);
        yield return new WaitForSeconds(1);
        effect.SetActive(false);
        player.transform.position = teleportPos.position;
        teleportEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        teleportEffect.SetActive(false);
        player.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        transform.localScale = new Vector2(1f, 1f);
        yield return new WaitForSeconds(1);
        teleportManager.gameObject.SetActive(true);
    }
}
