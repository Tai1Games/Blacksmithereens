using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

/// <summary>
/// Componente que controla los enemigos de la arena
/// </summary>
public class ArenaManagerEndless : MonoBehaviour
{
    public GameObject centroArena; //referencia al objeto que cambia de ronda
    public GameObject interfaz;
    public float tiempoEsperaPopUpMat = 1.5f;

    UIManager uim;
    int contador = 1;

    [SerializeField]
    Transform[] puertas = new Transform[4];
    [SerializeField]
    GameObject[] enemigos = new GameObject[5];

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
    }

    private bool empiezaRonda = false;
    private int eneMuertos = 0; //número de enemigos muertos en cierta oleada
    private bool finRonda = false; //Indica si han tocado el centro tras terminar la ronda

    void Start()
    {
        uim = interfaz.GetComponent<UIManager>();
        Ronda r = NewRonda(0);
        SpawnRonda(r.ronda, 0);
        StartCoroutine(FinRonda(r));
        uim.ActualizaTextoRonda(1);
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

    Ronda NewRonda(int contador)
    {
        int n = Random.Range(1, 3);
        Ronda r;
        r.ronda = new Oleada[n];
        for (int i = 0; i < r.ronda.Length; i++)
        {
            n = Random.Range(1, 4 + contador);
            UnityEngine.Debug.Log("n" + n);
            r.ronda[i].oleada = new Spawn[n];
            for (int j = 0; j < r.ronda[i].oleada.Length; j++)
            {
                n = Random.Range(0, puertas.Length);
                r.ronda[i].oleada[j].puerta = puertas[n];
                n = Random.Range(0, enemigos.Length);
                r.ronda[i].oleada[j].tipo = enemigos[n];
                float m = Random.Range(0.0f, 2.5f);
                r.ronda[i].oleada[j].espera = m;
            }
        }
        return r;
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

    void FormulaMateriales()
    {
        int mat = 30;
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
            centroArena.SetActive(true);
            FormulaMateriales(); //suma materiales
        }
    }

    /// <summary>
    /// Espera a que se toque el centro de la arena y llama a la siguiente ronda
    /// Si no existe, vuelve al menú
    /// </summary>
    IEnumerator FinRonda(Ronda r)
    {
        //Al tocar el centro el jugador se sana       
        //Empieza la siguiente ronda
        yield return new WaitUntil(() => finRonda);
        LevelManager.instance.Jugador().GetComponent<VidaJugador>().SumaVida(1000);
        contador++; //Incrementa el indicador de ronda actual
        uim.ActualizaTextoRonda(contador); //Llama al método de UIManager que actualiza los textos de ronda
        yield return new WaitUntil(() => empiezaRonda);  //hasta que no termina la cuenta atrás no empieza la proxima ronda
        r = NewRonda(contador);
        finRonda = false;
        SpawnRonda(r.ronda, 0);
        StartCoroutine(FinRonda(r));
        empiezaRonda = false;
    }

    /// <summary>
    /// Cuando es llamado empieza una nueva ronda
    /// </summary>
    public void EmpiezaRonda()
    {
        empiezaRonda = true;
    }
}