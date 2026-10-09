using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class bandeja : MonoBehaviour {

    private BoxCollider2D bandej;
    private GameObject bandejaObj;

    // Use this for initialization
    void Start() {
        bandejaObj = GameObject.FindGameObjectWithTag("bandeja");
        bandej = bandejaObj.GetComponent<BoxCollider2D>() as BoxCollider2D;
    }

    void OnTriggerEnter2D(Collider2D colisor) {

        if (colisor.gameObject.tag == "triggerBau") {
            bandej.enabled = false;

            List<Transform> unparent = new List<Transform>(transform.childCount);

            foreach (Transform child in bandejaObj.transform)
                if (child.tag == "pizza") {
                    unparent.Add(child);
                    child.GetComponent<Pizza>().naBandeja = false;
                }
            foreach (Transform child in unparent) {
                child.parent = null;
            }
        }
    }

    void OnTriggerExit2D(Collider2D colisor) {

        if (colisor.gameObject.tag == "triggerBau") {
            bandej.enabled = true;
        }
    }
}

