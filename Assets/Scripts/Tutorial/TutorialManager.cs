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
    public GameObject charger, ladron, lancero;


    private GameObject en, lan, cha,lad;
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
            case 0: //1
                instrucciones[paso].SetActive(true);
                if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 1: //2
                instrucciones[paso].SetActive(true);
                vida.enabled = true;
                vidaConstainer.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 2: //3
                instrucciones[paso].SetActive(true);
                materiales.enabled = true;
                ronda.enabled = true;
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 3: //4
                instrucciones[paso].SetActive(true);

                en = Instantiate(enemigo);
                DarMaterialesTutorial mat = en.GetComponent<DarMaterialesTutorial>();
                mat.SetTutorialManager(this);
                paso++;
                break;

            //no hay 4 por que queda en funcion del enemigo, cuando ha soltado los materiales llama al método MaterialesRecolectados

            case 5: //4.5
                instrucciones[paso].SetActive(true);
                if (Input.GetKeyDown("space"))
                {
                    instrucciones[paso-1].SetActive(false);
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 6: //5
                instrucciones[paso].SetActive(true);
                durabilidadArmas.SetActive(true);
                break;

            case 7: //6
                instrucciones[paso].SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;

            case 8: //7
                instrucciones[paso].SetActive(true);
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(false);
                    paso++;
                }
                break;
            case 9: //8
                if (en == null)
                {
                    instrucciones[paso].SetActive(true);
                    cha = Instantiate(charger);
                    paso++;
                }              
                break;
            case 10: //9
                if(cha == null)
                {
                    instrucciones[paso-1].SetActive(false);
                    lad = Instantiate(ladron);
                    instrucciones[paso].SetActive(true);
                    paso++;
                }
                break;
            case 11: //10
                if (lad == null)
                {
                    instrucciones[paso-1].SetActive(false);
                    lan = Instantiate(lancero);
                    instrucciones[paso].SetActive(true);
                    paso++;
                }
                break;
            case 12: //11
                if (lan == null)
                {
                    instrucciones[paso].SetActive(true);
                    instrucciones[paso-1].SetActive(false);
                    miniMapa.SetActive(true);
                    paso++;
                }
                break;
            case 13: //12
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso].SetActive(true);
                    instrucciones[paso - 1].SetActive(false);
                    miniMapa.SetActive(true);
                    paso++;
                }
                break;
            case 14: //13
                if (Input.GetMouseButtonDown(0))
                {
                    instrucciones[paso-1].SetActive(false);                
                    menuFinTutorial.SetActive(true);
                    Time.timeScale = 0;  //desactiva el juego
                    GameManager.instance.CambiarPausa(true);
                }
                break;
        }
	}

    /// <summary>
    /// Desactiva la pausa al volver al menu
    /// </summary>
    public void DesactivarPausa()
    {
        Time.timeScale = 1;  //desactiva el juego
        GameManager.instance.CambiarPausa(false);
    }


    /// <summary>
    /// Ya se han recolecctado todos los materiales y hay que pasar de paso
    /// </summary>
    public void MaterialesRecolectados()
    {
        instrucciones[paso-1].SetActive(false);
        instrucciones[paso].SetActive(true);
        paso++;

    }


    /// <summary>
    /// Cuento es llamado pasa de paso
    /// </summary>
    public void SiguientePaso()  //esta separado para que solo pueda ser llamada una vez llegada la instruccion de craftear armas
    {
        if (paso < instrucciones.Length && paso == 6) 
        {
            instrucciones[paso].SetActive(false);
            paso++;
        }
    }
}
