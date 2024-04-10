using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
   
    private bool manager = false;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void CameraSwitched()
    {
        manager = !manager;
        animator.SetBool("CameraSwitch", manager);
        
    }
}
