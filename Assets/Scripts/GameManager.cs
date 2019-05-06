using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Componente que se ocupa de organizar el código y las llamadas entre componentes
/// Todos los scripts tienen acceso a él
/// </summary>
public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public GameObject GOjugador;
    public CrafteoArmas menuArmas;

    private ControlJugador jugador;
    private AtaqueJugador ataquejugador;
    private bool juegoPausado = false;
    private VidaJugador vidaJ;

    /// <summary>
    /// Método que se asegura de que solo haya un GameManager al mismo tiempo
    /// y de que no se destruya al cambiar de escena
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        ataquejugador = GOjugador.GetComponent<AtaqueJugador>();
        jugador = GOjugador.GetComponent<ControlJugador>();
        vidaJ = GOjugador.GetComponent<VidaJugador>();
    }
    void Start () {
		
	}
	   
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            vidaJ.CheatsVida();
            ataquejugador.SubirDaño();
        }
	}

    /// <summary>
    /// Este método cambia la escena actual a aquella especificada en el string escena.
    /// </summary>
    /// <param name="escena"></param>
    public void CargaEscena(string escena)
    {
        SceneManager.LoadScene(escena);
    }

    /// <summary>
    /// Este método se encarga de cargar la escena de final de partida
    /// </summary>
    public void GameOver()
    {
        CargaEscena("GameOverMenu");
    }

    /// <summary>
    /// Sale del juego.
    /// </summary>
    public void Salir()
    {
        Application.Quit();
    }

    /// <summary>
    /// Devuelve si el juego está pausado o no
    /// </summary>
    public bool Pausa()
    {
        return juegoPausado;
    }

    /// <summary>
    /// Cambia el esto del juego en cuanto a la pausa
    /// </summary>
    public void CambiarPausa( bool estado)
    {
        juegoPausado = estado;
        if (juegoPausado)
        {
            ataquejugador.enabled = false;
            jugador.enabled = false;
            menuArmas.enabled = false;
        }
        else
        {
            ataquejugador.enabled = true;
            jugador.enabled = true;
            menuArmas.enabled = true;

        }
    }
}
