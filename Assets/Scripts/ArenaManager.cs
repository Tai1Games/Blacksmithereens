using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

/// <summary>
/// Componente que controla los enemigos de la arena
/// </summary>
public class ArenaManager : MonoBehaviour
{
    public GameObject centroArena; //referencia al objeto que cambia de ronda
    public float matBase; //materiales por pasarse una ronda de manera óptima
    public float factorMax; //tiempoFin * factorMax = tiempo a partir del cual recibes 0 materiales
    public GameObject interfaz;
    public float tiempoEsperaPopUpMat = 1.5f;

    UIManager uim;
    int contador = 1;
    [System.Serializable]

    struct Spawn //Instancia de enemigo
    {
        public Transform puerta; //punto donde aparece el enemigo
        public GameObject tipo; //prefab de enemigo
        public float espera; //tiempo de espera desde que apareció el anterior
    }
    [System.Serializable]
    struct Oleada //Array de enemigos por oleada
    {
        [SerializeField]
        public Spawn[] oleada;
    }
    [System.Serializable]
    struct Ronda //Array de oleadas por ronda
    {
        [SerializeField]
        public Oleada[] ronda;
        public float tiempoFin; //tiempo objetivo con el que pasarse una ronda
        public int idNota; //Nota que será mostrada al final de la ronda
    }
    [SerializeField]
    Ronda[] arena; //Array de rondas por arena

    private bool empiezaRonda=false;
    private int eneMuertos = 0; //número de enemigos muertos en cierta oleada
    private float tiempo = 0; //tiempo que el jugador tarda en pasarse una ronda
    private float tiempoFin = 0; //variable auxiliar para Ronda.tiempoFin
    private bool finRonda = false; //Indica si han tocado el centro tras terminar la ronda
    Stopwatch reloj = new Stopwatch(); //Reloj que mide el tiempo en tardarse una ronda

    void Start()
    {
        uim = interfaz.GetComponent<UIManager>();
        SpawnArena(arena, 0);
        uim.ActualizaTextoRonda(1);
        ReproduceMusica();
    }

    void Update()
    {

    }

    /// <summary>
    /// Aumenta en uno el número de enemigos muertos
    /// </summary>
    public void EnemigoMuerto()
    {
        eneMuertos++;
    }

    /// <summary>
    /// Indica que se h terminado una ronda
    /// </summary>
    public void TocarCentro()
    {
        finRonda = true;
    }

    /// <summary>
    /// Spawnea el enemigo que le inidica el parametro "i"
    /// </summary>
    void SpawnOleada(Spawn[] oleada, int i)
    {
        if (i == 0) eneMuertos = 0;
        StartCoroutine(Espera(oleada, i));
    }

    /// <summary>
    /// Spawnea la oleada que le indica el parametro "i"
    /// </summary>
    void SpawnRonda(Oleada[] ronda, int i)
    {    
        SpawnOleada(ronda[i].oleada, 0);
        StartCoroutine(FinOleada(ronda[i].oleada, ronda, i));
    }

    /// <summary>
    /// Spawnea la ronda que le indica el parametro "i"
    /// </summary>
    void SpawnArena(Ronda[] arena, int i)
    {
        reloj.Start(); //Empieza el reloj cuando empieza la ronda
        tiempoFin = arena[i].tiempoFin; //paso de variable auxiliar por temas de eficiencia        
        finRonda = false;
        SpawnRonda(arena[i].ronda, 0);
        StartCoroutine(FinRonda(arena[i].ronda, arena, i));
    }

    void FormulaMateriales(float tiempo, Oleada[] ronda)
    {
        if (tiempoFin == 0) tiempoFin = 10; //por si a ciertos desarrolladores se les olvida poner el tiempoFin
        if (tiempo < tiempoFin) tiempo = tiempoFin; //para que matBase sea el límite de materiales recibidos 
        //formula exponencial que da matBase materiales si se pasa una ronda en el tiempo óptimo y 0 si se pasa en (tiempo óptimo * factorMax)
        int mat = (int)((matBase - (matBase * (tiempo - tiempoFin) / (tiempoFin * (factorMax - 1)))) / (tiempo - tiempoFin + 1));
        StartCoroutine(MuestraPopUp(mat));
    }

