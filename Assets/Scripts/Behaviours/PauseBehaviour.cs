using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    private bool _onPause = false;
    public GameObject PausePH; 
    // Start is called before the first frame update
    void Start()
    {
        PausePH.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            
            if (_onPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        _onPause = true;
        PausePH.SetActive(true);
        //Debug.Log("Pause");
    }

    public void Resume()
    {
        Time.timeScale = 1;
        _onPause = false;
        PausePH.SetActive(false);
        //Debug.Log("Resume");
    }
}
