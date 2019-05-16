using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioSource musica1;
    public AudioSource musica2;
    public AudioSource musica3;
    public AudioSource musicaCheats;
    public AudioSource musicaEndless;

	void Start () {
		
	}
	
	void Update () {
		
	}
    
    /// <summary>
    /// Dependiendo de la música que esté sonando, la para y reproduce la pista nueva indicada. Si ya está sonando, no hace nada.
    /// </summary>
    public void ReproduceMusica(int pista)
    {
        switch (pista)
        {
            case 1:
                if (musicaCheats != null && !musicaCheats.isPlaying)
                {
                    if (musica1 != null && !musica1.isPlaying)
                        musica1.Play();
                }
                break;
            case 2:
                if (musicaCheats != null && !musicaCheats.isPlaying)
                {
                    if (musica1 != null && musica1.isPlaying)
                        musica1.Stop();
                    if (!musica2.isPlaying)
                        musica2.PlayDelayed(3.5f);
                }
                break;
            case 3:
                if (musicaCheats != null && !musicaCheats.isPlaying)
                {
                    if (musica2.isPlaying)
                        musica2.Stop();
                    if (!musica3.isPlaying)
                        musica3.PlayDelayed(3.5f);
                }
                break;
            case 4:
                if (musica1 != null && musica1.isPlaying)
                    musica1.Stop();
                else if (musica2 != null && musica2.isPlaying)
                    musica2.Stop();
                else if (musica3 != null && musica3.isPlaying)
                    musica3.Stop();
                else if (musicaEndless != null && musicaEndless.isPlaying)
                    musicaEndless.Stop();
                if (musicaCheats != null && !musicaCheats.isPlaying)
                    musicaCheats.Play();
                break;
            case 5: //Único caso que puede parar la música de cheats (para que no cambie al pasar de ronda)
                if(musicaCheats!=null && musicaCheats.isPlaying)
                    musicaCheats.Stop();
                break;
            case 6:
                if (musicaEndless != null && !musicaEndless.isPlaying)
                    musicaEndless.Play();
                break;
        }
    }
}
