using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Este script lee el input del raton (ambos clicks) y avisa al arma que lleva equipada el jugador;

public enum Armas
{
    Martillo,
    Lanza,
    Espada,
    Tomahawk,
}

public class AtaqueJugador : MonoBehaviour
{

    public GameObject[] arrayArmas;

    Armas armaActual = Armas.Martillo;     //Guarda el arma que porta el jugador
    LanzaAtaque scriptLanzaAtaque;
    MartilloAtaque scriptMartilloAtaque;
    EspadaAtaque scriptEspadaAtaque;
    TomahawkAtaque scriptTomahawkAtaque;
    

    void Start()
    {
        scriptMartilloAtaque = gameObject.GetComponentInChildren<MartilloAtaque>();
        scriptLanzaAtaque = gameObject.GetComponentInChildren<LanzaAtaque>();
        scriptEspadaAtaque = gameObject.GetComponentInChildren<EspadaAtaque>();
        scriptTomahawkAtaque = gameObject.GetComponentInChildren<TomahawkAtaque>();

        for (int k = 1; k < arrayArmas.Length; k++)
        {
            arrayArmas[k].SetActive(false);
        }
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            switch (armaActual) //dependiendo del valor de armaActual, llama al arma correspondiente
            {
                case Armas.Martillo: //martillo
                    scriptMartilloAtaque.AtaqueMartillo();  //Avisa a martillo para que ataque.
                    break;
                case Armas.Lanza: //lanza
                    scriptLanzaAtaque.AtaqueLanza();    //Avisa a lanza para que ataque.
                    break;
                case Armas.Espada: //espada
                    scriptEspadaAtaque.AtaqueEspadas();  //Avisa a la espada para que ataque.
                    break;
                case Armas.Tomahawk:
                    scriptTomahawkAtaque.tomahawkAtaque();  //Avisa al tomahawk para que ataque.
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
                case Armas.Lanza: //lanza
                    scriptLanzaAtaque.LanzarLanza();    //Avisa a lanza para que sea lanzada.
                    break;
                case Armas.Tomahawk: //tomahawk
                    scriptTomahawkAtaque.LanzarTomahawk();  //Avisa al tomahawk para que sea lanzado.
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
    /// <param name="arma">Nombre del arma al que se quiere cambiar</param>
    public void CambioArma(Armas arma)
    {
        arrayArmas[(int)armaActual].SetActive(false);
        armaActual = arma;
        arrayArmas[(int)armaActual].SetActive(true);
    }
}
