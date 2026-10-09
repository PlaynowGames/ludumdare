using UnityEngine;
using UnityEngine.UI;

namespace PizzaPanic {

    // Tela de instruções exibida só na primeira partida. O jogo fica pausado até o jogador tocar/apertar algo.
    public class Tutorial : MonoBehaviour {

        private const string Chave = "tutorialVisto";

        // Evita fechar sem querer com o mesmo clique que abriu a cena
        private const float TempoMinimo = 0.4f;

        public static bool Ativo { get; private set; }

        public static bool DeveMostrar {
            get { return PlayerPrefs.GetInt(Chave, 0) == 0; }
        }

        private GameObject _painel;
        private float _inicio;

        public void Montar(Transform canvas) {
            string controles = Application.isMobilePlatform
                ? "TOQUE NA METADE ESQUERDA OU DIREITA DA TELA PARA SE MOVER"
                : "USE AS SETAS (OU A / D) PARA SE MOVER";

            Image fundo = UiFactory.CriarImagem(canvas, "PainelTutorial", new Color(0f, 0f, 0f, 0.85f));
            fundo.raycastTarget = true;
            UiFactory.Esticar(fundo.rectTransform);
            _painel = fundo.gameObject;

            Text titulo = UiFactory.CriarTexto(fundo.transform, "Titulo", "COMO JOGAR", 52, TextAnchor.MiddleCenter, new Color(1f, 0.85f, 0.2f));
            UiFactory.Ancorar(titulo.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 240f), new Vector2(900f, 80f));

            string instrucoes =
                controles + "\n\n" +
                "PEGUE AS PIZZAS QUE CAEM NA SUA BANDEJA\n" +
                "ENCOSTE NO BAU DO MOTOBOY PARA ENTREGAR\n" +
                "ENTREGUE RAPIDO PARA FAZER COMBO\n" +
                "BANDEJA CHEIA DEIXA VOCE MAIS LENTO\n\n" +
                "DOURADA = x5   VERDE = +1 VIDA   QUEIMADA = EVITE!\n" +
                "NAO DEIXE AS PIZZAS CAIREM NO CHAO";
            Text texto = UiFactory.CriarTexto(fundo.transform, "Texto", instrucoes, 26, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Ancorar(texto.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 20f), new Vector2(1100f, 380f));

            Text continuar = UiFactory.CriarTexto(fundo.transform, "Continuar", "TOQUE OU APERTE UMA TECLA PARA COMECAR", 28, TextAnchor.MiddleCenter, new Color(1f, 0.6f, 0.2f));
            UiFactory.Ancorar(continuar.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -250f), new Vector2(1000f, 60f));

            _inicio = Time.realtimeSinceStartup;
            Ativo = true;
            Time.timeScale = 0f;
        }

        private void Update() {
            if (!Ativo || Time.realtimeSinceStartup - _inicio < TempoMinimo) {
                return;
            }

            bool toque = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
            if (Input.anyKeyDown || toque) {
                Fechar();
            }
        }

        private void Fechar() {
            PlayerPrefs.SetInt(Chave, 1);
            PlayerPrefs.Save();

            Ativo = false;
            Time.timeScale = 1f;
            Destroy(_painel);
            Destroy(this);
        }

        private void OnDestroy() {
            Ativo = false;
        }
    }
}
