using UnityEngine;
using System.Collections;

public class Bau : MonoBehaviour {

	public float multiplicatorTimeLimit = 1;

	//private Score score;
	private int pontos = 10;
	private AudioSource audio;
	private scoreAdder scor;

	private float timeSinceLastCollision;
	private int multiplicationRatio = 1;
	private bool cheio;
	float Xpoint;



	// Use this for initialization
	void Start () {
		//score = GameObject.FindGameObjectWithTag("pontos").GetComponent<Score>();
		audio = gameObject.GetComponent<AudioSource>();
		cheio = false;
		scor = GameObject.FindGameObjectWithTag("canvPontos").GetComponent<scoreAdder>();

	}
	
	// Update is called once per frame
	void Update () {
		if (cheio && Time.timeSinceLevelLoad - timeSinceLastCollision > multiplicatorTimeLimit) {
			cheio = false;
			transform.parent.GetComponent<MotoboyController> ().Go ();
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Estou aqui!!" + other.gameObject.name);

		if (other.gameObject.tag == "pizza") {
			audio.Play ();

			if (Time.timeSinceLevelLoad - timeSinceLastCollision > multiplicatorTimeLimit)
				multiplicationRatio = 1;
			else
				multiplicationRatio++;


			int random = UnityEngine.Random.Range(1, 30);
			int random2 = UnityEngine.Random.Range(30, 60);

			if (transform.parent.GetComponent<MotoboyController> ().direcao == "e") {
				Xpoint = -232f;
			} else if (transform.parent.GetComponent<MotoboyController> ().direcao == "d"){
				Xpoint = 210f;
			}
			Debug.Log (transform.parent.GetComponent<MotoboyController> ().transform.position);

			scor.createScoreAnimation (Xpoint,-93f,"+" + multiplicationRatio * pontos,multiplicationRatio * pontos);
			scor.Recorde ();
			scor.Pontuacao ();

			timeSinceLastCollision = Time.timeSinceLevelLoad;
			cheio = true;

			Destroy (other.gameObject);
		} else {
			//Destroy(gameObject);
		}



	}
}
