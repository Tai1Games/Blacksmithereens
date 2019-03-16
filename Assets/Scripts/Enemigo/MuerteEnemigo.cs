using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Maneja la muerte del enemigo y dropea materiales
 */

public class MuerteEnemigo : MonoBehaviour {

    public int matDropeados;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    /// <summary>
    /// Suma materiales al jugador y destruye al enemigo
    /// </summary>
    public void Muerte()
    {
        if (this.GetComponent<Ladron>()) // Si el enemigo es el ladrón, devuelve los materiales que ha robado
            this.GetComponent<Ladron>().RecuperaMateriales();
        else //Si no, devuelve matdropeados
        {
            LevelManager.instance.SumarMateriales(matDropeados);
            LevelManager.instance.MuestraPopUpMat("+ " + matDropeados.ToString(), new Vector2(transform.position.x, transform.position.y)); //Dice al LvlManager que muestre los materiales nuevos en la posición donde muere
        }
        LevelManager.instance.EnemigoMuerto();
        Destroy(this.gameObject);
    }
}
