using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentroArena : MonoBehaviour
{

    public UIManager ui; //referencia al arenaManager

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Cuando colisiona con el jugador, se lo indica al arenaManager
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ControlJugador>())
        {
            LevelManager.instance.mostrarTextoFinRonda(true);
            ui.EmpiezaCuntaAtras();  //activa cuenta atras
            this.gameObject.SetActive(false);
        }
    }
}
