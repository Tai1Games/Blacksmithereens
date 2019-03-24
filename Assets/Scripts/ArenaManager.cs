using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Componente que controla los enemigos de la arena
/// </summary>
public class ArenaManager : MonoBehaviour
{
    public GameObject centroArena; //referencia al objeto que cambia de ronda
    public GameObject interfaz;

    private UIManager uim;


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
    [SerializeField]
    Ronda[] arena; //Array de rondas por arena

    private int eneMuertos = 0; //número de enemigos muertos en cierta oleada
    private bool finRonda = false; //Indica si han tocado el centro tras terminar la ronda

    void Start()
    {
        SpawnArena(arena, 0);
        uim = interfaz.GetComponent<UIManager>();
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
        
        //uim.ActualizaTextoRonda(i + 1);
        SpawnOleada(ronda[i].oleada, 0);
        StartCoroutine(FinOleada(ronda[i].oleada, ronda, i));

    }

    /// <summary>
    /// Spawnea la ronda que le indica el parametro "i"
    /// </summary>
    void SpawnArena(Ronda[] arena, int i)
    {
        centroArena.SetActive(false);
        finRonda = false;
        SpawnRonda(arena[i].ronda, 0);
        StartCoroutine(FinRonda(arena[i].ronda, arena, i));
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
        else centroArena.SetActive(true);
    }

    /// <summary>
    /// Espera a que se toque el centro de la arena y llama a la siguiente ronda
    /// Si no existe, vuelve al menú
    /// </summary>
    IEnumerator FinRonda(Oleada[] ronda, Ronda[] arena, int i)
    {
        print(i + 1);
        yield return new WaitUntil(() => finRonda);
        if (i + 1 < arena.Length) SpawnArena(arena, i + 1);
        else LevelManager.instance.VuelveaMenu();
    }
}