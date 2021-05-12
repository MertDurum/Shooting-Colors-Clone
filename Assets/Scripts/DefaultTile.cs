using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTile : MonoBehaviour
{
    public Color ExpectedColor;
    public Color CurrentColor;

    public void SetColor(Color _color)
    {
        CurrentColor = _color;
        gameObject.GetComponentInChildren<MeshRenderer>().material.color = CurrentColor;
        // Call Game-Manager to check each tile to decide if the win conditions are set
    }

    public bool isCorrect()
    {
        if (CurrentColor == ExpectedColor)
            return true;
        return false;
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("TEST#1");
        Color newColor = col.GetComponent<Projectile>().ProjectileColor;

        if (newColor != null)
            SetColor(newColor);
    }
}
