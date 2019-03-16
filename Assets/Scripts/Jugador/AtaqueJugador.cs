using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script lee el input del raton (ambos clicks) y avisa al arma que lleva equipada el jugador;

public class AtaqueJugador : MonoBehaviour
{

    public GameObject[] arrayArmas;

    int armaActual = 0;     //Codifica el arma que porta el jugador; 0 = martillo, 1 = lanza, 2 = espada, 3 = tomahawk
    LanzaAtaque scriptLanzaAtaque;
    MartilloAtaque scriptMartilloAtaque;


    void Start()
    {
        scriptMartilloAtaque = gameObject.GetComponentInChildren<MartilloAtaque>();
        scriptLanzaAtaque = gameObject.GetComponentInChildren<LanzaAtaque>();

        arrayArmas[1].SetActive(false);
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            switch (armaActual) //dependiendo del valor de armaActual, llama al arma correspondiente
            {
                case 0: //martillo
                    scriptMartilloAtaque.AtaqueMartillo();  //Avisa a martillo para que ataque.
                    break;
                case 1: //lanza
                    scriptLanzaAtaque.AtaqueLanza();    //Avisa a lanza para que ataque.
                    break;
                default:
                    Debug.Log("ningun arma seleccionada");
                    break;

            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            switch (armaActual) //dependiendo del valor de armaActual, llama al arma correspondiente
            {
                case 1: //lanza
                    scriptLanzaAtaque.LanzarLanza();    //Avisa a lanza para que sea lanzada.
                    break;
                default:
                    Debug.Log("ningun arma seleccionada");
                    break;

            }
        }
    }

    /// <summary>
    /// Cambia al arma especificada
    /// </summary>
    /// <param name="arma">0:martillo 1:lanza 2:espada 3:tomahawk</param>
    public void CambioArma(int arma)
    {
        arrayArmas[armaActual].SetActive(false);
        armaActual = arma;
        arrayArmas[armaActual].SetActive(true);
    }
}
