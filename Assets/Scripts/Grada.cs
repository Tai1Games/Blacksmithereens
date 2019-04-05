using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grada : MonoBehaviour {

    Animator anim;
    int empezar;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        empezar = Random.Range(0, 1);
        StartCoroutine("empezarAnimacion");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private IEnumerable empezarAnimacion()
    {
        yield return new WaitForSeconds(empezar);
        anim.SetTrigger("Mover");
    }
}
