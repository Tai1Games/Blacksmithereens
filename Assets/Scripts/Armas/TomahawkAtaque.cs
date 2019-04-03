using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomahawkAtaque : MonoBehaviour {


    public GameObject tomahawkLanzado; //prefab del gameobject que lanzaremos
    public float velocidad; //velocidad del objeto creado
    private Vector2 mouse_position;
    private Vector2 offset, screenPoint;

    Animator animador;
    AtaqueJugador scriptArmas;
    GameObject lanzado;

    public int durMaxTomahawk = 3; //durabilidad del tomahawk
    private int durActualTomahawk;

    // Use this for initialization
    void Start () {

        animador = gameObject.GetComponent<Animator>();
        durActualTomahawk = durMaxTomahawk;  //asignamos la durabilidad del tomahawk
        scriptArmas = LevelManager.instance.Jugador().GetComponent<AtaqueJugador>();
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void tomahawkAtaque()
    {
        if ((transform.localPosition.y < 0.09768)) //Posicion relativa al jugador, para evitar ataques dobles
        {
            animador.SetTrigger("AtaqueTomahawk");  //Trigger para activar la animación
        }
    }

    /// <summary>
    /// Instancia un tomahawk arrojable y resta 1 punto de durabilidad al arma.
    /// </summary>
    public void LanzarTomahawk()
    {

        if (transform.localPosition.y < 0.09768)      //Posicion relativa al jugador, para evitar ataques dobles
        {
            lanzado = (GameObject)Instantiate(tomahawkLanzado, transform); //crea la lanza que va a ser lanzada
            lanzado.GetComponent<SpriteRenderer>().sortingOrder = 1; //cambia la sortingLayer
            lanzado.transform.parent = null;  //elimina el padre de la lanzaLanzada para evitar que rote con el jugador
            mouse_position = Input.mousePosition; //obtiene posicion del raton
            screenPoint = Camera.main.WorldToScreenPoint(transform.position); //saca la posicion del jugador en relacion al tamaño de la pantalla de juego
            offset = new Vector2(mouse_position.x - screenPoint.x, mouse_position.y - screenPoint.y); //diferencia de posicion entre raton y jugador
            lanzado.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(offset, velocidad); //impulsa la lanza
            restaDurTomahawk(1); //resta un punto de durabilidad al arma

        }
    }

    /// <summary>
    /// Resta durabilidad al arma. Si llega a 0, se destruye y cambia al martillo
    /// </summary>
    /// <param name="cantidad"></param>
    public void restaDurTomahawk (int cantidad)
    {
        durActualTomahawk -= cantidad;
        if (durActualTomahawk <= 0)
        {
            scriptArmas.CambioArma(0);
            durActualTomahawk = durMaxTomahawk;
        }
    }
}
