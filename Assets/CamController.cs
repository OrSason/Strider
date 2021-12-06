using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public Cinemachine.CinemachineFreeLook cam;
    public float reCenterSpeed = 0.3f;

    void Awake()
    {
        cam = GetComponent<Cinemachine.CinemachineFreeLook>();
    }

    void Update()
    {
        if (cam.m_XAxis.Value > 0.5)
        {
            cam.m_XAxis.Value -= reCenterSpeed;
        }
        if (cam.m_XAxis.Value < -0.5)
        {
            cam.m_XAxis.Value += reCenterSpeed;
        }

        if (cam.m_YAxis.Value > 0.5)
        {
            cam.m_YAxis.Value -= reCenterSpeed;
        }
        if (cam.m_YAxis.Value < -0.5)
        {
            cam.m_YAxis.Value += reCenterSpeed;
        }

    }
}
