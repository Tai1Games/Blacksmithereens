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

    Animator rondaEsquina;
    Animator rondaAnuncio;

    float barraMaxTamano;

	// Use this for initialization
	void Start () {
        barraMaxTamano = barraVida.rectTransform.rect.width;
        rondaEsquina = textoRondaEsquina.GetComponent<Animator>();
        rondaAnuncio = textoRondaAnuncio.GetComponent<Animator>();
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

    public void ActualizaTextoRonda(int i)
    {
        rondaAnuncio.Play("AnuncioRonda", -1, 0);
        rondaEsquina.Play("AnuncioRonda", -1, 0);
        textoRondaAnuncio.text = "Ronda " + i;
        textoRondaEsquina.text = "Ronda " + i;
    }
}
