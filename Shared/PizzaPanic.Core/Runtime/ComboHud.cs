using UnityEngine;
using UnityEngine.UI;

namespace PizzaPanic {

    // Mostra "COMBO xN" e uma barra com o tempo restante da janela do combo
    public class ComboHud : MonoBehaviour {

        private const float LarguraBarra = 260f;
        private const float AlturaBarra = 12f;

        private RectTransform _raiz;
        private Text _texto;
        private Image _barra;
        private RectTransform _barraRect;

        private float _fim;
        private float _duracao = 1f;
        private float _pulso;

        public void Montar(Transform canvas) {
            _raiz = UiFactory.CriarRect("Combo", canvas);
            UiFactory.Ancorar(_raiz, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), new Vector2(0f, -70f), new Vector2(LarguraBarra + 80f, 110f));

            _texto = UiFactory.CriarTexto(_raiz, "Texto", "", 34, TextAnchor.UpperCenter, Color.white);
            UiFactory.Ancorar(_texto.rectTransform, new Vector2(0.5f, 1f), new Vector2(0.5f, 1f), Vector2.zero, new Vector2(LarguraBarra + 80f, 90f));

            Image fundo = UiFactory.CriarImagem(_raiz, "BarraFundo", new Color(0f, 0f, 0f, 0.5f));
            UiFactory.Ancorar(fundo.rectTransform, new Vector2(0.5f, 0f), new Vector2(0.5f, 0f), new Vector2(0f, 4f), new Vector2(LarguraBarra, AlturaBarra));

            _barra = UiFactory.CriarImagem(fundo.transform, "Barra", Color.yellow);
            _barraRect = _barra.rectTransform;
            UiFactory.Ancorar(_barraRect, new Vector2(0f, 0.5f), new Vector2(0f, 0.5f), Vector2.zero, new Vector2(LarguraBarra, AlturaBarra));

            _raiz.gameObject.SetActive(false);
        }

        private void OnEnable() {
            GameEvents.Entrega += AoEntregar;
        }

        private void OnDisable() {
            GameEvents.Entrega -= AoEntregar;
        }

        private void AoEntregar(EntregaInfo entrega) {
            if (_raiz == null) {
                return;
            }

            string mensagem;
            Color cor;

            if (entrega.tipo == PizzaEspecial.Tipo.Queimada) {
                mensagem = "COMBO PERDIDO!";
                cor = new Color(1f, 0.3f, 0.3f);
                _duracao = 1f;
            } else {
                mensagem = "COMBO x" + entrega.combo;
                if (entrega.tipo == PizzaEspecial.Tipo.Vida) {
                    mensagem += "\n+1 VIDA";
                } else if (entrega.tipo == PizzaEspecial.Tipo.Dourada) {
                    mensagem += "\nDOURADA!";
                }
                cor = Color.Lerp(Color.white, new Color(1f, 0.55f, 0.1f), Mathf.Clamp01((entrega.combo - 1) / 5f));
                _duracao = Mathf.Max(entrega.janela, 0.1f);
            }

            _fim = Time.timeSinceLevelLoad + _duracao;
            _pulso = 1f;
            _texto.text = mensagem;
            _texto.color = cor;
            _barra.color = cor;
            _raiz.gameObject.SetActive(true);
        }

        private void Update() {
            if (_raiz == null || !_raiz.gameObject.activeSelf) {
                return;
            }

            float restante = _fim - Time.timeSinceLevelLoad;
            if (restante <= 0f) {
                _raiz.gameObject.SetActive(false);
                return;
            }

            _barraRect.sizeDelta = new Vector2(LarguraBarra * Mathf.Clamp01(restante / _duracao), AlturaBarra);

            _pulso = Mathf.MoveTowards(_pulso, 0f, Time.unscaledDeltaTime * 4f);
            _texto.rectTransform.localScale = Vector3.one * (1f + 0.4f * _pulso);
        }
    }
}
