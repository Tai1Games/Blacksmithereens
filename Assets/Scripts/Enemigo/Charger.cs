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
    private bool EstadoEnemigo = false;//si es true usa este scipt para moverse
    private bool cargarotacion = false;
    private MovimientoEnemigo movEnemigo;
    private Vector2 diferencia;
    private float angulo;


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
        if (!EstadoEnemigo && !cargarotacion)
        {
            diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
            angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
            transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
        }
    }

    private void FixedUpdate()
    {
        if (moveratras) //El enemigo se mueve hacia atrás
        {
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
        EstadoEnemigo = true;
        movEnemigo.CambiarEstadoEnemigo(EstadoEnemigo); //como va a seguir al jugador, usa el MoviminetoEnemigo
        velocidad /= 2; //Se reduce la velocidad para prepararse antes de la carga
        yield return new WaitForSeconds(TiempoPreparacion); //Se espera durante un tiempo de preparación elegido desde el editor
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        EstadoEnemigo = false;
        movEnemigo.CambiarEstadoEnemigo(EstadoEnemigo); 
        rb.velocity = Vector2.zero;  //se para
        yield return new WaitForSeconds(0.5f);
        EstadoEnemigo = true;
        movEnemigo.CambiarEstadoEnemigo(EstadoEnemigo);
        yield return new WaitForSeconds(0.01f);
        cargarotacion = true;
        Vector2 DireccionCarga = movEnemigo.DevolverMovimiento().normalized;  //coge la direccion hacia la q se tiene q mover
        EstadoEnemigo = false;
        movEnemigo.CambiarEstadoEnemigo(EstadoEnemigo);
        rb.AddForce(DireccionCarga * fuerza); //Se añade la fuerza de carga
        yield return new WaitForSeconds(TiempoDescanso); //Se espera durante un tiempo de descanso
        cargarotacion = false;
        velocidad *= 2; //La velocidad vuelve a a normalidad
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        EstadoEnemigo = false;
        movEnemigo.CambiarEstadoEnemigo(EstadoEnemigo); //deja de usar el MovimientoEnemigo para moverse
        moveratras = true; //El enemigo comienza a alejarse del jugador, tras lo cual comenzará el proceso de carga de nuevo
    }
}
