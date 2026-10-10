using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace PizzaPanic {

    // Botão de pausa no canto da tela (e Esc/P), com volume, mudo e volta ao menu
    public class PauseMenu : MonoBehaviour {

        private GameObject _painel;
        private Text _textoVolume;
        private Text _rotuloMudo;
        private bool _pausado;

        public void Montar(Transform canvas) {
            UiFactory.GarantirEventSystem();

            Button pausar = UiFactory.CriarBotao(canvas, "BotaoPausa", "II", 32, Alternar);
            UiFactory.Ancorar((RectTransform)pausar.transform, new Vector2(1f, 1f), new Vector2(1f, 1f), new Vector2(-20f, -20f), new Vector2(70f, 70f));

            Image fundo = UiFactory.CriarImagem(canvas, "PainelPausa", new Color(0f, 0f, 0f, 0.78f));
            fundo.raycastTarget = true;
            UiFactory.Esticar(fundo.rectTransform);
            _painel = fundo.gameObject;

            Text titulo = UiFactory.CriarTexto(fundo.transform, "Titulo", "PAUSADO", 56, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Ancorar(titulo.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 230f), new Vector2(600f, 80f));

            Button continuar = UiFactory.CriarBotao(fundo.transform, "Continuar", "CONTINUAR", 32, Alternar);
            UiFactory.Ancorar((RectTransform)continuar.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 130f), new Vector2(380f, 70f));

            Button menos = UiFactory.CriarBotao(fundo.transform, "VolumeMenos", "-", 36, DiminuirVolume);
            UiFactory.Ancorar((RectTransform)menos.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(-150f, 30f), new Vector2(70f, 70f));

            _textoVolume = UiFactory.CriarTexto(fundo.transform, "Volume", "", 28, TextAnchor.MiddleCenter, Color.white);
            UiFactory.Ancorar(_textoVolume.rectTransform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, 30f), new Vector2(220f, 70f));

            Button mais = UiFactory.CriarBotao(fundo.transform, "VolumeMais", "+", 36, AumentarVolume);
            UiFactory.Ancorar((RectTransform)mais.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(150f, 30f), new Vector2(70f, 70f));

            Button mudo = UiFactory.CriarBotao(fundo.transform, "Mudo", "", 28, AlternarMudo);
            UiFactory.Ancorar((RectTransform)mudo.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -70f), new Vector2(380f, 70f));
            _rotuloMudo = mudo.GetComponentInChildren<Text>();

            Button menu = UiFactory.CriarBotao(fundo.transform, "Menu", "VOLTAR AO MENU", 28, VoltarAoMenu);
            UiFactory.Ancorar((RectTransform)menu.transform, new Vector2(0.5f, 0.5f), new Vector2(0.5f, 0.5f), new Vector2(0f, -170f), new Vector2(380f, 70f));

            AtualizarTextos();
            _painel.SetActive(false);
        }

        private void Update() {
            if (Tutorial.Ativo) {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) {
                Alternar();
            }
        }

        public void Alternar() {
            if (Tutorial.Ativo) {
                return;
            }

            _pausado = !_pausado;
            _painel.SetActive(_pausado);
            Time.timeScale = _pausado ? 0f : 1f;
            AudioListener.pause = _pausado;
        }

        private void DiminuirVolume() {
            VolumeSettings.AlterarVolume(-0.1f);
            AtualizarTextos();
        }

        private void AumentarVolume() {
            VolumeSettings.AlterarVolume(0.1f);
            AtualizarTextos();
        }

        private void AlternarMudo() {
            VolumeSettings.AlternarMudo();
            AtualizarTextos();
        }

        private void VoltarAoMenu() {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            SceneManager.LoadScene(0);
        }

        private void AtualizarTextos() {
            _textoVolume.text = "VOLUME " + Mathf.RoundToInt(VolumeSettings.Volume * 100f) + "%";
            _rotuloMudo.text = VolumeSettings.Mudo ? "SOM: DESLIGADO" : "SOM: LIGADO";
        }
    }
}
