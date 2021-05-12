using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public GameObject ProjectilePrefab;
    public Color ProjectileColor;
    public Transform FiringTransform;

    public MeshRenderer MR;
    public GameManager GM;

    void Start()
    {
        MR.material.color = ProjectileColor;
    }

    private void OnMouseDown()
    {
        if (!GM.Paused)
        {
            GameObject projectile = Instantiate(ProjectilePrefab, FiringTransform.position, FiringTransform.rotation);
            projectile.GetComponent<Projectile>().SetColor(ProjectileColor);
        }
    }
}
