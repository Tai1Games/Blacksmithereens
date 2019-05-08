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

        animacion = panel.GetComponent<Animator>();
        texto = panel.GetComponentInChildren<Image>().GetComponentInChildren<Text>();

    }
	    
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("k")) animacion.ResetTrigger("Aparecer"); ;	
	}

    public void desactivarFinalAnimacion()
    {
        panel.SetActive(false);
    }

    public void devuelveFragmento(int id)
    {
        lectura = new StreamReader(path);
        panel.SetActive(true);
        animacion.SetTrigger("Aparecer");
        string lineaLeida = "", fragmento = "";
        Debug.Log("id" +id);
        while (lineaLeida != id.ToString() && !lectura.EndOfStream)
        {
            lineaLeida = lectura.ReadLine();
        }
        Debug.Log(lineaLeida);

        if (lineaLeida == id.ToString())
        {
            lineaLeida = "";
            while ((lineaLeida = lectura.ReadLine()) != id.ToString() && !lectura.EndOfStream)
            {
                fragmento += "\n" + lineaLeida;
            }
        }
        Debug.Log(fragmento);
        texto.text = fragmento;
        lectura.Close();
    }

}
