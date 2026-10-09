using UnityEngine;
using System.Collections;
using PizzaPanic;

public class Bau : MonoBehaviour {

	public float multiplicatorTimeLimit = 1;

	private int pontos = 10;
	private AudioSource audio;
	private scoreAdder scor;

	private float timeSinceLastCollision;
	private int multiplicationRatio = 1;
	private bool cheio;
	private bool comboQuebrado;
	float Xpoint;

	// Use this for initialization
	void Start () {
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
		if (other.gameObject.tag == "pizza") {
			PizzaEspecial.Tipo tipo = PizzaEspecial.TipoDe (other.gameObject);
			PizzaEspecial especial = other.GetComponent<PizzaEspecial> ();
			bool queimada = tipo == PizzaEspecial.Tipo.Queimada;

			if (comboQuebrado || Time.timeSinceLevelLoad - timeSinceLastCollision > multiplicatorTimeLimit)
				multiplicationRatio = 1;
			else
				multiplicationRatio++;
			comboQuebrado = queimada;

			// O som sobe de tom a cada pizza do combo; a queimada toca grave
			audio.pitch = queimada ? 0.6f : Mathf.Min (1f + 0.1f * (multiplicationRatio - 1), 1.6f);
			audio.Play ();

			int ganho;
			if (queimada) {
				// Pizza queimada tira pontos (sem deixar o placar negativo) e quebra o combo
				ganho = -Mathf.Min (pontos * 2, scoreAdder.actualScore);
			} else {
				int multiplicador = especial != null ? especial.Multiplicador : 1;
				ganho = multiplicationRatio * pontos * multiplicador;
			}

			if (tipo == PizzaEspecial.Tipo.Vida) {
				GameObject vidas = GameObject.FindGameObjectWithTag ("vidas");
				if (vidas != null) {
					vidas.GetComponent<Vidas> ().GanharVida ();
				}
			}

			if (transform.parent.GetComponent<MotoboyController> ().direcao == "e") {
				Xpoint = -232f;
			} else if (transform.parent.GetComponent<MotoboyController> ().direcao == "d"){
				Xpoint = 210f;
			}

			scor.createScoreAnimation (Xpoint, -93f, (ganho >= 0 ? "+" : "") + ganho, ganho);
			scor.Recorde ();
			scor.Pontuacao ();

			timeSinceLastCollision = Time.timeSinceLevelLoad;
			cheio = true;

			GameEvents.NotificarEntrega (multiplicationRatio, ganho, multiplicatorTimeLimit, transform.position, tipo);

			Destroy (other.gameObject);
		}
	}
}
