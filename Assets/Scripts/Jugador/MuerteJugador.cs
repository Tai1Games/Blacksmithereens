using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuerteJugador : MonoBehaviour {

    public Transform puntoSpawn;
    private Rigidbody2D rb;

	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	void Update () {
		
	}

    /// <summary>
    /// Este método devuelve al jugador a un punto de Spawn al llegar su salud a 0 (es decir, cuando VidaJugador llame a este método)
    /// </summary>
    public void JugadorMuere()
    {
        LevelManager.instance.VuelveaMenu();
    }
}
