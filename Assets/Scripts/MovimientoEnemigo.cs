using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class MovimientoEnemigo : MonoBehaviour {

    public float velocidad;
    private Rigidbody2D rb;
    private Vector2 movimiento;
    private GameObject jugador;
    private Vector2 diferencia;
    private float angulo;
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugador
    }
	
	void Update ()
    {
        
        //diferencia de posicion entre el jugador y el enemigo
        diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
        angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
        transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
    }

    private void FixedUpdate()
    {
        if (jugador != null) //cacheo de referencia
        {
            //halla el vector direccion entre la posicion del enemigo y la del jugador y lo normaliza
            movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized;
            //mueve al enemigo asegurandose de que no supera la velocidad si se mueve en diagonal
            rb.velocity = Vector2.ClampMagnitude(movimiento * velocidad, velocidad);
        }
    }
}
