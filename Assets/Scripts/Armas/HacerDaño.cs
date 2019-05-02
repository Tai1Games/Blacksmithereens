using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HacerDaño : MonoBehaviour {

    public int daño;

    private Collider2D[] enemigosDañados;
    private int pos=0;
    private bool dañado;

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
        dañado = false;

        for (int i = 0; i < enemigosDañados.Length; i++)  //busca en el array de enemigos a ver si ese enemigo ya esta registrado o no
        {
            if (enemigosDañados[i] == col) dañado = true;
        }

        if (vida != null && !dañado)
        {
            vida.RestaVida(daño);
            System.Array.Resize(ref enemigosDañados, pos+1);  //añade una posicion al array
            enemigosDañados[pos] = col;
            pos++;
        }
    }

    /// <summary>
    /// Busca si el enimigo que se le pasa ya se le ha tocado durante esa animacion
    /// provisinalmente es bool por que es necesario para el martillo, tengo que preguntar a guille si esto es legal o no
    /// </summary>
    /// <param name="col"></param>
    /// <returns></returns>
    public bool BuscarEnemigo(Collider2D col)
    {

        for (int i = 0; i < enemigosDañados.Length; i++)
        {
            if (enemigosDañados[i] == col) dañado = true;
        }

        return dañado;
    }


    /// <summary>
    /// Una vez se ha terminado la animacion llama a este metodo que resetea los parametros para el proximo ataque
    /// </summary>
    public void AtaqueTerminado()
    {
        enemigosDañados = new Collider2D[0];
        pos = 0;
    }
}
