using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Maneja la vida del enemigo
 */
public class VidaEnemigo : MonoBehaviour {
    public int vidaEnemigo;

    int vidaActual;
    MuerteEnemigo scriptMuerte;


    // Use this for initialization
    void Start()
    {
        vidaActual = vidaEnemigo;
        scriptMuerte = GetComponent<MuerteEnemigo>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Resta un numero a la vida del eneimgo
    /// </summary>
    /// <param name="cantidad">Cantidad a restar a la vida</param>
    /// <param name="jugador">Referencia del jugador</param>
    public void RestaVida(int cantidad)
    {
        vidaActual = vidaActual - cantidad;
        if (vidaActual <= 0 && scriptMuerte) //Si existe el componente adecuado para morir, se muere el enemigo
        {
            scriptMuerte.Muerte();
        }
    }
}
