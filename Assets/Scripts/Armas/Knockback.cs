using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour {

    Ladron ladron;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ladron = collision.GetComponent<Ladron>();
        if (ladron != null)
        {
            ladron.SetKnockback();
        }
    }
}
