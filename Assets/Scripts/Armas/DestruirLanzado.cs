using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Destruye el gameobject tras un tiempo fijo.
 */


public class DestruirLanzado : MonoBehaviour {

    public float tiempoDestruir = 1f;

	// Use this for initialization
	void Start () {

        Invoke("Destruir", tiempoDestruir);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Destruir()
    {
        Destroy(this.gameObject);
    }
}
