using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Color ProjectileColor;
    public float Speed;
    public float LifeTime;

    public MeshRenderer MR;

    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    public void SetColor(Color _color)
    {
        ProjectileColor = _color;
        MR.material.color = ProjectileColor;
    }

    void Update()
    {
        // not sure if this is the best way to do this.
        gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);
    }
}
