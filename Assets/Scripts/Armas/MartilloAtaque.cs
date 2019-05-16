using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartilloAtaque : MonoBehaviour
{
    Animator animacion;  //Para acceder a la animación del martillo.
    private Sprite spriteStill;
    private SpriteRenderer spriteContr;

    // Use this for initialization
    void Start()
    {
        animacion = GetComponent<Animator>();
        spriteStill = GetComponent<SpriteRenderer>().sprite;
        spriteContr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Activa la animación del martillo para atacar.
    /// </summary>
    public void AtaqueMartillo()
    {
        if (animacion.GetCurrentAnimatorStateInfo(0).IsName("MartilloDefault")) //Posicion relativa al jugador, para evitar ataques dobles
        {
            animacion.SetTrigger("Ataque");  //Trigger para activar la animación
        }
    }

    /// <summary>
    /// Sube la durabilidad a 1000 y la vida a 10000
    /// </summary>
    public void ActivaCheats(bool estado)
    {        
        HacerDaño daño = GetComponent<HacerDaño>();
        if(estado)daño.CheatsArmas(true);
        else daño.CheatsArmas(false);
    }
}

