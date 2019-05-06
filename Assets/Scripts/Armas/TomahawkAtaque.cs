using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomahawkAtaque : MonoBehaviour {


    public GameObject tomahawkLanzado; //prefab del gameobject que lanzaremos
    public float velocidad; //velocidad del objeto creado
    private Vector2 mouse_position;
    private Vector2 offset, screenPoint;
    
    AtaqueJugador scriptArmas;
    GameObject tomLanzado;

    public int durMaxTomahawk = 3; //durabilidad del tomahawk
    private int durActualTomahawk;

    // Use this for initialization
    void Start () {
        
        durActualTomahawk = durMaxTomahawk;  //asignamos la durabilidad del tomahawk
        scriptArmas = LevelManager.instance.Jugador().GetComponent<AtaqueJugador>();
    
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    /// <summary>
    /// Instancia un tomahawk arrojable y resta 1 punto de durabilidad al arma.
    /// </summary>
    public void LanzarTomahawk()
    {

        if (transform.localPosition.y < 0.09768)      //Posicion relativa al jugador, para evitar ataques dobles
        {
            tomLanzado = (GameObject)Instantiate(tomahawkLanzado, transform); //crea la lanza que va a ser lanzada
            tomLanzado.GetComponent<SpriteRenderer>().sortingOrder = 1; //cambia la sortingLayer
            tomLanzado.transform.parent = null;  //elimina el padre de la lanzaLanzada para evitar que rote con el jugador

            Vector2 offset = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); //Vector entre el mouse y el jugador
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; //transforma a ángulos 
            angle -= 90; //el sprite temporal no está rotado adecuadamente
            tomLanzado.transform.rotation = Quaternion.Euler(0, 0, angle); //aplica la rotación

            tomLanzado.GetComponent<Rigidbody2D>().velocity = Vector2.ClampMagnitude(offset, velocidad); //impulsa la lanza
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
		else
			LevelManager.instance.ActualizaDurabilidad(durMaxTomahawk, durActualTomahawk);
	}

    /// <summary>
    /// Resetea al valor maximo la durabilidad del tomahawk
    /// </summary>
    public void ReseteaDurTomahawk()
    {
        durActualTomahawk = durMaxTomahawk;
    }

    /// <summary>
    /// Sube el numero de unidades disponibles a 10000
    /// </summary>
    public void ActivaCheats()
    {
        durActualTomahawk = durMaxTomahawk = 10000;
    }

}
