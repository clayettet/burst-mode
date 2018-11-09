using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public AudioClip clip;

public class AudioMouseOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(clip);
    }

    public static void PlaySound(AudioClip clip)
    {
        GameObject Go = new GameObject();
        AudioSource audioSource = Go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
        GameObject.Destroy(Go, audioSource.clip.length + 0.1f);
    }
}
