using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * Este meétodo se encarga de:
    *  llevar la cuenta de la durabilidad de la espada
    *  que al al recibir la orden de ataque(ataqueJugador.cs), la espada ataque y luego vuelva a su posicion normal
    *  lanzar la espada
   */

public class EspadaAtaque : MonoBehaviour {

    public int durMaxEspada = 20;       //Durabilidad máxima
    public GameObject espadaPrefab;     //prefab de la espada que va a ser lanzada
    public float velocidad;             //velocidad del lanzamiento

    Animator animador;                  //animador de la espada
    AtaqueJugador scriptarmas;          //script que lleva el jugador y que le permite atacar
    int durActualEspada;                //Durabilidad actual, la que va disminuyendo

    GameObject espadaLanzada;           //espada lanzada en si
    
    Vector2 mouse_position;             //posicion del raton
    Vector2 offset, screenPoint;        //vectores para sacar el angulo

    void Start()
    {
        animador = gameObject.GetComponent<Animator>();     //asignamos las referencias
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
    /// Comprueba si la lanza está cerca del jugador y, en ese caso, activa la animación de lanzarla
    /// </summary>
    public void LanzarEspada()
    {
        if (animador.GetCurrentAnimatorStateInfo(0).IsName("EspadaNormal"))     //Posicion relativa al jugador, para evitar ataques dobles
        {            
            animador.SetTrigger("PreparaLanz");     //Le decimos a la espada que haga la animacion de carga de la lanza, el lanzamiento como tal se llama en un evento en dicha animación     
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
		else
			LevelManager.instance.ActualizaDurabilidad(durMaxEspada, durActualEspada);

    }

    /// <summary>
    /// Resetea al valor maximo la durabilidad de la espada
    /// </summary>
    public void ReseteaDurEspada()
    {
        durActualEspada = durMaxEspada;
    }

    void GeneraEspada() {

        espadaLanzada = (GameObject)Instantiate(espadaPrefab, transform); //crea la lanza que va a ser lanzada
        espadaLanzada.GetComponent<SpriteRenderer>().sortingOrder = 1; //cambia la sortingLayer
        espadaLanzada.transform.parent = null;  //elimina el padre de la lanzaLanzada para evitar que rote con el jugador
        mouse_position = Input.mousePosition; //obtiene posicion del raton
        screenPoint = Camera.main.WorldToScreenPoint(transform.position); //saca la posicion del jugador en relacion al tamaño de la pantalla de juego
        offset = new Vector2(mouse_position.x - screenPoint.x, mouse_position.y - screenPoint.y); //diferencia de posicion entre raton y jugador
        espadaLanzada.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(offset, velocidad); //impulsa la lanza
        espadaLanzada.GetComponent<HacerDanoEspadaLanzada>().SetDurabilidad(durActualEspada); //le pasa a la lanza la durabilidad actual
        durActualEspada = durMaxEspada;  //resetea la durabiliad complete de la lanza

        this.gameObject.SetActive(false);

        scriptarmas.CambioArma(0); //cambia al martillo
    }

}
