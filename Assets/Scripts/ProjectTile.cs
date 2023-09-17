using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject fire;
    [SerializeField] private float duration;
    [SerializeField] private float heaightY;

    public AnimationCurve curve;

    Vector3 _mousePos;
    CharecterController player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CharecterController>();

        _mousePos = player.mousePos;
    }

    public IEnumerator Curve(Vector3 start, Vector2 target)
    {
        float timePassed = 0;
        Vector2 end = target;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float LinearT = timePassed / duration;
            float heaightT = curve.Evaluate(LinearT);

            float heaight = Mathf.Lerp(0f, heaightY, heaightT);

            transform.position = Vector2.Lerp(start,end,LinearT) + new Vector2(0f, heaight);
            yield return null;
        }

        GameObject obj = Instantiate(fire,transform.position,Quaternion.identity);
        Destroy(obj,3f);
        Destroy(gameObject);
    }
}
