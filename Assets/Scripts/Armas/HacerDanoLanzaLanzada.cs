using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDanoLanzaLanzada : MonoBehaviour {

    private int durabilidad;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetDurabilidad(int num)
    {
        durabilidad = num;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        int daño;
        VidaEnemigo vida;
        vida = col.GetComponent<VidaEnemigo>();   //coge referencia al objeto colisionado

        if (vida != null)
        {
            daño = 20 + (durabilidad / 10) * 20;  //formula para calcular el daño según la durabilidad de la lanza al ser lanzada
            vida.RestaVida(daño);
        }
    }

}
