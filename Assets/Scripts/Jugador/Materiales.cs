using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materiales : MonoBehaviour {

    /*
     * Método que controla los materiales del jugador: los suma, resta e informa de su valor actual
     */

    public int matMax = 200;

    int matActuales = 0;
    int matAux;

    private void Start()
    {
        matAux = matMax;
    }

    private void Update()
    {
        
    }

    /// <summary>
    /// Método que suma cierto número de materiales al jugador 
    /// </summary>
    /// <param name="matRecibidos"> Indica el número de materiales a sumar </param>
    public void SumarMateriales(int matRecibidos)
    {
        matActuales += matRecibidos;
        if (matActuales > matMax) matActuales = matMax;
        LevelManager.instance.ActualizaMateriales(matActuales, matMax);
    }

    /// <summary>
    /// /// Método que resta cierto número de materiales al jugador
    /// </summary>
    /// <param name="matUsados"> Indica el número de materiales a restar </param>
    public void RestarMateriales(int matUsados)
    {
        matActuales -= matUsados;
        if(matActuales<0) matActuales = 0;
        LevelManager.instance.ActualizaMateriales(matActuales, matMax);
    }

    /// <summary>
    /// Informa a quien lo llame del número de materiales actual del jugador (e.g. el ladrón)
    /// </summary>
    /// <returns></returns>
    public int DecirMateriales()
    {
        return matActuales;
    }


    /// <summary>
    /// suma 10000 materiales
    /// </summary>
    public void ActivaCheats(bool estado)
    {
        if(estado)matActuales = matMax = 10000;
        else matActuales = matMax = matAux;

        LevelManager.instance.ActualizaMateriales(matActuales, matMax);
    }
}
