using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    public GameObject camera1;
    public GameObject camera2;
    public int manager;

    public void ManageCamera()
    {
        if (manager == 0)
        {
            Cam2();
            manager = 1;
        }
        else
        {
            Cam1();
            manager = 0;
        }
    }

    void Cam1()
    {
        camera1.SetActive(true);
        camera2.SetActive(false);
    }

    void Cam2()
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
    }
}
