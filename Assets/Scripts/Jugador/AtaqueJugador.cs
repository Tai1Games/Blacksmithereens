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
    private Vector3[] initPos;
    

    void Start()
    {
        initPos = new Vector3[4];
        scriptMartilloAtaque = gameObject.GetComponentInChildren<MartilloAtaque>();
        scriptLanzaAtaque = gameObject.GetComponentInChildren<LanzaAtaque>();
        scriptEspadaAtaque = gameObject.GetComponentInChildren<EspadaAtaque>();
        scriptTomahawkAtaque = gameObject.GetComponentInChildren<TomahawkAtaque>();

        initPos[0] = arrayArmas[0].transform.localPosition;
        for (int k = 1; k < arrayArmas.Length; k++)
        {
            initPos[k] = arrayArmas[k].transform.localPosition;
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
                    scriptTomahawkAtaque.LanzarTomahawk();  //Avisa al tomahawk para que sea lanzado
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
                case Armas.Espada:
                    scriptEspadaAtaque.LanzarEspada();
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
        arrayArmas[(int)armaActual].transform.localPosition = (initPos[(int)armaActual]);
        arrayArmas[(int)armaActual].SetActive(false);
        armaActual = arma;
        arrayArmas[(int)armaActual].SetActive(true);
		LevelManager.instance.ActualizaDurabilidad(1, 1);

		switch (arma) {
			case Armas.Martillo:
				LevelManager.instance.CambiaSpriteUI(Armas.Martillo);
				break;
			case Armas.Lanza:
                scriptLanzaAtaque.ReseteaDurLanza();
				LevelManager.instance.CambiaSpriteUI(Armas.Lanza);
                break;
            case Armas.Espada:
                scriptEspadaAtaque.ReseteaDurEspada();
				LevelManager.instance.CambiaSpriteUI(Armas.Espada);
				break;
            case Armas.Tomahawk:
                scriptTomahawkAtaque.ReseteaDurTomahawk();
				LevelManager.instance.CambiaSpriteUI(Armas.Tomahawk);
				break;
        }
    }


    /// <summary>
    /// Sube el daño de cada arma a 10000 y la durabiliadad a 1000
    /// </summary>
    public void SubirDaño(bool estado)
    {
        if (estado)  //ativa cheats
        {
            scriptEspadaAtaque.ActivaCheats(true);
            scriptMartilloAtaque.ActivaCheats(true);
            scriptLanzaAtaque.ActivaCheats(true);
            scriptTomahawkAtaque.ActivaCheats(true);
        }
        else  //desactiva cheats
        {
            scriptEspadaAtaque.ActivaCheats(false);
            scriptMartilloAtaque.ActivaCheats(false);
            scriptLanzaAtaque.ActivaCheats(false);
            scriptTomahawkAtaque.ActivaCheats(false);
        }

    }

}
