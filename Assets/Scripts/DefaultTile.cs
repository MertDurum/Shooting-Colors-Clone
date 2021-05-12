using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTile : MonoBehaviour
{
    public Color ExpectedColor;
    public Color CurrentColor;

    void Start()
    {

    }

    public void SetColor(Color _color)
    {
        // Call Game-Manager to check each tile to decide if the win conditions are set
        CurrentColor = _color;
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = CurrentColor; // edit this.
    }

    public bool isCorrect()
    {
        if (CurrentColor == ExpectedColor)
            return true;
        return false;
    }

    // onTriggerEnter -> call set color
}
