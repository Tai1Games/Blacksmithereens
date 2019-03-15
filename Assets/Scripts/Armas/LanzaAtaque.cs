using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanzaAtaque : MonoBehaviour
{

    /*
     * Este meétodo se encarga de:
     *  llevar la cuenta de la durabilidad de la lanza
     *  que al al recibir la orden de ataque(ataqueJugador.cs), la lanza se mueva hacia delante y luego vuelva a su posicion normal.
    */

    Animator animador;
    Transform transformLanza;
    AtaqueJugador scriptarmas;

     int durMaxLanza = 20 ;       //Durabilidad máxima
     int durActualLanza;                 //Durabilidad actual, la que va disminuyendo

    void Start()
    {
        animador = gameObject.GetComponent<Animator>();
        transformLanza = gameObject.GetComponent<Transform>();
        durActualLanza = durMaxLanza;
        scriptarmas = LevelManager.instance.Jugador().GetComponent<AtaqueJugador>();
    }

    void Update()
    {

    }

    /// <summary>
    /// Comprueba si la lanza está cerca del jugador y, en ese caso, activa la animación de ataque
    /// </summary>
     public void AtaqueLanza() {
        if ((transformLanza.localPosition.y > -0.8) && animador.GetCurrentAnimatorStateInfo(0).IsName("LanzaNormal"))      //Posicion relativa al jugador, para evitar ataques dobles
        {
            animador.SetTrigger("Ataque");
            RestaDurLanza(1);   //Esta llamada realmente iria e el OnTriggerEnter de HacerDano.cs
        }
    }


    /// <summary>
    /// Resta 'cantidad' a la durabilidad actual de la lanza
    /// </summary>
    /// <param name="cantidad">Puntos de durabilidad a ser descontados</param>
     public void RestaDurLanza(int cantidad) {
        durActualLanza -= cantidad;
        print("Durabilidad Lanza: " + durActualLanza);
        if (durActualLanza <= 0)
        {
            scriptarmas.CambioArma(0);
            durActualLanza = durMaxLanza;
        }
    }

}
