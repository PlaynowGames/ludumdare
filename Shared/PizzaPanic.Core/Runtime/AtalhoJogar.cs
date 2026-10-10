using UnityEngine;
using UnityEngine.Events;

namespace PizzaPanic {

    // Enter ou Espaço também iniciam o jogo nos menus
    public class AtalhoJogar : MonoBehaviour {

        public UnityAction aoJogar;

        private void Update() {
            if (aoJogar != null && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space))) {
                aoJogar();
            }
        }
    }
}
