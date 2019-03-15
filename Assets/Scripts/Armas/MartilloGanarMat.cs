using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartilloGanarMat : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D (Collider2D objeto)
    {
        //Al colisionar con el enemigo, activa su método para obtener materiales adicionales
        EnemigoDropeaMatMartillo sumaMat;
        sumaMat = objeto.GetComponent<EnemigoDropeaMatMartillo>();

        if (sumaMat != null) sumaMat.OtorgarMat();
    }
}
