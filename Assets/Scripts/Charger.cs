using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class Charger : MonoBehaviour {

    public float TiempoPreparacion;
    public float TiempoRepeticion;
    public int fuerza;
    public float TiempoDescanso;

    private float velocidad;
    private Rigidbody2D rb;
    private Vector2 movimiento;
    private GameObject jugador;
    private bool moveratras = false;
    private MovimientoEnemigo movEnemigo;


	void Start ()
    {
        movEnemigo = GetComponent<MovimientoEnemigo>();
        velocidad = movEnemigo.DevuelveVelocidad();
        rb = GetComponent<Rigidbody2D>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugador
        InvokeRepeating("ComienzaCarga", 0, TiempoRepeticion);
    }
	
	void Update ()
    {
    }

    private void FixedUpdate()
    {
        if (moveratras) //El enemigo se mueve hacia atrás
        {
            print("FFF");
            movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized;
            rb.velocity = Vector2.ClampMagnitude(-movimiento * velocidad, velocidad);
        }

    }


    /// <summary>
    /// Este método se encarga de comenzar la corrutina 'Carga' al ser llamado desde el InvokeRepeating de Start()
    /// </summary>
    private void ComienzaCarga() 
    {
        StartCoroutine(Carga());
    }

    /// <summary>
    /// Esta corrutina  controla el moviminto del Charger, dividido en varias fases que se suceden tras cierto tiempo
    /// </summary>
    /// <returns></returns>
    private IEnumerator Carga()
    {
        moveratras = false; //Si es true, el enemigo se mueve hacia atrás
        movEnemigo.CambiarEstadoEnemigo(true); //como va a seguir al jugador, usa el MoviminetoEnemigo
        velocidad /= 2; //Se reduce la velocidad para prepararse antes de la carga
        yield return new WaitForSeconds(TiempoPreparacion); //Se espera durante un tiempo de preparación elegido desde el editor
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        movEnemigo.CambiarEstadoEnemigo(false);
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(0.5f);
        Vector2 DireccionCarga = movEnemigo.DevolverMovimiento().normalized;
        rb.AddForce(DireccionCarga * fuerza); //Se añade la fuerza de carga
        yield return new WaitForSeconds(TiempoDescanso); //Se espera durante un tiempo de descanso
        velocidad *= 2; //La velocidad vuelve a a normalidad
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        movEnemigo.CambiarEstadoEnemigo(false); //deja de usar el MovimientoEnemigo para moverse
        moveratras = true; //El enemigo comienza a alejarse del jugador, tras lo cual comenzará el proceso de carga de nuevo
    }
}
