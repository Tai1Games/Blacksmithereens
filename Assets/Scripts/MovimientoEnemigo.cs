using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class MovimientoEnemigo : MonoBehaviour {

    //Ladron
    public float LadronVelocidad;
    public int LadronDaño;
    public int LadronmatRobados;

    bool volver = false;
    bool robado = false;
    bool knockback = false;
    private Vector2 salida;


    //Charger
    public float ChargerTiempoPreparacion;
    public float ChargerTiempoRepeticion;
    public int Chargerfuerza;
    public float ChargerTiempoDescanso;
    public float Chargervelocidad;

    private bool moverse = true;
    private bool moveratras = false;


    //Comun entre enemigos
    private Rigidbody2D rb;
    private GameObject jugador;
    private Vector2 offset = new Vector2(0.5f, 0.5f);
    private Vector2 movimiento;
    private Vector2 diferencia;
    private float angulo;


    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugador
        salida = transform.position;
        if(this.gameObject.tag == "Charger") InvokeRepeating("ComienzaCarga", 0, ChargerTiempoRepeticion);
    }
	
	void Update ()
    {
        if(this.gameObject.tag == "Ladron")  //movimiento del ladron
        {
            //diferencia de posicion entre el jugador y el enemigo
            diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
            angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
            transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
        }
        else if (this.gameObject.tag == "Charger")
        {
            if (moverse || moveratras)
            {
                //diferencia de posicion entre el jugador y el enemigo
                diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
                angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
                transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
            }
        }
    }

    private void FixedUpdate()
    {
        if (this.gameObject.tag == "Ladron")  //Ladron
        {
            if (jugador != null && volver == false && !knockback) //cacheo de referencia
            {
                //halla el vector direccion entre la posicion del enemigo y la del jugador y lo normaliza
                movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized;
                //mueve al enemigo asegurandose de que no supera la velocidad si se mueve en diagonal
                rb.velocity = Vector2.ClampMagnitude(movimiento * LadronVelocidad, LadronVelocidad);
            }
            else if (jugador != null && !knockback) //Si volver = true, el ladron huye del jugador hacia su punto de salida
            {
                //halla el vector direccion entre la posicion del enemigo y la del jugador y lo normaliza
                movimiento = new Vector2(salida.x - rb.position.x, salida.y - rb.position.y).normalized;
                //mueve al enemigo asegurandose de que no supera la velocidad si se mueve en diagonal
                rb.velocity = Vector2.ClampMagnitude(movimiento * LadronVelocidad, LadronVelocidad);
                //Si el ladron está huyendo y llega al punto de salida, desaparece
                if (transform.position.x > salida.x - offset.x && transform.position.y > salida.y - offset.y &&
                    transform.position.x < salida.x + offset.x && transform.position.y < salida.y + offset.y)
                {
                    LevelManager.instance.EnemigoMuerto();
                    Destroy(gameObject);
                }
            }
        }
        else if (this.gameObject.tag == "Charger")
        {
            if (jugador != null && moverse && !knockback) //cacheo de referencia
            {
                //halla el vector direccion entre la posicion del enemigo y la del jugador y lo normaliza
                movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized;
                //mueve al enemigo asegurandose de que no supera la velocidad si se mueve en diagonal
                rb.velocity = Vector2.ClampMagnitude(movimiento * Chargervelocidad, Chargervelocidad);
            }
            else if (moveratras && !knockback) //El enemigo se mueve hacia atrás
            {
                movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized;
                rb.velocity = Vector2.ClampMagnitude(-movimiento * Chargervelocidad, Chargervelocidad);
            }
        }

    }


    /// <summary>
    /// Cuando un arma colisiona con el enemigo, le aplica una fuerza de knockback
    /// </summary>
    public void SetKnockBack(Vector2 direccion)
    {
        knockback = true;  //esta en proceso de knockback
        rb.velocity = direccion;
        StartCoroutine(DesactivarKnockback());  //tiene un tiempo de espera antes de volver a atacar
    }


    /// <summary>
    /// Si el ladrón ha robado materiales, los devolverá al morir y se mostrará el texto pop-up (este método es invocado por MuerteEnemigo)
    /// </summary>
    public void RecuperaMateriales()
    {
        if (robado)
        {
            jugador.GetComponent<Materiales>().SumarMateriales(LadronmatRobados);
            LevelManager.instance.MuestraPopUpMat("+ " + LadronmatRobados.ToString(), new Vector2(transform.position.x, transform.position.y)); //Dice al LvlManager que muestre los materiales nuevos en la posición donde muere
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
            Materiales mat = collision.gameObject.GetComponent<Materiales>();
            int numMateriales = mat.DecirMateriales();
            //Si el jugador no tiene suficientes materiales, el ladrón ataca
            //Debería rbar los que tiene el jugador aunque no sean suficientes
            if (numMateriales == 0)
            {
                if (collision.gameObject.GetComponent<VidaJugador>())
                    collision.gameObject.GetComponent<VidaJugador>().RestaVida(LadronDaño);
            }
            else //Si los tiene, le roba y emprende su huida
            {
                robado = true;
                if (numMateriales >= LadronmatRobados)
                    mat.RestarMateriales(LadronmatRobados);
                else
                {
                    LadronmatRobados = numMateriales;
                    mat.RestarMateriales(numMateriales);
                }
                volver = true;
            }
        }
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
        moverse = true; //Si es true, el enemigo se mueve hacia delante mediante rb.velocity
        Chargervelocidad /= 2; //Se reduce la velocidad para prepararse antes de la carga
        yield return new WaitForSeconds(ChargerTiempoPreparacion); //Se espera durante un tiempo de preparación elegido desde el editor
        moverse = false; //Se cancela el movimiento mediante rb.velocity para añadir una fuerza de carga
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.5f);
        rb.AddForce(movimiento * Chargerfuerza); //Se añade la fuerza de carga
        yield return new WaitForSeconds(ChargerTiempoDescanso); //Se espera durante un tiempo de descanso
        Chargervelocidad *= 2; //La velocidad vuelve a a normalidad
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        moveratras = true; //El enemigo comienza a alejarse del jugador, tras lo cual comenzará el proceso de carga de nuevo
    }
}
