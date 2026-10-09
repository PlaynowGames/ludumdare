using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class btoController : MonoBehaviour {


	public void botaoPlay(){
		SceneManager.LoadScene(1);
		scoreAdder.Inicializar ();
	}


	public  void botaoQuit(){
		
		Application.Quit();
		Debug.Log ("Saiu do jogo");

	}


}
