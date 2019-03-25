using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class Ladron : MonoBehaviour
{
    public int matRobados;

    bool volver = false;
    bool robado = false;
    Vector2 offset = new Vector2(0.5f, 0.5f);
    Rigidbody2D rb;
    Vector2 movimiento;
    GameObject jugador;
    private Vector2 salida;
    MovimientoEnemigo movenemigo;
    int velocidad;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugador
        salida = transform.position;
        movenemigo = GetComponent<MovimientoEnemigo>();
        velocidad = movenemigo.DevuelveVelocidad();
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        if (jugador != null && volver) //Si volver = true, el ladron huye del jugador hacia su punto de salida
        {
            //halla el vector direccion entre la posicion del enemigo y la del jugador y lo normaliza
            movimiento = new Vector2(salida.x - rb.position.x, salida.y - rb.position.y).normalized;
            //mueve al enemigo asegurandose de que no supera la velocidad si se mueve en diagonal
            rb.velocity = Vector2.ClampMagnitude(movimiento * velocidad, velocidad);
            //Si el ladron está huyendo y llega al punto de salida, desaparece
            if (transform.position.x > salida.x - offset.x && transform.position.y > salida.y - offset.y &&
                transform.position.x < salida.x + offset.x && transform.position.y < salida.y + offset.y)
            {
                LevelManager.instance.EnemigoMuerto();
                Destroy(gameObject);
            }
        }
    }


    /// <summary>
    /// Si el ladrón ha robado materiales, los devolverá al morir y se mostrará el texto pop-up (este método es invocado por MuerteEnemigo)
    /// </summary>
    public void RecuperaMateriales()
    {
        if (robado)
        {
            jugador.GetComponent<Materiales>().SumarMateriales(matRobados);
            LevelManager.instance.MuestraPopUpMat("+ " + matRobados.ToString(), new Vector2(transform.position.x, transform.position.y)); //Dice al LvlManager que muestre los materiales nuevos en la posición donde muere
        }
    }

    /// <summary>
    /// Al entrar en contacto con el jugador, le roba matRobados materiales (solo una vez) y procede a huir hacia el punto de salida.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Materiales>() && robado == false)
        {
            movenemigo.CambiarEstadoEnemigo(false);
            Materiales mat = collision.gameObject.GetComponent<Materiales>();
            int numMateriales = mat.DecirMateriales();
            //Si el jugador no tiene suficientes materiales, el ladrón ataca
            //Debería rbar los que tiene el jugador aunque no sean suficientes
            if (numMateriales == 0)
            {
                if (collision.gameObject.GetComponent<VidaJugador>())
                    collision.gameObject.GetComponent<VidaJugador>().RestaVida(movenemigo.DevuelveDaño());
            }
            else //Si los tiene, le roba y emprende su huida
            {
                robado = true;
                if (numMateriales < matRobados) matRobados = numMateriales;
                mat.RestarMateriales(matRobados);

                LevelManager.instance.MuestraPopUpMat("- " + matRobados, new Vector2(transform.position.x, transform.position.y));
               
                volver = true;
            }
        }
    }
}