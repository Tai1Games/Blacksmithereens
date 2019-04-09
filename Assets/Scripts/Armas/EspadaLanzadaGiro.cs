using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Este script hace que la espada de vueltas al ser lanzada
/// </summary>
 
public class EspadaLanzadaGiro : MonoBehaviour
{
    public float rotacion;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Awake()
    {
        this.GetComponent<Rigidbody2D>().angularVelocity = rotacion;
    }
}
