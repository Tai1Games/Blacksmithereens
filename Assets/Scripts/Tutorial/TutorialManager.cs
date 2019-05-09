using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public Image vidaConstainer, vida;
    public GameObject miniMapa, durabilidadArmas;
    public Text textoAnuncioRonda, materiales, ronda;
    public GameObject[] instrucciones;
    public GameObject enemigo, menuFinTutorial;


    private GameObject en;
    private int paso = 0; //indica por que paso del turorial va el jugador

	// Use this for initialization
	void Start () {
        vidaConstainer.enabled = false;
        vida.enabled = false;
        miniMapa.SetActive(false);
        durabilidadArmas.SetActive(false);
        textoAnuncioRonda.enabled = false;
        materiales.enabled = false;
        ronda.enabled = false;
        menuFinTutorial.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        switch (paso)
        {
            case 0: //movimiento
                instrucciones[paso].SetActive(true);
                if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 1: //vida
                instrucciones[paso].SetActive(true);
                vida.enabled = true;
                vidaConstainer.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 2: //rondas y materiales
                instrucciones[paso].SetActive(true);
                materiales.enabled = true;
                ronda.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 3: //martillo
                instrucciones[paso].SetActive(true);

                en = (GameObject)Instantiate(enemigo);
                DarMaterialesTutorial mat = en.GetComponent<DarMaterialesTutorial>();
                mat.SetTutorialManager(this);
                paso++;
                break;

            case 5: //abrir menu de crafteo
                instrucciones[paso].SetActive(true);
                if (Input.GetKeyDown("space"))
                {
                    instrucciones[paso-1].SetActive(false);
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 6: //craftear lanza
                instrucciones[paso].SetActive(true);
                durabilidadArmas.SetActive(true);
                break;

            case 7: //durabilidad
                instrucciones[paso].SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 8: //ataque
                instrucciones[paso].SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;
            case 9:
                if (en == null)
                {
                    menuFinTutorial.SetActive(true);
                    Time.timeScale = 0;  //desactiva el juego
                    GameManager.instance.CambiarPausa(true);
                    paso++;
                }
                break;
        }

	}


    public void MaterialesRecolectados()
    {
        instrucciones[paso-1].SetActive(false);
        instrucciones[paso].SetActive(true);
        paso++;

    }


    public void LanzaComprada()
    {
        instrucciones[paso ].SetActive(false);
        paso++;
    }
}
