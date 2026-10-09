using UnityEngine;
using System.Collections;
using PizzaPanic;

public class PlayerTouchController : MonoBehaviour {

	public float minBounds, maxBounds;
	public float moveDistance;
	public float delayBetweenMovements;
	public float rotationOffset = 0;

	public AudioSource audioSource;
	public AudioClip scoreClip;

	public Sprite[] players;
	public SpriteRenderer pls;

	private float lastMovementTime;
	private float input;
	private float orientation;

	// GUITexture foi removido do Unity: o objeto (chamado "esquerdo" ou "direito")
	// passa a cobrir a metade correspondente da tela por cálculo direto.
	public Transform player;
	private string lado;
	private Transform bandejaTransform;

	// Use this for initialization
	void Start () {
		lastMovementTime = Time.timeSinceLevelLoad;
		pls = GameObject.FindGameObjectWithTag("hero").GetComponent<SpriteRenderer>();
		lado = gameObject.name;
		GameObject bandeja = GameObject.FindGameObjectWithTag("bandeja");
		if (bandeja != null) {
			bandejaTransform = bandeja.transform;
		}
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

	}

	// Verifica se o toque caiu na metade da tela deste controle
	private bool ToqueNoMeuLado (Vector2 posicao) {
		float meio = Screen.width / 2f;
		return lado == "esquerdo" ? posicao.x < meio : posicao.x >= meio;
	}

	// Update is called once per frame
	void Update () {

		// Quanto mais pizzas na bandeja, mais devagar o jogador anda
		float atraso = delayBetweenMovements * Difficulty.FatorPeso (Difficulty.PizzasNaBandeja (bandejaTransform));

		if (Time.timeSinceLevelLoad - lastMovementTime >= atraso) {

			foreach (UnityEngine.Touch touch in Input.touches) {

				if (ToqueNoMeuLado (touch.position)) {

					if (touch.phase != TouchPhase.Ended) {
						if (lado == "direito") {

							orientation = Mathf.Abs (player.transform.localScale.x);
							if (player.transform.localScale.x != orientation) { //Looking to other direction
								player.transform.localScale = new Vector3 (-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
								player.transform.Translate (Vector3.left * rotationOffset);
							} else if (player.transform.position.x + moveDistance <= maxBounds) //Doesnt allow player reach area outside bounds
								player.transform.Translate (Vector3.right * moveDistance);

							lastMovementTime = Time.timeSinceLevelLoad;

						}


						if (lado == "esquerdo") {
							orientation = Mathf.Abs (player.transform.localScale.x);
							if (player.transform.localScale.x != -orientation) {//Looking to other direction 
								player.transform.localScale = new Vector3(-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
								player.transform.Translate (Vector3.right * rotationOffset);
							}
							else if (player.transform.position.x - moveDistance >= minBounds) //Doesnt allow player reach area outside bounds
								player.transform.Translate(Vector3.left * moveDistance);

							lastMovementTime = Time.timeSinceLevelLoad;
						}
					}

				}


			}
		}

		AtualizarSprite ();
	}

	// Posições em x = 0, ±2, ±4 usam os sprites 0, 1 e 2 (tolerância evita comparar float com ==)
	private void AtualizarSprite() {
		float distancia = Mathf.Abs (player.transform.position.x);
		int indice = Mathf.RoundToInt (distancia / 2f);

		if (Mathf.Abs (distancia - indice * 2f) < 0.01f && indice < players.Length) {
			pls.sprite = players [indice];
		}
	}



}
