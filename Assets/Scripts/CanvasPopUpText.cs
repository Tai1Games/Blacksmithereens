using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasPopUpText : MonoBehaviour {

    private Text texto;
    private Animator animacion;
    
   

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}
    /// <summary>
    /// Cambia el texto a los materiales nuevos y se destruye al finalizar la animación
    /// </summary>
    /// <param name="mat">Nuevos materiales</param> 
    public void CambiaParametrosTexto(string mat, Color color, Vector3 escala)
    {
        texto = GetComponentInChildren<Text>();
        animacion = GetComponentInChildren<Animator>();

        //print(color);
        texto.text = mat; //Cambia el texto al número de materiales
        texto.color = color; //Cambia el color del texto
        texto.rectTransform.localScale = escala;  //Cambia el tamaño del texto
        //print(texto.color);

        AnimatorClipInfo[] clipInfo = animacion.GetCurrentAnimatorClipInfo(0); //Tiempo que tarda la animacion del texto en finalizar
        Destroy(gameObject, clipInfo[0].clip.length);  //El objeto se destruye al pasar el tiempo de animación
        
    }
}
