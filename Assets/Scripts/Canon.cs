using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    public Color ProjectileColor;

    public MeshRenderer MR;

    void Start()
    {
        MR = gameObject.GetComponentInChildren<MeshRenderer>();
        MR.material.color = ProjectileColor;
    }
}
