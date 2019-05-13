using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDanoEspadaLanzada : MonoBehaviour
{
    private int durabilidad;
    
    private Collider2D[] enemigosDañados;
    private int pos = 0;
    private bool dañado;

    // Use this for initialization
    void Start()
    {
        enemigosDañados = new Collider2D[0];
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
            dañado = false;
            int i = 0;
            while (i < enemigosDañados.Length && !dañado)  //busca en el array de enemigos a ver si ese enemigo ya esta registrado o no
            {
                if (enemigosDañados[i] == col) dañado = true;
                i++;
            }

            if (!dañado)
            {
                daño = 30 + (durab / 15) * 20;  //formula para calcular el daño según la durabilidad de la espada al ser lanzada
                int daño2 = (int)Mathf.Round(daño);
                vida.RestaVida(daño2);
                System.Array.Resize(ref enemigosDañados, pos + 1);  //añade una posicion al array
                enemigosDañados[pos] = col;
            }
        }
    }

}
