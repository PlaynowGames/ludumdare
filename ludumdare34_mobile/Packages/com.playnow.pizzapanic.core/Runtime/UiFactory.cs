using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PizzaPanic {

    // Monta elementos de uGUI por código, já que as cenas do projeto não são editadas à mão
    public static class UiFactory {

        private static Font _fonte;

        public static Font Fonte {
            get {
                if (_fonte == null) {
                    _fonte = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
                }
                return _fonte;
            }
        }

        public static Canvas CriarCanvas(string nome, int ordem) {
            GameObject go = new GameObject(nome);
            Canvas canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = ordem;

            CanvasScaler escala = go.AddComponent<CanvasScaler>();
            escala.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            escala.referenceResolution = new Vector2(1280f, 720f);
            escala.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            escala.matchWidthOrHeight = 0.5f;

            go.AddComponent<GraphicRaycaster>();
            return canvas;
        }

        public static RectTransform CriarRect(string nome, Transform pai) {
            GameObject go = new GameObject(nome, typeof(RectTransform));
            go.transform.SetParent(pai, false);
            return (RectTransform)go.transform;
        }

        public static void Ancorar(RectTransform rect, Vector2 ancora, Vector2 pivo, Vector2 posicao, Vector2 tamanho) {
            rect.anchorMin = ancora;
            rect.anchorMax = ancora;
            rect.pivot = pivo;
            rect.anchoredPosition = posicao;
            rect.sizeDelta = tamanho;
        }

        public static void Esticar(RectTransform rect) {
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
        }

        public static Image CriarImagem(Transform pai, string nome, Color cor) {
            RectTransform rect = CriarRect(nome, pai);
            Image imagem = rect.gameObject.AddComponent<Image>();
            imagem.color = cor;
            imagem.raycastTarget = false;
            return imagem;
        }

        public static Text CriarTexto(Transform pai, string nome, string conteudo, int tamanho, TextAnchor alinhamento, Color cor) {
            RectTransform rect = CriarRect(nome, pai);
            Text texto = rect.gameObject.AddComponent<Text>();
            texto.font = Fonte;
            texto.text = conteudo;
            texto.fontSize = tamanho;
            texto.alignment = alinhamento;
            texto.color = cor;
            texto.horizontalOverflow = HorizontalWrapMode.Wrap;
            texto.verticalOverflow = VerticalWrapMode.Overflow;
            texto.raycastTarget = false;

            Shadow sombra = rect.gameObject.AddComponent<Shadow>();
            sombra.effectDistance = new Vector2(2f, -2f);
            return texto;
        }

        public static Button CriarBotao(Transform pai, string nome, string rotulo, int tamanho, UnityAction aoClicar) {
            Image fundo = CriarImagem(pai, nome, new Color(0.15f, 0.15f, 0.22f, 0.95f));
            fundo.raycastTarget = true;

            Button botao = fundo.gameObject.AddComponent<Button>();
            botao.targetGraphic = fundo;

            Text texto = CriarTexto(fundo.transform, "Rotulo", rotulo, tamanho, TextAnchor.MiddleCenter, Color.white);
            Esticar(texto.rectTransform);

            botao.onClick.AddListener(aoClicar);
            return botao;
        }

        public static void GarantirEventSystem() {
            if (Object.FindFirstObjectByType<EventSystem>() != null) {
                return;
            }
            new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
        }
    }
}
