using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingBongPong : MonoBehaviour {

    //Izquierda y derecha representan la posición de ambos cuerpos del enemigo
    public Transform izquierda;
    public Transform derecha;
    public float separacion; //La separación que tendrán los cuerpos del centro de la arena
    public int velocidadRotacion;
    public int velocidadCabeza;

    private int cuerpoMuerto = 0; //Esta variable lleva la cuenta de cuerpos vencidos por el jugador
    CabezaPong cabeza;

	// Use this for initialization
	void Start () {
        transform.position = Vector3.zero; //Se resetea la posición
        //Se ajustan las posiciones de ambos cuerpos
        izquierda.position = new Vector3(-separacion, 0, 0);
        derecha.position = new Vector3(separacion, 0, 0);
        cabeza = GetComponentInChildren<CabezaPong>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, 0, velocidadRotacion*Time.deltaTime); //Se rota el enemigo
	}

    /// <summary>
    /// Suma la cuenta de cuerpos vencidos por el jugador. Si los dos han sido vencidos, el enemigo muere.
    /// </summary>
    public void MuereCuerpo()
    {
        //Se duplica la velocidad de la cabeza y de rotación de los cuerpos
        velocidadRotacion *= 2;
        cabeza.VariaVelCabeza(2);
        cuerpoMuerto++; //Se suma uno a la cuenta de cuerpos derrotados
        if (cuerpoMuerto >= 2) //Si se han derrotado los dos cuerpos, el enemigo muere
            this.GetComponent<MuerteEnemigo>().Muerte();
    }

    /// <summary>
    /// Este método devuelve la velocidad asignada a la cabeza del enemigo
    /// </summary>
    /// <returns></returns>
    public int VelocidadCabeza()
    {
        return velocidadCabeza;
    }
}
