using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOrWinMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Win() {
        /// TODO: Open Next scene
    }

    public void Lose() {
        SceneManager.LoadScene("SampleScene");
    }

    public void No(){}

}

