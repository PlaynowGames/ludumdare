using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Vidas : MonoBehaviour {


    public Sprite[] vidaAtual;
    private Image imagem;
    private int contador;

    void Start() {
        imagem = GetComponent<Image>();
        imagem.sprite = vidaAtual[0];
    }

    // Remove uma vida do jogador. Retorna false quando não há mais vidas (game over).
    public bool ExcluirVida() {
        if (contador >= vidaAtual.Length - 1) {
            return false;
        }

        contador++;
        imagem.sprite = vidaAtual[contador];
        return true;
    }
}
