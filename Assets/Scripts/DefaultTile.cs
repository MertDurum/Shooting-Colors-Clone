using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultTile : MonoBehaviour
{
    public Color ExpectedColor;
    public Color CurrentColor;

    public MeshRenderer MR;

    public void SetColor(Color _color)
    {
        CurrentColor = _color;
        MR.material.color = CurrentColor;
        // Call Game-Manager to check each tile to decide if the win conditions are set
    }

    public bool isCorrect()
    {
        if (CurrentColor == ExpectedColor)
            return true;
        return false;
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        Color newColor = col.gameObject.GetComponent<Projectile>().ProjectileColor;

        if (newColor != null)
            SetColor(newColor);
    }
}
