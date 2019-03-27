using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Se encarga de hacer daño al jugador cuando colisione con un enemigo
 */


public class DañaJugador : MonoBehaviour {

    public int daño;    

	void Start () {
		
	}
	
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D objeto)
    {
        VidaJugador vida = objeto.gameObject.GetComponent<VidaJugador>();
        if (vida)
        {
            vida.RestaVida(daño);
        }
    }
}
