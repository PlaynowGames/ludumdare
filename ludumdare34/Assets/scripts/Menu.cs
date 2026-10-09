using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Menu : MonoBehaviour {

    public GUISkin skinMenu;
    public Texture2D btnMenuPlay;
    public Texture2D titulo;
    public Texture2D btnVoltar;

    void OnGUI() {
        GUI.skin = skinMenu;

        bool play = GUI.Button(new Rect(Screen.width - 950, Screen.height - 100, 178, 80), btnMenuPlay);
        bool sair = GUI.Button(new Rect(Screen.width - 250, Screen.height - 100, 178, 80), btnVoltar);

        if (play) {
            SceneManager.LoadScene(1);
            Score.Inicializar();
        }

        if (sair) {
            Application.Quit();
            Debug.Log("Saiu do jogo");
        }
    }
}
