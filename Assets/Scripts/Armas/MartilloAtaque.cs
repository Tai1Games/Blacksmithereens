using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartilloAtaque : MonoBehaviour
{
    Animator animacion;  //Para acceder a la animación del martillo.

    // Use this for initialization
    void Start()
    {

        animacion = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Activa la animación del martillo para atacar.
    /// </summary>
    public void AtaqueMartillo()
    {
        if ((transform.localPosition.y < 0.095))  //Posicion relativa al jugador, para evitar ataques dobles
        {
            animacion.SetTrigger("Ataque");  //Trigger para activar la animación
        }
    }
}
