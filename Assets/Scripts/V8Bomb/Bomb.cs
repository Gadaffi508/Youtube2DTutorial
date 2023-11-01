using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float insensity;
    public float duration;
    public Camera mainCamera;

    float shakeAmount = 0;

    private void Awake()
    {
        if(mainCamera == null) mainCamera = Camera.main;
        Debug.Log("Merhaba");
    }

    private void Shake(float _insensity,float _duration)
    {
        shakeAmount = _insensity;
        InvokeRepeating("ShakeTime",0,0.01f);
        Invoke("StopShake",_duration);
    }

    void ShakeTime()
    {
        if (shakeAmount > 0)
        {
            Vector3 camPos = mainCamera.transform.position;

            float offsetX = Random.Range(-1f,1f) * shakeAmount;
            float offsetY = Random.Range(-1f,1f) * shakeAmount;

            camPos.x += offsetX;
            camPos.y += offsetY;

            mainCamera.transform.position = camPos;
        }
    }

    void StopShake()
    {
        CancelInvoke("ShakeTime");
        Vector3 _offset = new Vector3(0,0,-10);
        mainCamera.transform.localPosition = Vector3.zero + _offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Shake(insensity,duration);
        Destroy(gameObject,duration);
    }
}
