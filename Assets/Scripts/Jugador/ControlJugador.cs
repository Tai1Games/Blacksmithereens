using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour {

    public float velocidad; //Velocidad del jugador
    private Rigidbody2D rb; //Componente Rigidbody2D
    private Vector2 movimiento; //En cada frame se multiplica cada eje activo por la velocidad y se integran en el vector
    private Vector2 mouse_position; //posicion del raton
    private Vector2 offset, screenPoint; //vectores para sacar el angulo
    private float angle; //angulo del jugador 
    private Animator animacion;

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        //LevelManager.instance.AsignarJugador(this.gameObject); //Da una referencia al LevelManager
        animacion = GetComponent<Animator>();
	}

    private void Update()
    {
        movimiento = new Vector2(Input.GetAxis("Horizontal") * velocidad, Input.GetAxis("Vertical") * velocidad);

        if (movimiento != Vector2.zero) animacion.SetTrigger("Movimineto");
        else animacion.SetTrigger("Quieto");
        

        mouse_position = Input.mousePosition; //obtiene posicion del raton
        screenPoint = Camera.main.WorldToScreenPoint(transform.position); //saca la posicion del jugador en relacion al tamaño de la pantalla de juego
        offset = new Vector2(mouse_position.x - screenPoint.x, mouse_position.y - screenPoint.y); //diferencia de posicion entre raton y jugador
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg; //angulo a traves de la tangente y lo pasa a grados
        angle -= 90;
        transform.rotation = Quaternion.Euler(0, 0, angle); //cambia la rotación del jugador
    }

    void FixedUpdate () {
        rb.velocity = Vector2.ClampMagnitude(movimiento, velocidad); //La velocidad del personaje se limita para que no sea mayor al tener dos ejes activos
	}
}
