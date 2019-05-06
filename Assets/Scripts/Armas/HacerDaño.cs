using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDaño : MonoBehaviour {

    public int daño;

    private int dañoAux;

	// Use this for initialization
	void Start () {
        dañoAux = daño;
    }

    // Update is called once per frame
    void Update () {
		
	}


    private void OnTriggerEnter2D(Collider2D col)
    {
        VidaEnemigo vida;
        vida = col.GetComponent<VidaEnemigo>();   //coge referencia al objeto colisionado

        if (vida != null)
        {
            vida.RestaVida(daño);
        }
    }


    /// <summary>
    /// Añade 10000 de daño a cada arma
    /// </summary>
    public void CheatsArmas(bool estado)
    {
        if (estado) daño = 10000;
        else daño = dañoAux;
    }


}
