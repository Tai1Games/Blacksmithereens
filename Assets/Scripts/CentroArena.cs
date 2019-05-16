using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentroArena : MonoBehaviour
{

    public UIManager ui; //referencia al arenaManager
    private int idTexto; //ID para encontrar el fragmento de texto para la nota al final de la ronda

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Cambia el ID de la arena al que recibe del ArenaManager.
    /// </summary>
    /// <param name="ID"> ID que identifica el fragmento de texto correspondiente </param>
    public void asignarIDTexto (int ID)
    {
        idTexto = ID;
    }

    /// <summary>
    /// Cuando colisiona con el jugador, se lo indica al arenaManager
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ControlJugador>())
        {
            if (idTexto != 0) LevelManager.instance.mostrarTextoFinRonda(idTexto); //Muestra la nota del final de la ronda
            else ui.EmpiezaCuntaAtras(); //Si no tiene ninguna nota asignada, empieza la siguiente ronda instantáneamente.
            this.gameObject.SetActive(false); //Desactiva el gameObject
        }
    }
}
