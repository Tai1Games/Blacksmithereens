using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaDir : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void nuevaDirección (Vector3 dir)
    {
        transform.right = dir+ new Vector3(90, -90, 0);
    }
}
