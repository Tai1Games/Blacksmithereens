using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class Ziccboi : MonoBehaviour {

    public float velocidad;
    public float offsetAtaque;
    public float tiempoEspera;

    private Rigidbody2D rb;
    private Vector2 movimiento;
    private GameObject jugador;
    private Vector2 diferencia;
    private float angulo;
    Animator anim;
    bool atacando = false;

	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugador
        anim = GetComponentInChildren<Animator>();
    }
	
	void Update ()
    {
        
        //diferencia de posicion entre el jugador y el enemigo
        diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
        angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
        transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
        if(rb.position.x > jugador.transform.position.x - offsetAtaque && rb.position.y > jugador.transform.position.y - offsetAtaque &&
                    rb.position.x < jugador.transform.position.x + offsetAtaque && rb.position.y < jugador.transform.position.y + offsetAtaque
                    && atacando == false) //Si el ziccboi está a menos de una determinada distancia del jugador y no está atacando ya...
        {
            atacando = true;
            StartCoroutine(ZiccboiAtaca()); //Comienza la corrutina de ataque
        }
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

    /// <summary>
    /// Controla el ataque del ziccboi y la espera tras este
    /// </summary>
    /// <returns></returns>
    private IEnumerator ZiccboiAtaca()
    {
        anim.Play("EspadaZiccboiAtaca", -1, 0); //Se reproduce la animación de ataque de la espada
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(-1).length + tiempoEspera); //Se espera a que acabe + un tiempo de espera a elegir
        atacando = false; //Se pone el ataque a false para poder volver a atacar
    }
}
