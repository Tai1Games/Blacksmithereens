using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDaño : MonoBehaviour {

    public int daño;

    private Collider2D[] enemigosDañados;
    private int pos=0;

	// Use this for initialization
	void Start () {
        enemigosDañados = new Collider2D[3];
    }

    // Update is called once per frame
    void Update () {
		
	}
    

    private void OnTriggerEnter2D(Collider2D col)
    {        
        VidaEnemigo vida;
        vida = col.GetComponent<VidaEnemigo>();   //coge referencia al objeto colisionado
        bool dañado = false;

        for(int i = 0; i < enemigosDañados.Length; i++)
        {
            if (enemigosDañados[i] == col) dañado = true;
        }


        if (vida != null && !dañado)
        {
            vida.RestaVida(daño);
            enemigosDañados[pos] = col;
            pos++;
        }
    }

    public void AtaqueTerminado()
    {
        enemigosDañados = new Collider2D[3];
        pos = 0;
    }
}
