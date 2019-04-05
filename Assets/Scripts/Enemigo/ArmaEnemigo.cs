using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaEnemigo : MonoBehaviour {
    
    private GameObject jugador;
    public int daño;

	// Use this for initialization
	void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        VidaJugador vida;
        vida = col.GetComponent<VidaJugador>();   //coge referencia al objeto colisionado

        if (vida != null)
        {
            vida.RestaVida(daño); //Al colisionar con el jugador, le resta vida
        }
    }
}
