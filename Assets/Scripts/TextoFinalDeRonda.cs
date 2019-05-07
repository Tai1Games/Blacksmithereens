using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextoFinalDeRonda : MonoBehaviour {

    public string path = "Assets/Textos/test.txt";
    public GameObject panel;
    private StreamReader lectura;
    private Text texto;
    Animator animacion;

    // Use this for initialization
    void Start () {

        lectura = new StreamReader(path);
        animacion = panel.GetComponent<Animator>();
        texto = panel.GetComponentInChildren<Image>().GetComponentInChildren<Text>();

    }
	    
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("k")) activar(false);
		
	}

    public void activar (bool mostrar)
    {
        if (mostrar)
        {
            panel.SetActive(true);
            animacion.SetTrigger("Aparecer");
        }   
        else animacion.ResetTrigger("Aparecer");
    }

    public void desactivarFinalAnimacion()
    {
        panel.SetActive(false);
    }

    public void devuelveFragmento()
    {
        string lineaLeida = "", fragmento = "";

        while ((lineaLeida = lectura.ReadLine()) != "@" && !lectura.EndOfStream)
        {
            fragmento += "\n" + lineaLeida;
        }
        texto.text = fragmento;

        if (lectura.EndOfStream) lectura.Close();
    }

}
