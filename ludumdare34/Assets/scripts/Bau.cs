using UnityEngine;
using System.Collections;

public class Bau : MonoBehaviour {

    public float multiplicatorTimeLimit = 1;

    private Score score;
    private int pontos = 10;
    private AudioSource audio;

    private float timeSinceLastCollision;
    private int multiplicationRatio = 1;
    private bool cheio;

    // Use this for initialization
    void Start() {
        score = GameObject.FindGameObjectWithTag("pontos").GetComponent<Score>();
        audio = gameObject.GetComponent<AudioSource>();
        cheio = false;
    }

    // Update is called once per frame
    void Update() {
        if (cheio && Time.timeSinceLevelLoad - timeSinceLastCollision > multiplicatorTimeLimit) {
            cheio = false;
            transform.parent.GetComponent<MotoboyController>().Go();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("Estou aqui!!" + other.gameObject.name);

        if (other.gameObject.tag == "pizza") {
            audio.Play();

            if (Time.timeSinceLevelLoad - timeSinceLastCollision > multiplicatorTimeLimit) {
                multiplicationRatio = 1;
            } else {
                multiplicationRatio++;
            }

            score.SomarPonto(multiplicationRatio * pontos);
            score.Recorde();
            score.Pontuacao();

            timeSinceLastCollision = Time.timeSinceLevelLoad;
            cheio = true;

            Destroy(other.gameObject);
        }
    }
}
