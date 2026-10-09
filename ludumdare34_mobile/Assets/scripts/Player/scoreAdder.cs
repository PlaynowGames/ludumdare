using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class scoreAdder : MonoBehaviour
{
	
	public static int actualScore;
	private GameObject pts;
	private Vector3 target;

	public GameObject myGO;


	void scaleScore(){
		LeanTween.scale (pts, new Vector3 (2f, 2f, 2f), 1f).setDelay (0.2f).setEase (LeanTweenType.easeOutBack).setOnComplete (
			() => {

				LeanTween.scale (pts, new Vector3 (1f, 1f, 1f), 1f).setDelay (0.2f).setEase (LeanTweenType.easeOutBack);
			}
		);
	}



	void Start(){
		target = transform.position;
		pts = GameObject.FindGameObjectWithTag ("pontuacao");
	}



	public void createScoreAnimation(float x, float y, string msg, int score){

		myGO = new GameObject ();
		myGO.transform.parent = this.transform;


		Text text = myGO.AddComponent<Text> ();
		text.name = "ts"+Time.time;
		//text.transform.position = new Vector3(x,y,0);
		text.alignment = TextAnchor.MiddleCenter;
		text.text = msg;

		RectTransform tes = text.GetComponent<RectTransform>(); 
		tes.sizeDelta = new Vector2 (67.4f,20.88f);
		tes.anchoredPosition = new Vector2 (x,y);



		// Arial.ttf embutida foi removida do Unity (2022+); LegacyRuntime.ttf é a substituta
		Font ArialFont = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
		text.font = ArialFont;
		text.material = ArialFont.material;

		float dif = text.gameObject.transform.position.y - y;

		Debug.Log (pts.transform.localPosition.y + " - " + y);

		LeanTween.moveLocal (text.gameObject, new Vector3 (pts.transform.localPosition.x, pts.transform.localPosition.y, 0f), 0.8f).setEase (LeanTweenType.easeOutQuad).setOnComplete (
			() => {

				Destroy (text.gameObject);
				scaleScore();
				actualScore += score;
				pts.GetComponent<Text>().text =  "" + actualScore;
			}
		);


	}

	public void Recorde(){

		if(actualScore > PlayerPrefs.GetInt("recorde")){
			PlayerPrefs.SetInt("recorde",actualScore);
		}

	}

	public void Pontuacao(){
		PlayerPrefs.SetInt ("pontuacao", actualScore);
	}


	public static void Inicializar(){
		actualScore = 0;
	}
}
