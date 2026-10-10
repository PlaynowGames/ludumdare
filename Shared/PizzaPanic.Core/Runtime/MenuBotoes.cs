using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PizzaPanic {

    // Botões Jogar/Sair dos menus (Menu e GameOver) em uGUI, no lugar do antigo OnGUI.
    // Usam as mesmas texturas de antes e escalam junto com a resolução da janela.
    public static class MenuBotoes {

        private static readonly Vector2 Tamanho = new Vector2(178f, 80f);

        // Posições a partir do canto inferior direito, na resolução de referência (1280x720)
        private static readonly Vector2 PosicaoJogar = new Vector2(-950f, 20f);
        private static readonly Vector2 PosicaoSair = new Vector2(-250f, 20f);

        public static void Criar(Texture2D jogar, Texture2D sair, UnityAction aoJogar, UnityAction aoSair) {
            UiFactory.GarantirEventSystem();

            Canvas canvas = UiFactory.CriarCanvas("MenuBotoes", 100);
            CriarBotao(canvas.transform, "Jogar", jogar, "JOGAR", PosicaoJogar, aoJogar);
            CriarBotao(canvas.transform, "Sair", sair, "SAIR", PosicaoSair, aoSair);

            AtalhoJogar atalho = canvas.gameObject.AddComponent<AtalhoJogar>();
            atalho.aoJogar = aoJogar;
        }

        private static void CriarBotao(Transform pai, string nome, Texture2D textura, string rotulo, Vector2 posicao, UnityAction aoClicar) {
            Button botao;

            if (textura != null) {
                RectTransform rect = UiFactory.CriarRect(nome, pai);
                RawImage imagem = rect.gameObject.AddComponent<RawImage>();
                imagem.texture = textura;

                botao = rect.gameObject.AddComponent<Button>();
                botao.targetGraphic = imagem;

                ColorBlock cores = botao.colors;
                cores.highlightedColor = new Color(0.85f, 0.85f, 0.85f);
                cores.pressedColor = new Color(0.6f, 0.6f, 0.6f);
                botao.colors = cores;

                botao.onClick.AddListener(aoClicar);
            } else {
                // Textura não configurada na cena: usa um botão de texto
                botao = UiFactory.CriarBotao(pai, nome, rotulo, 28, aoClicar);
            }

            UiFactory.Ancorar((RectTransform)botao.transform, new Vector2(1f, 0f), Vector2.zero, posicao, Tamanho);
        }
    }
}
