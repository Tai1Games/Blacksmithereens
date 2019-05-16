using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSoundEffects : MonoBehaviour {

    public AudioClip soundEffect;

    public AudioSource soundSource;

	void Start () {
       
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnEnable()
    {
        soundSource.Play();
    }
}
