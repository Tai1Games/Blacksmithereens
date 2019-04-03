using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDaño : MonoBehaviour {
    
    private GameObject jugador;
    public int daño;
    VidaEnemigo vidaEnemigo;
    VidaBingBong vidaBingBong;


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        
        vidaEnemigo = col.GetComponent<VidaEnemigo>();   //coge referencia al objeto colisionado
        vidaBingBong = col.GetComponent<VidaBingBong>();

        if (vidaEnemigo != null)
            vidaEnemigo.RestaVida(daño);
        else if (vidaBingBong != null)
            vidaBingBong.RestaVida(daño);
    }
}
