using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Este script se encarga del funcionamiento del menu de crafteo de armas
/// </summary>
public class CrafteoArmas : MonoBehaviour {

    public GameObject menuArmas;
    public int CosteTomahawk = 30, CosteEspada = 70, CosteLanza = 70;
    
    bool menuActivo = false;
    Materiales scriptMateriales;
    AtaqueJugador scriptArmas;

	// Use this for initialization
	void Start () {
        scriptMateriales = LevelManager.instance.Jugador().GetComponent<Materiales>();
        scriptArmas = LevelManager.instance.Jugador().GetComponent<AtaqueJugador>();
	}
	
	// Update is called once per frame
	void Update () {
       
            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire3")) //abre y cierra el menu con el espacio y la rueda del raton
            {
                menuActivo = !menuActivo;
                menuArmas.SetActive(menuActivo);
            }
        

	}
    public void CraftearLanza()
    {
        if (scriptMateriales.DecirMateriales() >= CosteLanza)
        {
            scriptMateriales.RestarMateriales(CosteLanza);
            menuActivo = !menuActivo;
            menuArmas.SetActive(menuActivo);

            scriptArmas.CambioArma(Armas.Lanza);

            Debug.Log("Crafteando Lanza");
        }
    }

    public void CraftearEspada() {
        if (scriptMateriales.DecirMateriales() >= CosteEspada)
        {
            scriptMateriales.RestarMateriales(CosteEspada);
            menuActivo = !menuActivo;
            menuArmas.SetActive(menuActivo);

            scriptArmas.CambioArma(Armas.Espada);

            Debug.Log("Crafteando Espada");
        }
    }

    public void CraftearTomahawk()
    {
        if (scriptMateriales.DecirMateriales() >= CosteTomahawk)
        {
            scriptMateriales.RestarMateriales(CosteTomahawk);
            menuActivo = !menuActivo;
            menuArmas.SetActive(menuActivo);
            scriptArmas.CambioArma(Armas.Tomahawk);
        }
    }


}
