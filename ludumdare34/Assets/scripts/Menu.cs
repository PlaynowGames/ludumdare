using UnityEngine;
using UnityEngine.SceneManagement;
using PizzaPanic;

public class Menu : MonoBehaviour {

    // Campos mantidos para não perder as referências já salvas na cena
    public GUISkin skinMenu;
    public Texture2D btnMenuPlay;
    public Texture2D titulo;
    public Texture2D btnVoltar;

    void Start() {
        MenuBotoes.Criar(btnMenuPlay, btnVoltar, Jogar, Sair);
    }

    void Jogar() {
        Score.Inicializar();
        SceneManager.LoadScene(1);
    }

    void Sair() {
        Application.Quit();
        Debug.Log("Saiu do jogo");
    }
}
