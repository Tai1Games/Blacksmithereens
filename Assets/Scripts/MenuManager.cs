using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Este método envía el nombre seleccionado de la escena al GameManager, que con la librería SceneManagement
    /// se encargará de cambiar la ecena.
    /// </summary>
    /// <param name="escena"></param>
    public void CambiaEscena(string escena)
    {
        GameManager.instance.CargaEscena(escena);
    }

    /// <summary>
    /// Envía al GameManager la orden para que ejecute la salida del juego.
    /// </summary>
    public void OrdenSalida()
    {
        GameManager.instance.Salir();
    }
}
