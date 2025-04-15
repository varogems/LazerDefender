using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingCamera : MonoBehaviour
{



    [SerializeField] float m_shakeDuration    = 1f;
    [SerializeField] float m_shakeMagnitude   = 0.5f;
    Vector3                m_initPosCamera;
    [SerializeField] bool  m_isShakingScreen;

    void Awake()
    {
        m_initPosCamera = transform.position;
    }
    

    public void shakeScreen()
    {
        if(m_isShakingScreen) StartCoroutine(CRTShakeScreen());
    }

    IEnumerator CRTShakeScreen()
    {
        float elapsedTime = 0;
        while (elapsedTime < m_shakeDuration)
        {
            transform.position = m_initPosCamera + (Vector3)Random.insideUnitCircle * m_shakeMagnitude;
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = m_initPosCamera;
    }


}
