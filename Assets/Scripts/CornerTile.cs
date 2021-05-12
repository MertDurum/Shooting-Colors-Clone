using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornerTile : DefaultTile
{
    protected override void OnTriggerEnter(Collider col)
    {
        Color newColor = col.gameObject.GetComponent<Projectile>().ProjectileColor;

        if (newColor != null)
        {
            SetColor(newColor);

            Transform t = col.gameObject.transform;
            t.rotation = t.rotation * Quaternion.Euler(0, 90, 0);
            t.position = new Vector3(gameObject.transform.position.x, t.position.y, gameObject.transform.position.z);
        }
    }
}
