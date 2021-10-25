using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MazeSetings : MonoBehaviour
{
    public Text sizeX;
    public Text sizeY;
    public Slider resizeX;
    public Slider resizeY;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sizeX.text = resizeX.value.ToString();
        sizeY.text = resizeY.value.ToString();
    }

    public void StartMaze()
    {
        Settings.SizeX = ((int)resizeX.value %2 == 0) ? ((int)resizeX.value-1) : ((int)resizeX.value);
        Settings.SizeY = ((int)resizeY.value % 2 == 0) ? ((int)resizeY.value-1) : ((int)resizeY.value);
        Debug.Log(Settings.SizeX + " " + Settings.SizeY);
        SceneManager.LoadScene("Maze");
    }
}
