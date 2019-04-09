using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grada : MonoBehaviour {

    SpriteRenderer sprite;
    Animator anim;
    float empezar;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        float posiciony = (transform.parent.position.y) + 30;
        sprite.sortingOrder = Mathf.RoundToInt(posiciony*(-1));
        anim = GetComponent<Animator>();
        empezar = Random.Range(0f, 1.5f);
        StartCoroutine(Animacion());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private IEnumerator Animacion()
    {
        yield return new WaitForSeconds(empezar);
        anim.SetTrigger("Mover");
    }
}
