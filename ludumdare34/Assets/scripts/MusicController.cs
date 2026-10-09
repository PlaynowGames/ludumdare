using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {


    public AudioSource audio;
    public AudioClip startClip;
    public AudioClip loopClip;
    public float loopClipStartPosition = 44.5f;

    // Use this for initialization
    void Start() {
        audio.loop = false;
        audio.clip = startClip;
        audio.Play();
        Invoke("ChangeClip", startClip.length);
    }

    public void ChangeClip() {
        audio.clip = loopClip;
        audio.time = loopClipStartPosition;
        audio.Play();
        Invoke("ChangeClip", loopClip.length - loopClipStartPosition);
    }
}
