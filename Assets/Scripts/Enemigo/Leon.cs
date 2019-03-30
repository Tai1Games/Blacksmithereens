using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class Leon : MonoBehaviour {

    public float tiempoSalto;
    public float tiempoEspera;
    public float velocidad;
    public float tiempoTerminarSalto;
    public float fuerzaKnockbackLanza;

    float t;
    bool knockback = false;
    bool saltando = false;
    float angulo;
    Animator animador;
    Rigidbody2D rb;
    GameObject jugador;
    Vector2 movimiento;
    Vector2 posicionJugador;
    Vector2 diferencia;

    void Start ()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animador = this.GetComponent<Animator>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugador
        ComienzaSalto(); //Comienza con el bucle del salto del león
    }
	
	void Update ()
    {
        if (saltando == false)
        {
            //diferencia de posicion entre el jugador y el enemigo
            diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
            angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
            transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
        }
    }

    private void FixedUpdate()
    {
        if (!knockback)
        {
            if (saltando)
            {
                t += Time.fixedDeltaTime; //El contador de tiempo se va incrementando
                rb.velocity = Vector2.ClampMagnitude(movimiento * velocidad, velocidad);

                if (t >= tiempoSalto)
                    StartCoroutine(TerminaSalto(0)); //Terminamos el salto sin esperar
                else if (rb.position.x > posicionJugador.x - 0.5f && rb.position.y > posicionJugador.y - 0.5f &&
                        rb.position.x < posicionJugador.x + 0.5f && rb.position.y < posicionJugador.y + 0.5f)
                    StartCoroutine(TerminaSalto(tiempoTerminarSalto)); //Terminamos el salto esperando el tiempo elegido
            }
        }        
    }

    /// <summary>
    /// Espera un tiempo determinado par ahcer daño si el león está encima del jugador. Si no, no lo hace
    /// </summary>
    /// <param name="tiempo"></param>
    /// <returns></returns>
    private IEnumerator TerminaSalto(float tiempo)
    {
        saltando = false;
        t = 0; //Se reinicia el tiempo
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(tiempo);
        Physics2D.IgnoreLayerCollision(9, 8, false); //Las colisiones entre el león y el jugador/enemigos vuelven a funcionar
        Physics2D.IgnoreLayerCollision(9, 10, false);
    }

    /// <summary>
    /// Comienza la corrutina de salto
    /// </summary>
    private void ComienzaSalto() 
    {
        StartCoroutine(Salta());
    }

    /// <summary>
    /// Esta corrutina contiene un bucle que se repetirá mientars el enemigo siga activo, y su comportamiento será el de saltar hacia el jugador y esperar
    /// </summary>
    /// <returns></returns>
    private IEnumerator Salta()
    {
        while (this.enabled) //Mientras el león esté activo...
        {
            //Se moverá hacia el jugador
            posicionJugador = jugador.transform.position;
            movimiento = new Vector2(posicionJugador.x - rb.position.x, posicionJugador.y - rb.position.y).normalized; 
            Physics2D.IgnoreLayerCollision(9, 8, true); //Mientras salta, no colisiona con el jugador...
            Physics2D.IgnoreLayerCollision(9, 10, true); //... ni con otros enemigos
            saltando = true; //Indica a fixedUpdate que comience el movimiento
            animador.Play("LeonSaltando", -1, 0); //Reproduce la animación de salto
            yield return new WaitUntil(() => saltando == false); //Espera hasta que saltando sea igual a false
            yield return new WaitForSeconds(tiempoEspera); //Espera antes de volver a comenzar el bucle
        }
    }

    /// <summary>
    /// Al entrar en contacto con un arma, a este objeto se le aplica un knockback
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Lanza")StartCoroutine(Knockback());  //empieza el knockback
    }

    /// <summary>
    /// Controla todo el proceso del knockback
    /// </summary>
    private IEnumerator Knockback()
    {
        Vector2 knock;
        knockback = true; //desactiva el movimineto normal
        posicionJugador = jugador.transform.position;
        movimiento = new Vector2(posicionJugador.x - rb.position.x, posicionJugador.y - rb.position.y).normalized;
        knock = movimiento * (-1) * fuerzaKnockbackLanza; //direccion inversa a la que se quiere dirigir
        rb.velocity = knock;
        yield return new WaitForSeconds(0.2f);
        rb.velocity = Vector2.zero;
        knockback = false; //activa el movimineto normal
    }
}
