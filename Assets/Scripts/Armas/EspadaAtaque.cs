using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Este meétodo se encarga de:
    *  llevar la cuenta de la durabilidad de la espada
    *  que al al recibir la orden de ataque(ataqueJugador.cs), la espada ataque y luego vuelva a su posicion normal.
   */

public class EspadaAtaque : MonoBehaviour {

    

    public int durMaxEspada = 20;       //Durabilidad máxima

    Animator animador;
    AtaqueJugador scriptarmas;
    int durActualEspada;                 //Durabilidad actual, la que va disminuyendo

    void Start()
    {
        animador = gameObject.GetComponent<Animator>();
        durActualEspada = durMaxEspada;
        scriptarmas = LevelManager.instance.Jugador().GetComponent<AtaqueJugador>();
    }

    void Update()
    {

    }

    /// <summary>
    /// Si la espada no está ya atacando, realiza un ataque.
    /// </summary>
    public void AtaqueEspadas()
    {
        if (animador.GetCurrentAnimatorStateInfo(0).IsName("EspadaNormal"))      //animacion actual, para evitar ataques dobles
        {
            animador.SetTrigger("Ataque");
        }
    }


    /// <summary>
    /// Resta 'cantidad' a la durabilidad actual de la espada.
    /// En cada ataque, se llama desde la animacion de ataque de la espada
    /// </summary>
    /// <param name="cantidad">Puntos de durabilidad a ser descontados</param>
    public void RestaDurEspada(int cantidad)
    {
        durActualEspada -= cantidad;
        print("Durabilidad Espada: " + durActualEspada);
        if (durActualEspada <= 0)
        {
            scriptarmas.CambioArma(Armas.Martillo);
            durActualEspada = durMaxEspada;
        }   

    }

}
