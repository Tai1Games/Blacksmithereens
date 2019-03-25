﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class MovimientoEnemigo : MonoBehaviour {

    public int Velocidad;
    public int Daño;

    bool EstadoEnemigo = true;
    bool knockback = false;
    private Rigidbody2D rb;
    private GameObject jugador;
    private Vector2 movimiento;
    private Vector2 diferencia;
    private float angulo;
    

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugado
    }
	
	void Update ()
    {

        if(!knockback && (EstadoEnemigo))
        {        //diferencia de posicion entre el jugador y el enemigo
            diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
            angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
            transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
            movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized * Velocidad;
        }
    }

    private void FixedUpdate()
    {
        if(EstadoEnemigo)rb.velocity = movimiento;
    }

    /// <summary>
    /// Cuando un arma colisiona con el enemigo, le aplica una fuerza de knockback
    /// </summary>
    public void SetKnockBack(Vector2 direccion)
    {
        knockback = true;  //esta en proceso de knockback
        StartCoroutine(DesactivarKnockback());  //tiene un tiempo de espera antes de volver a atacar
        movimiento = (direccion);
    }


    /// <summary>
    /// Esta corrutina controla el tiempo de cooldown despues del knockback
    /// </summary>
    private IEnumerator DesactivarKnockback()
    {
        yield return new WaitForSeconds(0.2f);  //tiempo de cool down antes de que el enemigo te pueda atacar otra vez
        knockback = false;
    }

    /// <summary>
    /// Le dice al metodo hacer daño de cada enemigo el daño que tiene que hacer
    /// </summary>
    public int DevuelveDaño()
    {
        return Daño;
    }

    /// <summary>
    /// Cambia el estado del enemigo, si es false NO usa este script para su movimineto
    /// </summary>
    public void CambiarEstadoEnemigo(bool NuevoEstado)
    {
        EstadoEnemigo = NuevoEstado;
    }


    /// <summary>
    /// Devulve la direccion hacia la que el enemigo se tiene que mover, usado para la carga del charger
    /// </summary>
    public Vector2 DevolverMovimiento()
    {
        return movimiento;
    }


    public int DevuelveVelocidad()
    {
        return Velocidad;
    }

}
