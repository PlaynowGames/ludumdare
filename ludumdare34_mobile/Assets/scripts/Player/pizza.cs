using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class pizza : MonoBehaviour {

	private Transform bandeja;
	public bool naBandeja;

	public float dampTime = 0.15f;
	private Vidas vida;
	private AudioSource audio;
	public AudioSource fail;
	private Animator animator;

	private GameObject go;

	// Use this for initialization
	void Start () {
		go = GameObject.FindGameObjectWithTag("vidas");
		bandeja = GameObject.FindGameObjectWithTag("bandeja").transform;
		audio = gameObject.GetComponent<AudioSource>();
		fail = GameObject.FindGameObjectWithTag("somFail").GetComponent<AudioSource>();
		animator = gameObject.GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {


		if (gameObject.transform.position.y < bandeja.transform.position.y) {
			Debug.Log ("passei a bandeja");
			naBandeja = false;
			transform.parent = null;
		}

	}



	void OnCollisionEnter2D(Collision2D colisor){

		if (!naBandeja) {
			if (colisor.gameObject.tag == "bandeja") {
				naBandeja = true;
				transform.parent = bandeja;
				audio.Play ();
			} else if (colisor.gameObject.tag == "pizza") {
				if (colisor.transform.GetComponent<pizza> ().naBandeja == true) {
					naBandeja = true;
					transform.parent = bandeja;
					audio.Play ();
				}
			} else {
				Debug.Log ("COLIDIU " + colisor.gameObject.name);
				animator.SetBool ("splash", true);
				fail.Play ();
				Destroy (gameObject, 0.3f);
			}
		}

		if (colisor.gameObject.name == "chao") {
			vida = go.GetComponent<Vidas>  ();
			if (vida.ExcluirVida ()) {
			} else {
				SceneManager.LoadScene("GameOver");
			}
		}
	}


}
