using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class LogoController : MonoBehaviour
{
    public CinemachineStoryboard cam;
    public float logoFadeTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        cam.m_ShowImage = true;
        Invoke("closeLogo", logoFadeTime);
    }

    public void closeLogo()
    {
        cam.m_ShowImage = false;
    }
}
