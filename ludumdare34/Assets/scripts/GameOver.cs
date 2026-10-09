using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour {

    public GUISkin skinMenu;
    public Texture2D btnMenuPlay;
    public Texture2D titulo;
    public Texture2D btnVoltar;

    public UnityEngine.UI.Text pontos;
    public UnityEngine.UI.Text recorde;


    // Use this for initialization
    void Start() {
        pontos.text = PlayerPrefs.GetInt("pontuacao").ToString();
        recorde.text = PlayerPrefs.GetInt("recorde").ToString();
    }


    void OnGUI() {
        GUI.skin = skinMenu;
        ///GUI.DrawTexture(new Rect (Screen.width/2 - titulo.width/2, Screen.height/2 - titulo.height/2,titulo.width,titulo.height),titulo);

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
