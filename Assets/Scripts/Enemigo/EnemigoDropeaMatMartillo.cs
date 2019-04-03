using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoDropeaMatMartillo : MonoBehaviour {

    public int numGolpes = 3;  //Número de veces que el jugador puede obtener materiales de un solo enemigo
    public int cantMat = 25;  //Materiales obtenidos al dar un golpe con el martillo

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Al colisionar con el enemigo, obtiene materiales adicionales
    /// </summary>
    public void OtorgarMat()
    {
        if (numGolpes > 0) //Si no se pueden obtener más materiales del enemigo, no se suma nada.
        {
            LevelManager.instance.SumarMateriales(cantMat); //Se suman materiales.
            LevelManager.instance.MuestraPopUpMat(("+ " + cantMat), new Vector2(this.transform.position.x, this.transform.position.y)); //Se muestra el pop up de los nuevos materiales.
            numGolpes--;  //Se resta en 1 el número de golpes disponibles.
        }
    }


}
