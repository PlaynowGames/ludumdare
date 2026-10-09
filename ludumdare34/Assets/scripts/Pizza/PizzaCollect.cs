using UnityEngine;
using System.Collections;

public class PizzaCollect : MonoBehaviour {

    private BoxCollider2D bandeja;

    // Use this for initialization
    void Start() {
        bandeja = GameObject.FindGameObjectWithTag("bandeja").GetComponent<BoxCollider2D>() as BoxCollider2D;
    }

    void OnTriggerEnter2D(Collider2D colisor) {

        if (colisor.gameObject.tag == "bandeja" || colisor.gameObject.tag == "pizza") {
            Debug.Log("colidiu na caçamba");
            bandeja.enabled = false;
        }

    }

    void OnTriggerExit2D(Collider2D colisor) {

        Debug.Log("SAIUUUUUUUUUU" + colisor.gameObject.tag);

        if (colisor.gameObject.tag == "bandeja") {
            Debug.Log("saiu da caçandaco");
            bandeja.enabled = true;
        }
    }
}
