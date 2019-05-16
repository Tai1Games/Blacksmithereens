using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class TextoFinalDeRonda : MonoBehaviour {


    private Text texto;
    Animator animacion;
    private UIManager ui; //Referencia al UI Manager
    AtaqueJugador ataque; //Referencia al script ataque Jugador

    // Use this for initialization
    void Start () {
        
    }
	    
	// Update is called once per frame
	void Update () {

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

        animacion.SetTrigger("Aparecer"); //Activa la animación para que aparezca en pantalla el texto.
        LevelManager.instance.jugador.GetComponent<AtaqueJugador>().enabled = false; //Desactiva el ataque del jugador para que al hacer click para descartar la nota no haga un ataque

        texto.text = guardadoTexto(id); //Cambia el texto
    }

    private string guardadoTexto (int i)
    {
        switch (i)
        {
            case 1:
                return ("Veo que al final has acabado en la arena por mi culpa...\nLo siento, pero no podre hacer nada por ti hasta que hayas derrotado a todos los enemigos que el torneo tiene preparados...Pero confio en que podras escapar.Entonces, te explicare todo con mas calma, vale? Ten mucho cuidado.\nEspero que algun dia puedas perdonarme.\n-AM");
                
            case 2:
                return ("Lo estas haciendo muy bien!\nSegun mi informacion, esta sera la ultima ronda que tendras que sobrevivir para acabar con esto.Y despues, destruiremos este injusto torneo para siempre.Aguanta!\n\n- AM");

            case 3:
                return ("Ya ha pasado lo peor... Ahora solo tienes que derrotar a tu ultimo oponente y el trofeo sera tuyo... El trofeo sera NUESTRO\n\n- AM");

            case 4:
                return ("Pensandolo mejor, por que conformarme con que el trofeo sea nuestro? Es decir, nadie se daria cuenta si de repente liberara a todas las bestias que quedan aqui dentro, no?\nGracias por la ayuda, pero no puedo dejarte marchar. Bienvenido a la autentica batalla!\n\n-AM");

            case 5:
                return ("Nunca mas tendre que matar a nadie mas en esta arena si me hago con todo ese oro. Podre acabar con este sistema corrupto. \nSOLO TU ERES MI PRECIO A PAGAR POR ELLO.\nAPARTA DE MI CAMINO. SOLO ESTAS AQUI PARA SUFRIR.Y VAYA QUE SI LO VAS A HACER.\n\n-AM");

            case 6:
                return ("Deberias dejar de confiar en esos materiales tan malos que usas... Aunque no eres NADIE sin ellos.\nA ver como sobrevives ahora.\n\n- AM");

            case 7:
                return ("Hoy no tenia ganas de utilizar toda mi energia, pero en vista de que mis anteriores ataques no te afectaron...\nNo me dejas otra opcion mas que tomar medidas drasticas.\n\n-AM");

            case 8:
                return ("No... no puede ser... he de seguir... instanciando enemigos...\n\n-public GameObject(AM)");

            case 9:
                return ("Has roto el sistema de una manera... de la que jamas pense que podria romperse...\nComo lo has hecho? No suponia que -\n\n-AM");

            case 10:
                return ("Ahi esta tu premio. Tu derecho a salir de aqui. De volver al menu principal. De volver a batirte contra mi si quieres. Merecio la pena? Puedes salir de aqui, pero tus propios miedos ya estan dentro de tu cabeza.\n(Te recomiendo el modo endless.Corre a probarlo)\nGracias por jugar a Blown To Blacksmithereens!");
            default: return "";
        }
    }

}
