using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CambiaSprite : MonoBehaviour {

	public Image[] arrayArmas;


	// Use this for initialization
	void Start () {
		CambiaSpriteUI(Armas.Martillo);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Cambia el sprite desplegado al indicado
	/// </summary>
	public void CambiaSpriteUI(Armas arma)
	{
		foreach (Image img in arrayArmas)
			img.enabled = false;

		arrayArmas[(int)arma].enabled = true;
	}
}
