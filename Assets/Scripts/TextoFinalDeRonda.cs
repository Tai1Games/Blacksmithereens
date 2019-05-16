using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextoFinalDeRonda : MonoBehaviour
{

    private string path = "Assets/Textos/test.txt"; //Localización del archivo texto
    private StreamReader lectura;
    private Text texto;
    Animator animacion;
    private UIManager ui; //Referencia al UI Manager
    AtaqueJugador ataque; //Referencia al script ataque Jugador

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Se usa para pausar el juego para dejar que el jugador lea el texto de la nota. Se activa al final de la animación Aparecer de la nota.
    /// </summary>
    /// <param name="var"> Si está a 0, el juego se pausa por completo. En 1 funciona a velocidad normal. </param> 
    public void setTime(int var)
    {
        Time.timeScale = var;
    }

    /// <summary>
    /// Lanza un trigger para activar la animación de Desaparecer de la nota. Se activa desde el botón que contiene la misma.
    /// </summary>
    public void closeText()
    {
        animacion.ResetTrigger("Aparecer");
        LevelManager.instance.jugador.GetComponent<AtaqueJugador>().enabled = true; //Para que el click en el botón no sea registrado también como un ataque
        setTime(1); //Cambia la velocidad de juego a la normal.
    }

    /// <summary>
    /// Hace desaparecer los elementos de UI de la nota y hace empezar la siguiente ronda. Se activa al final de la animación de Desaparecer de la nota.
    /// </summary>
    public void dissapear()
    {
        ui = GetComponentInParent<UIManager>();
        ui.EmpiezaCuntaAtras();  //activa cuenta atras
        this.gameObject.SetActive(false);
    }


    /// <summary>
    /// Busca el fragmento de texto asociado al ID que recibe y cambia el texto de la nota correspondientemente.
    /// </summary>
    /// <param name="id"> ID que identifica al fragmento de texto.</param>
    public void devuelveFragmento(int id)
    {
        animacion = GetComponent<Animator>(); //controlador de animaciones de la nota.
        texto = this.gameObject.GetComponentInChildren<Image>().GetComponentInChildren<Text>(); //Texto que queremos modificar
        this.gameObject.SetActive(true); //Activa el gameobject (si no lo usamos permanecerá desactivado)
        lectura = new StreamReader(path); //Lector de texto

        animacion.SetTrigger("Aparecer"); //Activa la animación para que aparezca en pantalla el texto.
        LevelManager.instance.jugador.GetComponent<AtaqueJugador>().enabled = false; //Desactiva el ataque del jugador para que al hacer click para descartar la nota no haga un ataque
        string lineaLeida = "", fragmento = "";

        while (lineaLeida != id.ToString() && !lectura.EndOfStream) //Lee hasta que encuentra el ID
        {
            lineaLeida = lectura.ReadLine();
        }

        if (lineaLeida == id.ToString()) //Si lo encuentra, guarda todo el fragmento (saltos de línea incluidos) en el string fragmento
        {
            lineaLeida = "";
            while ((lineaLeida = lectura.ReadLine()) != id.ToString() && !lectura.EndOfStream) //Lee hasta que encuentra el ID
            {
                fragmento += "\n" + lineaLeida;
            }
        }
        texto.text = fragmento; //Cambia el texto
        lectura.Close(); //Cierra la lectura.
    }

}