    public IEnumerator MuestraPopUp(int mat)
    {
        yield return new WaitForSeconds(tiempoEsperaPopUpMat);
        LevelManager.instance.SumarMateriales(mat);
        Vector3 posicionJugador = LevelManager.instance.Jugador().transform.position;
        LevelManager.instance.MuestraPopUpMat("+ " + mat, new Vector3(posicionJugador.x, posicionJugador.y, posicionJugador.z), Color.black, new Vector3(3, 3, 1));
    }

    /// <summary>
    /// Espera el tiempo de espera que tiene cada enemigo y lo spawnea
    /// </summary>
    IEnumerator Espera(Spawn[] oleada, int i)
    {
        yield return new WaitForSeconds(oleada[i].espera);
        Instantiate(oleada[i].tipo, oleada[i].puerta);
        if (i + 1 < oleada.Length) SpawnOleada(oleada, i + 1);
    }

    /// <summary>
    /// Espera a que estén todos los enemigos muertos y llama a la siguiente oleada
    /// </summary>
    IEnumerator FinOleada(Spawn[] oleada, Oleada[] ronda, int i)
    {
        yield return new WaitUntil(() => eneMuertos >= oleada.Length);
        if (i + 1 < ronda.Length) SpawnRonda(ronda, i + 1);
        //Si no se comprueba, es que ha acabado la ronda
        else
        {
            centroArena.SetActive(true);  //Activa el centro de la arena para pasar de ronda.
            centroArena.GetComponent<CentroArena>().asignarIDTexto(arena[contador-1].idNota); //Asigna al centro de la arena el ID de la ronda para mostrar el texto.
            reloj.Stop(); //para el reloj cuando se termina la ronda
            tiempo = 1000 * reloj.Elapsed.Seconds + reloj.Elapsed.Milliseconds; //toma los segundos y milisegundos y los guarda
            tiempo /= 1000;
            UnityEngine.Debug.Log(tiempo);
            FormulaMateriales(tiempo, ronda); //suma materiales
            reloj.Reset(); //reloj a 0
        }
    }

    /// <summary>
    /// Espera a que se toque el centro de la arena y llama a la siguiente ronda
    /// Si no existe, vuelve al menú
    /// </summary>
    IEnumerator FinRonda(Oleada[] ronda, Ronda[] arena, int i)
    {       
        //Empieza la siguiente ronda
        yield return new WaitUntil(() => finRonda);
        //Al tocar el centro el jugador se sana
        LevelManager.instance.Jugador().GetComponent<VidaJugador>().SumaVida(1000);
        if (i + 1 < arena.Length)
        {
            contador++; //Incrementa el indicador de ronda actual
            uim.ActualizaTextoRonda(contador);
            if (contador == 5)
                LevelManager.instance.ActivaGrada();
            ReproduceMusica();
            yield return new WaitUntil(() => empiezaRonda);  //hasta que no termina la cuenta atrás no empieza la proxima ronda
            SpawnArena(arena, i + 1);
            empiezaRonda = false;   
        }
        else GameManager.instance.CargaEscena("MenuGanarG");
    }


    /// <summary>
    /// Cuando es llamado empieza una nueva ronda
    /// </summary>
    public void EmpiezaRonda()
    {
        empiezaRonda = true;
    }

    /// <summary>
    /// Le envía una orden de reproducir música al LevelManager dependiendo de la ronda en la que se encuentra el jugador.
    /// </summary>
    public void ReproduceMusica()
    {
        if (contador >= 1 && contador < 5)
            LevelManager.instance.Reproducir(1);
        else if (contador >= 5 && contador < 8)
            LevelManager.instance.Reproducir(2);
        else if (contador >= 9)
            LevelManager.instance.Reproducir(3);
    }
}