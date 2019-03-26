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
    public LevelManager levelManager; //referencia al levelManager
    public float matBase; //materiales por pasarse una ronda de manera óptima
    public float factorMax; //tiempoFin * factorMax = tiempo a partir del cual recibes 0 materiales

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
        public float tiempoFin;
    }
    [SerializeField]
    Ronda[] arena; //Array de rondas por arena

    private int eneMuertos = 0; //número de enemigos muertos en cierta oleada
    private float tiempo = 0;
    private float tiempoFin = 0;
    private bool finRonda = false; //Indica si han tocado el centro tras terminar la ronda
    Stopwatch reloj = new Stopwatch();

    void Start()
    {
        SpawnArena(arena, 0);
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
    /// Indica el cambio de ronda cuando se toca el centro
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
        reloj.Start();
        SpawnOleada(ronda[i].oleada, 0);
        StartCoroutine(FinOleada(ronda[i].oleada, ronda, i));
    }

    /// <summary>
    /// Spawnea la ronda que le indica el parametro "i"
    /// </summary>
    void SpawnArena(Ronda[] arena, int i)
    {
        tiempoFin = arena[i].tiempoFin;
        centroArena.SetActive(false);
        finRonda = false;
        SpawnRonda(arena[i].ronda, 0);
        StartCoroutine(FinRonda(arena[i].ronda, arena, i));
    }

    void FormulaMateriales(float tiempo, Oleada[] ronda)
    {
        int mat = (int)((matBase - (matBase * (tiempo - tiempoFin) / (tiempoFin * (factorMax - 1)))) / (tiempo - tiempoFin + 1));
        UnityEngine.Debug.Log(mat);
        levelManager.SumarMateriales(mat);
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
        reloj.Stop();
        tiempo = 1000 * reloj.Elapsed.Seconds + reloj.Elapsed.Milliseconds;
        tiempo /= 1000;
        UnityEngine.Debug.Log(tiempo);
        FormulaMateriales(tiempo, ronda);
        reloj.Reset();
        if (i + 1 < ronda.Length) SpawnRonda(ronda, i + 1);
        else centroArena.SetActive(true);
    }

    /// <summary>
    /// Espera a que se toque el centro de la arena y llama a la siguiente ronda
    /// Si no existe, vuelve al menú
    /// </summary>
    IEnumerator FinRonda(Oleada[] ronda, Ronda[] arena, int i)
    {
        yield return new WaitUntil(() => finRonda);
        if (i + 1 < arena.Length) SpawnArena(arena, i + 1);
        else LevelManager.instance.VuelveaMenu();
    }
}