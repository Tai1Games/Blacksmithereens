using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Image barraVida;
	public Image barraDurabilidad;
	public CanvasPopUpText popUpMatCanvas;
    public Text textoRondaEsquina;
    public Text textoRondaAnuncio;
    public Text cuentaAtras;
    public float tiempoCuentaAtras;
    public ArenaManager arenaManager;
	public Text textoMateriales;
    public TextoFinalDeRonda textoFinalRonda;

	CambiaSprite cambiaSprite;
	Animator rondaEsquina;
    Animator rondaAnuncio;

    float barraMaxTamano;

    void Awake () {
        cuentaAtras.enabled = false;
        barraMaxTamano = barraVida.rectTransform.rect.width;
        rondaEsquina = textoRondaEsquina.GetComponent<Animator>();
        rondaAnuncio = textoRondaAnuncio.GetComponent<Animator>();
    }
    // Use this for initialization
    void Start () {
		cambiaSprite = GetComponent<CambiaSprite>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Cambia la ui para reflejar la vida del jugador
    /// </summary>
    /// <param name="vida">cantidad de vida que tiene el jugador</param>
    /// <param name="vidaMax">vida maxima del jugador</param>
    public void ActualizaVida(int vida, int vidaMax)
    {
        barraVida.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal,((float)vida / vidaMax * barraMaxTamano));
    }

    /// <summary>
    /// Cambia la ui para reflejar los materiales del jugador
    /// </summary>
    /// <param name="materiales">Cantidad de materiales</param>
    /// <param name="materialesMax">Cantidad maxima de materiales a tener</param>
    public void ActualizaMateriales(int materiales, int materialesMax)
    {
        string texto = materiales + "/" + materialesMax;
        textoMateriales.text = materiales + "/" + materialesMax;
    }

    /// <summary>
    /// Crea un nuevo canvas en el mundo con el número de materiales nuevos.
    /// Lo destruye al finalizar la animación
    /// </summary>
    /// <param name="mat"></param> Cantidad de materiales a mostrar.
    /// <param name="pos"></param> Posición a la que los muestra.
    public void CreaPopUpMateriales (string mat, Vector2 pos, Color color, Vector3 escala)
    {
        CanvasPopUpText newPopUpMatCanvas = Instantiate(popUpMatCanvas, pos, Quaternion.identity);
        newPopUpMatCanvas.CambiaParametrosTexto (mat, color, escala); //Cambia el texto a el número de materiales nuevos.

    }

    /// <summary>
    /// Actualiza el testo de la ronda (tanto el de la esquina como el del anuncio) y reproduce sus animaciones de aparición
    /// </summary>
    /// <param name="i"></param>
    public void ActualizaTextoRonda(int i)
    {
        textoRondaAnuncio.text = "Ronda " + i;
        textoRondaEsquina.text = "Ronda " + i;
    }

    /// <summary>
    /// Empieza el proceso de la cuenta atrás
    /// </summary>
    public void EmpiezaCuntaAtras()
    {
        StartCoroutine(CuentaAtras());
    }

    /// <summary>
    /// Activa la animacion de cuenta atrás
    /// </summary>
    public void AnuncioRonda()
    {
        rondaAnuncio.Play("AnimacionAnuncio", -1, 0);
        rondaEsquina.Play("AnimacionEsquina", -1, 0);
    }


    /// <summary>
    /// Inicia una cuenta atrás para el comienzo de la ronda
    /// </summary>
    private IEnumerator CuentaAtras()
    {
        //hay q actualizar cartel y empezar ronda

        arenaManager.TocarCentro();  //termina la ronda actual
        AnuncioRonda();
        cuentaAtras.enabled = true;
        cuentaAtras.text = "3";
        yield return new WaitForSeconds(tiempoCuentaAtras);
        cuentaAtras.text = "2";
        yield return new WaitForSeconds(tiempoCuentaAtras);
        cuentaAtras.text = "1";
        yield return new WaitForSeconds(tiempoCuentaAtras);
        cuentaAtras.text = "GO!";
        yield return new WaitForSeconds(tiempoCuentaAtras);
        cuentaAtras.enabled = false;
        arenaManager.EmpiezaRonda();  //empieza la proxima linea

    }
    
	/// <summary>
	/// Actualiza la barra de durabilidad con respecto al maximo y la actual durabilida del arma
	/// </summary>
	public void ActualizaDurabilidad(int max, int actual)
	{
		float angulo = (max - actual) * 136f / max;
		barraDurabilidad.rectTransform.localRotation = Quaternion.Euler(0, 0, angulo);
	}

	public void CambiaSprite(Armas arma)
	{
		cambiaSprite.CambiaSpriteUI(arma);
	}

    public void muestraTextoFinalRonda(int id)
    {
        textoFinalRonda.devuelveFragmento(id);
    }
}
