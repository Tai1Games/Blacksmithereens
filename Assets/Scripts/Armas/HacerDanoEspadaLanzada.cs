using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDanoEspadaLanzada : MonoBehaviour
{
    private int durabilidad;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDurabilidad(int num)
    {
        durabilidad = num;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        float daño;
        float durab = durabilidad;
        VidaEnemigo vida;
        vida = col.GetComponent<VidaEnemigo>();   //coge referencia al objeto colisionado

        if (vida != null)
        {
            daño = 30 + (durab / 15) * 20;  //formula para calcular el daño según la durabilidad de la espada al ser lanzada
            int daño2 = (int)Mathf.Round(daño);
            vida.RestaVida(daño2);
        }
    }

}
