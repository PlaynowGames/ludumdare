using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using PizzaPanic;

public class Vidas : MonoBehaviour {


	public Sprite[] vidaAtual;
	private Image imagem;
	private int contador;

	void Start () {
		imagem = GetComponent<Image>();
		imagem.sprite = vidaAtual[0];
	}

	// Remove uma vida do jogador. Retorna false quando não há mais vidas (game over).
	public bool ExcluirVida(){
		if (contador >= vidaAtual.Length - 1) {
			return false;
		}

		contador++;
		imagem.sprite = vidaAtual[contador];
		GameEvents.NotificarVidaPerdida();
		return true;
	}

	// Devolve uma vida ao jogador (pizza verde). Retorna false se já está com todas.
	public bool GanharVida(){
		if (contador <= 0) {
			return false;
		}

		contador--;
		imagem.sprite = vidaAtual[contador];
		GameEvents.NotificarVidaGanha();
		return true;
	}
}
