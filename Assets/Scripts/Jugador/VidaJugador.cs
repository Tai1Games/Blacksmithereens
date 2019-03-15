using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Este script se encarga de manejar la vida del jugador, dejando aumentarla o disminuirla. 
 */

public class VidaJugador : MonoBehaviour {

    public int vidaMax = 100;

    int vidaActual;
    MuerteJugador muerte;

	// Use this for initialization
	void Start () {
        vidaActual = vidaMax;
        muerte = GetComponent<MuerteJugador>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Resta la cantidad de vida especificada al jugador
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a restar</param>
    public void RestaVida(int cantidad)
    {
        vidaActual = vidaActual - cantidad;
        if(vidaActual <= 0)
        {
            muerte.JugadorMuere();
        }
        LevelManager.instance.ActualizaVida(vidaActual, vidaMax);
    }

    /// <summary>
    /// Suma la cantidad especificada a la vida del jugador. Si supera el máximo, se queda en el máximo
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a añadir</param>
    public void SumaVida(int cantidad)
    {
        vidaActual = vidaActual + cantidad;
        if(vidaActual > vidaMax)
        {
            vidaActual = vidaMax;
        }
        LevelManager.instance.ActualizaVida(vidaActual, vidaMax);
    }
}
