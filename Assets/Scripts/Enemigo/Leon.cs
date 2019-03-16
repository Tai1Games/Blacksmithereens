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

    float t;
    Animator animador;
    bool saltando = false;
    Rigidbody2D rb;
    GameObject jugador;
    Vector2 diferencia;
    float angulo;
    Vector2 movimiento;


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
        if (saltando)
        {
            rb.velocity = Vector2.ClampMagnitude(movimiento * velocidad, velocidad); //El leon se mueve hacia el jugador
            t += Time.fixedDeltaTime; //El contador de tiempo se va incrementando
            if (t>= tiempoSalto) //Cuando es igual al tiempo definido de salto, el salto acaba
            {
                saltando = false;
                t = 0; //Se reinicia el tiempo
                rb.velocity = Vector2.zero;
                Physics2D.IgnoreLayerCollision(9, 8, false); //Las colisiones entre el león y el jugador/enemigos vuelven a funcionar
                Physics2D.IgnoreLayerCollision(9, 10, false);
            }
        }
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
            movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized; 
            Physics2D.IgnoreLayerCollision(9, 8, true); //Mientras salta, no colisiona con el jugador...
            Physics2D.IgnoreLayerCollision(9, 10, true); //... ni con otros enemigos
            saltando = true; //Indica a fixedUpdate que comience el movimiento
            animador.Play("LeonSaltando", -1, 0); //Reproduce la animación de salto
            yield return new WaitUntil(() => saltando == false); //Espera hasta que saltando sea igual a false
            yield return new WaitForSeconds(tiempoEspera); //Espera antes de volver a comenzar el bucle
        }
    }
}
