using UnityEngine;
using UnityEngine.SceneManagement;
using PizzaPanic;
using System.Collections;
using System.Collections.Generic;

public class Pizza : MonoBehaviour {

    private Transform bandeja;
    public bool naBandeja;

    public float dampTime = 0.15f;
    private Vidas vida;
    private AudioSource audio;
    public AudioSource fail;

    private GameObject go;

    // Use this for initialization
    void Start() {
        go = GameObject.FindGameObjectWithTag("vidas");
        bandeja = GameObject.FindGameObjectWithTag("bandeja").transform;
        audio = gameObject.GetComponent<AudioSource>();
        fail = GameObject.FindGameObjectWithTag("somFail").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update() {

    }

    void OnCollisionEnter2D(Collision2D colisor) {

        // Pizza de vida e pizza queimada não custam vida ao cair no chão
        if (colisor.gameObject.name == "chao" && PizzaEspecial.IgnoraChao(gameObject)) {
            Destroy(gameObject);
            return;
        }

        if (!naBandeja) {
            if (colisor.gameObject.tag == "bandeja" || ((colisor.gameObject.tag == "pizza") && (colisor.transform.GetComponent<Pizza>().naBandeja == true))) {
                this.alterarEstadoPiza(true);
            } else {
                Debug.Log("COLIDIU " + colisor.gameObject.name);
                Destroy(gameObject);
            }
        }

        if (colisor.gameObject.name == "chao") {
            vida = go.GetComponent<Vidas>();
            if (vida.ExcluirVida()) {
                fail.Play();
            } else {
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    // Altera o estado da pizza e aciona o audio caso colida com a badeja
    private void alterarEstadoPiza(bool naBandeja) {
        this.naBandeja = naBandeja;
        transform.parent = bandeja;
        if (naBandeja) {
            audio.Play();
        }

    }
}
