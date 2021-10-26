using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Debug.Log("ESC Pressed");
            if (GetComponent<PlayerController>().OnPause)
            {
                GetComponent<PlayerController>().OnPause = false;
                Debug.Log("Resume");
            }
            else
            {
                GetComponent<PlayerController>().OnPause = true;
                Debug.Log("Pause");
            }
        }
    }
}
