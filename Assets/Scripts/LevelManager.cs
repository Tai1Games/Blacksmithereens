using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controla las referencias pero no perdura entre escenas
/// </summary>
public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;
    public GameObject jugador;
    public UIManager uiManager;
    public ArenaManager arenaManager;

    /// <summary>
    /// Método que se asegura de que solo haya un GameManager al mismo tiempo
    /// </summary>
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(this.gameObject);
    }
    void Start () {
		
	}
	
	void Update ()
    {
	}

    /// <summary>
    /// Suma materiales al jugador
    /// </summary>
    public void SumarMateriales(int cantidad)
    {
        Materiales mat = jugador.GetComponent<Materiales>();
        if (mat) mat.SumarMateriales(cantidad);
    }

    /// <summary>
    /// Aumenta en uno el número de enemigos muertos
    /// </summary>
    public void EnemigoMuerto()
    {
        arenaManager.EnemigoMuerto();
    }

    /// <summary>
    /// Método para que los otros componentes tengan acceso al jugador
    /// </summary>
    /// <returns></returns>
    public GameObject Jugador()
    {
        return jugador;
    }

    /// <summary>
    /// Cambia la ui para reflejar la vida del jugador
    /// </summary>
    /// <param name="vida">cantidad de vida que tiene el jugador</param>
    /// <param name="vidaMax">vida maxima del jugador</param>
    public void ActualizaVida(int vida, int vidaMax)
    {
        uiManager.ActualizaVida(vida, vidaMax);
    }

    /// <summary>
    /// Cambia la ui para reflejar los materiales del jugador
    /// </summary>
    /// <param name="materiales">Cantidad de materiales</param>
    /// <param name="materialesMax">Cantidad maxima de materiales a tener</param>
    public void ActualizaMateriales(int materiales, int materialesMax)
    {
        uiManager.ActualizaMateriales(materiales, materialesMax);
    }

	/// <summary>
	/// Cambia la ui para reflejar la durabilidad del arma
	/// </summary>
	public void ActualizaDurabilidad(int max, int actual)
	{
		uiManager.ActualizaDurabilidad(max, actual);
	}

	public void CambiaSpriteUI(Armas arma)
	{
		uiManager.CambiaSprite(arma);
	}

    /// <summary>
    /// Dice al UIManager que muestre los nuevos materiales obtenidos
    /// </summary>
    /// <param name="mat"></param> Materiales a mostrar en pantalla
    /// <param name="pos"></param> Posición a la que se quieren enseñar
    public void MuestraPopUpMat(string mat, Vector2 pos, Color color, Vector3 escala)
    {
        uiManager.CreaPopUpMateriales(mat, pos, color, escala);
    }

    public void VuelveaMenu()
    {
        SceneManager.LoadScene("Menu 1");
    }
}
