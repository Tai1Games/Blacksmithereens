using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    public float Fuerza;
    private MovimientoEnemigo movEnemigo;
    private Vector2 mouse_position; //posicion del raton
    private Vector2 offset, screenPoint; //vectores para sacar el angulo

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        mouse_position = Input.mousePosition; //obtiene posicion del raton
        screenPoint = Camera.main.WorldToScreenPoint(transform.position); //saca la posicion del jugador en relacion al tamaño de la pantalla de juego
        offset = new Vector2(mouse_position.x - screenPoint.x, mouse_position.y - screenPoint.y); //diferencia de posicion entre raton y jugador

        movEnemigo = collision.GetComponent<MovimientoEnemigo>();
        if(movEnemigo) movEnemigo.SetKnockBack(offset.normalized * Fuerza);
    }
}
