using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabezaPong : MonoBehaviour {

    public Transform izquierda;
    public Transform derecha;

    private float offset = 1.5f; //Separación de los cuerpos a la cual la cabeza cambia de dirección
    private Rigidbody2D rb;
    bool direccion;
    int velocidad;
    BingBongPong padre; //Componente BingBongPong del padre

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        padre = GetComponentInParent<BingBongPong>();
        velocidad = padre.VelocidadCabeza(); //Se toma la velocidad de la cabeza del padre
    }

    // Update is called once per frame
    void Update()
    {
        //Si la cabeza está a una distancia offset de un cuerpo, cambia su dirección
        if (transform.position.x > izquierda.position.x - offset && transform.position.y > izquierda.position.y - offset &&
                transform.position.x < izquierda.position.x + offset && transform.position.y < izquierda.position.y + offset && direccion == false ||
                transform.position.x > derecha.position.x - offset && transform.position.y > derecha.position.y - offset &&
                transform.position.x < derecha.position.x + offset && transform.position.y < derecha.position.y + offset && direccion)
            direccion = !direccion;
    }

    private void FixedUpdate()
    {
        //Si direccion == true, la cabeza se mueve hacia el cuerpo derecho. Si no, hacia el izquierdo.
        if (direccion)
            rb.velocity = Vector2.ClampMagnitude(derecha.position*velocidad, velocidad);
        else
            rb.velocity = Vector2.ClampMagnitude(izquierda.position * velocidad, velocidad);
    }

    /// <summary>
    /// Este método multiplica la velocidad de la cabeza por múltiplo.
    /// </summary>
    /// <param name="multiplo"></param>
    public void VariaVelCabeza(int multiplo)
    {
        velocidad *= multiplo;
    }
}
