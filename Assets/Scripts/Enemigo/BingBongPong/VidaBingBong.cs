using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Maneja la vida del enemigo
 */
public class VidaBingBong : MonoBehaviour {
    public int vidaEnemigo;

    int vidaActual;
    BingBongPong padre;
    SpriteRenderer renderer;


    // Use this for initialization
    void Start()
    {
        vidaActual = vidaEnemigo;
        padre = GetComponentInParent<BingBongPong>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Resta un numero a la vida del eneimgo
    /// </summary>
    /// <param name="cantidad">Cantidad a restar a la vida</param>
    /// <param name="jugador">Referencia del jugador</param>
    public void RestaVida(int cantidad)
    {
        vidaActual = vidaActual - cantidad;
        if (vidaActual <= 0 && padre)
        {
            if (renderer)
                renderer.color = Color.grey; //Se cambia el color del cuerpo a gris
            padre.MuereCuerpo(); //Se le informa de la muerte al padre
            this.enabled = false; //Se desactiva este componente para no poder vencer al enemigo de nuevo
        }
        else
            LevelManager.instance.MuestraPopUpMat(cantidad.ToString(), new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z), Color.red, new Vector3(1, 1, 0));
    }
}
