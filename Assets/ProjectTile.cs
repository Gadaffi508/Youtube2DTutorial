using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    public float speed;
    public CharecterController target;
    Vector3 _mousePos;
    public GameObject fire;
    public AnimationCurve curve;
    public float duration = 1;
    public float heightY = 3;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<CharecterController>();

        _mousePos = target.mousePos;
    }

    private void Update()
    {
        StartCoroutine(Curve(transform.position,_mousePos));
    }

    public IEnumerator Curve(Vector3 start, Vector2 targetP)
    {
        float timePassed = 0;
        Vector2 end = targetP;
        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }
        GameObject Obj = Instantiate(fire,transform.position,Quaternion.identity);
        Destroy(Obj,5f);
        Destroy(gameObject);
    }
}
