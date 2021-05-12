using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTile : DefaultTile
{
    public ParticleSystem ExplosionEffect;

    protected override void OnTriggerEnter(Collider col)
    {
        Color newColor = col.gameObject.GetComponent<Projectile>().ProjectileColor;

        if (newColor != null)
        {
            SetColor(newColor);
            ParticleSystem.MainModule main = ExplosionEffect.main;
            main.startColor = newColor;
            ExplosionEffect.Play();
            Destroy(col.gameObject);
        }
    }
}
