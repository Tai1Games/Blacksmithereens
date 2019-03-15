using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//El script permite que la cámara siga al objeto en todo momento

public class MovimientoCamara : MonoBehaviour {
    public Transform siguiendo;
    //Distancia adicional para que la cámara no esté totalmente centrada
    //Iniciamos a 0 para que no de problemas si el objeto no está en escena.
    private Vector3 distancia = Vector3.zero;
    //La cámara seguirá al objeto si este bool está activo.
    bool activo = true; 

    void LateUpdate () {
        //Si activo es true, la cámara sigue la posición del objeto en todo momento hasta que el mismo desaparezca.
        if (siguiendo != null && activo) transform.position = new Vector3(siguiendo.transform.position.x + distancia.x, siguiendo.transform.position.y + distancia.y, -10);
        //siguiendo.transform.position + distancia;            
    }


    /// <summary>
    /// El método activa o desactiva el seguimiento de la cámara desde fuera.
    /// </summary>
    /// <param name=>"elegir true o false"</param>
    public void Activar (bool elige)
    {
        activo = elige;
    }

    /// <summary>
    /// Asigna un nuevo objetivo a seguir, además de la nueva distancia.
    /// </summary>
    /// <param name="objeto"></param>
    /// <param name="nuevadistancia"></param>
    public void AsignarSeguimiento (Transform objeto, Vector3 nuevaDistancia)
    {
        siguiendo = objeto;
        distancia = nuevaDistancia;
    }
}
