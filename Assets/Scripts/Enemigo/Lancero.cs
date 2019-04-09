using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class Lancero : MonoBehaviour {

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
    bool moviendo = true;
    bool knockback = false;

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
                    && atacando == false) //Si el lancero está a menos de una determinada distancia del jugador y no está atacando ya...
        {
            atacando = true;
            StartCoroutine(LanceroAtaca()); //Comienza la corrutina de ataque
        }
    }

    private void FixedUpdate()
    {
        if (!knockback)
        {
            if (jugador != null && moviendo == true) //cacheo de referencia
            {
                //halla el vector direccion entre la posicion del enemigo y la del jugador y lo normaliza
                movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized;
                //mueve al enemigo asegurandose de que no supera la velocidad si se mueve en diagonal
                rb.velocity = Vector2.ClampMagnitude(movimiento * velocidad, velocidad);
            }
            else rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Controla el ataque del lancero y la espera tras este
    /// </summary>
    /// <returns></returns>
    private IEnumerator LanceroAtaca()
    {
        moviendo = false;
        anim.Play("LanzaLanceroAtaca"); //Se reproduce la animación de ataque de la lanza
        yield return new WaitForSeconds(0.5f);
        moviendo = true;
        yield return new WaitForSeconds(tiempoEspera); //Se espera a que acabe + un tiempo de espera a elegir
        atacando = false; //Se pone el ataque a false para poder volver a atacar
    }

    /// <summary>
    /// Al entrar en contacto con un arma, a este objeto se le aplica un knockback
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Lanza" && collision.tag != "Jugador") StartCoroutine(ParonAlRecibirGolpe());        //empieza proceso knockback
        else if (collision.tag != "Jugador") StartCoroutine(Knockback());
    }


    /// <summary>
    /// Controla todo el proceso del paron cuando recibe un golpe
    /// </summary>
    private IEnumerator ParonAlRecibirGolpe()
    {
        knockback = true; //desactiva el movimineto normal
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.2f);
        knockback = false; //activa el movimineto normal
    }


    /// <summary>
    /// Controla todo el proceso del knockback
    /// </summary>
    private IEnumerator Knockback()
    {
        Vector2 knock;
        knockback = true; //desactiva el movimineto normal
        knock = movimiento * -1;
        rb.velocity = knock;
        yield return new WaitForSeconds(0.2f);
        knockback = false; //activa el movimineto normal
    }
}
