using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *Este script se encarga de manejar la vida del jugador, dejando aumentarla o disminuirla. 
 */

public class VidaJugador : MonoBehaviour {

    public int vidaMax = 100;
    public float tiempoInvulnerabilidad;

    int vidaActual;
    MuerteJugador muerte;
    bool invulnerable;
    float t;
    Animator animador;

	// Use this for initialization
	void Start () {
        vidaActual = vidaMax;
        muerte = GetComponent<MuerteJugador>();
        animador = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (invulnerable)
            t += Time.deltaTime; //Si invulnerabilidad es true, se suma tiempo al contador
        if(t>=tiempoInvulnerabilidad) //Al acabar el tiempo, se desactiva la invulnerabilidad y se resetea el contador
        {
            invulnerable = false;
            t = 0;
        }
	}

    /// <summary>
    /// Resta la cantidad de vida especificada al jugador
    /// </summary>
    /// <param name="cantidad">Cantidad de vida a restar</param>
    public void RestaVida(int cantidad)
    {
        if(invulnerable == false) //Si el boooleano de invulnerabilidad está activado, no se resta vida
        {
            vidaActual = vidaActual - cantidad;
            if (vidaActual <= 0)
            {
                muerte.JugadorMuere();
            }
            LevelManager.instance.ActualizaVida(vidaActual, vidaMax);
            StartCoroutine(Invulnerabilidad()); //Se comienza la corrutina de invulnerabilidad
        }
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

    /// <summary>
    /// Esta corrutina activará el booleano de invulnerabilidad y reproducirá la animación hasta que el tiempo acabe
    /// </summary>
    private IEnumerator Invulnerabilidad()
    {
        invulnerable = true;
        for(int i=0; i<(int)tiempoInvulnerabilidad; i++)
        {
            animador.Play("Invulnerabilidad", -1, 0);
            yield return new WaitForSeconds(1);
        }
    }
}
