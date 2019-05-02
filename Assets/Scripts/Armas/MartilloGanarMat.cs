using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartilloGanarMat : MonoBehaviour {

    private HacerDaño hd;

	// Use this for initialization
	void Start () {
        hd = GetComponent<HacerDaño>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D objeto)
    {
        bool tocado = hd.BuscarEnemigo(objeto);  //mira si durante esa animacion ya le ha dado a ese enemigo o no

        if (tocado)
        {
            //Al colisionar con el enemigo, activa su método para obtener materiales adicionales
            EnemigoDropeaMatMartillo sumaMat;
            sumaMat = objeto.GetComponent<EnemigoDropeaMatMartillo>();
            if (sumaMat != null) sumaMat.OtorgarMat();
        }
    }
}
