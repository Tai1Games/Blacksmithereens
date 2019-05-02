using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartilloGanarMat : MonoBehaviour {
    

    private Collider2D[] enemigosDañados;
    private int pos = 0;
    private bool dañado;

    // Use this for initialization
    void Start () {
        enemigosDañados = new Collider2D[0];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D objeto)
    {

        dañado = false;
        int i = 0;
        while (i < enemigosDañados.Length && !dañado)  //busca en el array de enemigos a ver si ese enemigo ya esta registrado o no
        {
            if (enemigosDañados[i] == objeto) dañado = true;
            i++;
        }

        if (!dañado)
        {
            System.Array.Resize(ref enemigosDañados, pos + 1);  //añade una posicion al array
            enemigosDañados[pos] = objeto;
            pos++;

            //Al colisionar con el enemigo, activa su método para obtener materiales adicionales
            EnemigoDropeaMatMartillo sumaMat;
            sumaMat = objeto.GetComponent<EnemigoDropeaMatMartillo>();
            if (sumaMat != null) sumaMat.OtorgarMat();

        }


    }

    /// <summary>
    /// Una vez se ha terminado la animacion llama a este metodo que resetea los parametros para el proximo ataque
    /// </summary>
    public void AtaqueTerminadoMartillo()
    {
        enemigosDañados = new Collider2D[0];
        pos = 0;
    }
}
