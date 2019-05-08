using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextoFinalDeRonda : MonoBehaviour {

    public string path = "Assets/Textos/test.txt";
    private StreamReader lectura;
    private Text texto;
    Animator animacion;
    private UIManager ui;
    AtaqueJugador ataque;

    // Use this for initialization
    void Start () {
        
    }
	    
	// Update is called once per frame
	void Update () {

	}

    public void desactivarFinalAnimacion()
    {
        this.gameObject.SetActive(false);
    }

    public void setTime(int var)
    {
        Time.timeScale = var;
    }

    public void closeText()
    {
        animacion.ResetTrigger("Aparecer");
        LevelManager.instance.jugador.GetComponent<AtaqueJugador>().enabled = true;
        setTime(1);
    }

    public void dissapear()
    {
        ui = GetComponentInParent<UIManager>();
        ui.EmpiezaCuntaAtras();  //activa cuenta atras
        this.gameObject.SetActive(false);
    }


    public void devuelveFragmento(int id)
    {
        animacion = GetComponent<Animator>();
        texto = this.gameObject.GetComponentInChildren<Image>().GetComponentInChildren<Text>();
        this.gameObject.SetActive(true);
        lectura = new StreamReader(path);

        animacion.SetTrigger("Aparecer");
        LevelManager.instance.jugador.GetComponent<AtaqueJugador>().enabled = false;
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
