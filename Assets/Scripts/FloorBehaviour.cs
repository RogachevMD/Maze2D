using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
    public void MarkAsVisited()
    {
        GetComponent<SpriteRenderer>().color = Color.cyan;
    }
}
