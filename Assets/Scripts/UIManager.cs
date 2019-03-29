using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text textoMateriales;
    public Image barraVida;
    public CanvasPopUpMat popUpMatCanvas;
    public Text textoRondaEsquina;
    public Text textoRondaAnuncio;
    public Text cuentaAtras;
    public float tiempoCuentaAtras;
    public ArenaManager arenaManager;

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
        Debug.Log(texto);
        textoMateriales.text = materiales + "/" + materialesMax;
    }

    /// <summary>
    /// Crea un nuevo canvas en el mundo con el número de materiales nuevos.
    /// Lo destruye al finalizar la animación
    /// </summary>
    /// <param name="mat"></param> Cantidad de materiales a mostrar.
    /// <param name="pos"></param> Posición a la que los muestra.
    public void CreaPopUpMateriales (string mat, Vector2 pos)
    {
        CanvasPopUpMat newPopUpMatCanvas = Instantiate(popUpMatCanvas, pos, Quaternion.identity);
        newPopUpMatCanvas.CambiaParametrosTexto(mat); //Cambia el texto a el número de materiales nuevos.

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

    public void EmpiezaCuntaAtras()
    {
        StartCoroutine(CuentaAtras());
    }


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
        arenaManager.TocarCentro();

    }
}
