using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Componente que controla los enemigos de la arena
/// </summary>
public class ArenaManager : MonoBehaviour
{
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
    void Start()
    {
        SpawnRonda(arena[0].ronda, 0);
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
    /// Spawnea cierta oleada enemigo a enemigo
    /// </summary>
    void SpawnOleada(Spawn[] oleada)
    {
        eneMuertos = 0;
        for (int i = 0; i < oleada.Length; i++)
        {
            StartCoroutine(Espera(oleada,i));            
        }
    }

    /// <summary>
    /// Spawnea la oleada que le indica el parametro "i"
    /// </summary>
    void SpawnRonda(Oleada[] ronda, int i)
    {
        SpawnOleada(ronda[i].oleada);
        StartCoroutine(FinOleada(ronda[i].oleada, ronda, i));
    }

    /*void SpawnArena(Ronda[] arena, int i, int j)
    {
        SpawnRonda(arena[i].ronda, j)
    }*/

    /// <summary>
    /// Espera el tiempo de espera que tiene cada enemigo y lo spawnea
    /// </summary>
    IEnumerator Espera(Spawn[] oleada, int i)
    {
        yield return new WaitForSeconds(oleada[i].espera);
        Instantiate(oleada[i].tipo, oleada[i].puerta);
    }

    /// <summary>
    /// Espera a que estén todos los enemigos muertos y llama a la siguiente oleada
    /// </summary>
    IEnumerator FinOleada(Spawn[] oleada, Oleada[] ronda, int i)
    {
        yield return new WaitUntil(() => eneMuertos >= oleada.Length);
        if (i + 1 < ronda.Length) SpawnRonda(ronda, i + 1);
        else LevelManager.instance.VuelveaMenu();
    }
}