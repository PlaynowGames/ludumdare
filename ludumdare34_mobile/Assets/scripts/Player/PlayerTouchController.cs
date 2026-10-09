using UnityEngine;
using System.Collections;

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

	private GUITexture bto;
	public Transform player;




	// Use this for initialization
	void Start () {
		lastMovementTime = Time.timeSinceLevelLoad;
		pls = GameObject.FindGameObjectWithTag("hero").GetComponent<SpriteRenderer>();
		bto = gameObject.GetComponent<GUITexture> ();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

	}


	// Update is called once per frame
	void Update () {

		int screenHeight = Screen.height; 
		int screenWidth = Screen.width;

		if (bto.name == "esquerdo") {
			bto.pixelInset = new Rect(0, 0, screenWidth /2, screenHeight);
		} else if (bto.name == "direito") {
			bto.pixelInset = new Rect(screenWidth /2, 0, screenWidth /2, screenHeight);
	    }

		if (Time.timeSinceLevelLoad - lastMovementTime >= delayBetweenMovements) {

			foreach (UnityEngine.Touch touch in Input.touches) {

				if (bto.HitTest (touch.position)) {

					if (touch.phase != TouchPhase.Ended) {
						if (bto.name == "direito") {

							orientation = Mathf.Abs (player.transform.localScale.x);
							if (player.transform.localScale.x != orientation) { //Looking to other direction
								player.transform.localScale = new Vector3 (-player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
								player.transform.Translate (Vector3.left * rotationOffset);
							} else if (player.transform.position.x + moveDistance <= maxBounds) //Doesnt allow player reach area outside bounds
								player.transform.Translate (Vector3.right * moveDistance);

							lastMovementTime = Time.timeSinceLevelLoad;

						}


						if (bto.name == "esquerdo") {
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
