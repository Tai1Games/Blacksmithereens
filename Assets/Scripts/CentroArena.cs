using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentroArena : MonoBehaviour
{

    public UIManager ui; //referencia al arenaManager
    private int idTexto;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

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
            if (idTexto != 0) LevelManager.instance.mostrarTextoFinRonda(idTexto);
            else ui.EmpiezaCuntaAtras();
            this.gameObject.SetActive(false);
        }
    }
}
