using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controla el movimiento del enemigo (lineal) con velocidad ajustable y la rotacion
/// </summary>
public class MovimientoEnemigo : MonoBehaviour {

    public int Velocidad;
    public int Daño;

    bool LadronEstado;
    bool knockback = false;
    private Rigidbody2D rb;
    private GameObject jugador;
    private Vector2 movimiento;
    private Vector2 diferencia;
    private float angulo;
    private Ladron ladron;
    private Charger charger;
    

    void Start ()
    {
        ladron = GetComponent<Ladron>();
        charger = GetComponent<Charger>();
        rb = GetComponent<Rigidbody2D>();
        jugador = LevelManager.instance.Jugador(); //recibe una referencia del jugador
        if(ladron) LadronEstado = ladron.DevolverEstado(); //si LadronEstado es true, utiliza este codigo para moverse
        //hay que poner que cada vez que se cambia el estado del enemigo cambie aqui tmb
    }
	
	void Update ()
    {
        //diferencia de posicion entre el jugador y el enemigo
        diferencia = new Vector2(jugador.transform.position.x - transform.position.x, jugador.transform.position.y - transform.position.y);
        angulo = Mathf.Atan2(diferencia.x, diferencia.y) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
        transform.rotation = Quaternion.Euler(0, 0, -angulo); //cambia la rotacion del enemigo
        if(!knockback && (LadronEstado))movimiento = new Vector2(jugador.transform.position.x - rb.position.x, jugador.transform.position.y - rb.position.y).normalized *Velocidad;
    }

    private void FixedUpdate()
    {
        rb.velocity = movimiento;
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
    
    public int DevuelveDaño()
    {
        return Daño;
    }

}
