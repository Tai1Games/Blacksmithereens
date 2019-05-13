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
    private Materiales matJ;
    private bool cheats = false; //si es true los cheats están activos

    /// <summary>
    /// Método que se asegura de que solo haya un GameManager al mismo tiempo
    /// y de que no se destruya al cambiar de escena
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else Destroy(this.gameObject);

        ataquejugador = GOjugador.GetComponent<AtaqueJugador>();
        jugador = GOjugador.GetComponent<ControlJugador>();
        vidaJ = GOjugador.GetComponent<VidaJugador>();
        matJ = GOjugador.GetComponent<Materiales>();
    }
    void Start () {
		
	}
	   
	void Update () {
        if (Input.GetKeyDown("p"))
        {
            cheats = !cheats;  //cambia el estado de los cheats 
            if (cheats) //los activa
            {
                vidaJ.CheatsVida(true);
                ataquejugador.SubirDaño(true);
                matJ.ActivaCheats(true);
                LevelManager.instance.Reproducir(4);
            }
            else  //los desactiva
            {
                vidaJ.CheatsVida(false);
                ataquejugador.SubirDaño(false);
                matJ.ActivaCheats(false);
                LevelManager.instance.Reproducir(5);
                LevelManager.instance.Reproducir();
            }

            
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
    public void CambiarPausa(bool estado)
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
