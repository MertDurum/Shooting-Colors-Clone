using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : DefaultTile
{
    protected override void OnTriggerEnter(Collider col)
    {
        Color newColor = col.gameObject.GetComponent<Projectile>().ProjectileColor;

        if (newColor != null)
        {
            SetColor(newColor);
            // add particle effect
            Destroy(col.gameObject);
        }
    }
}
