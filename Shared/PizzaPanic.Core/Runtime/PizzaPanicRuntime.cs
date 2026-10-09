using UnityEngine;
using UnityEngine.SceneManagement;

namespace PizzaPanic {

    // Sobe sozinho com o jogo (sem precisar de objeto na cena) e monta o HUD novo na cena de gameplay
    public class PizzaPanicRuntime : MonoBehaviour {

        private const string CenaJogo = "Cena1";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Iniciar() {
            GameObject go = new GameObject("PizzaPanicRuntime");
            DontDestroyOnLoad(go);
            go.AddComponent<PizzaPanicRuntime>();
        }

        private void Awake() {
            VolumeSettings.Carregar();
            SceneManager.sceneLoaded += AoCarregarCena;
        }

        private void OnDestroy() {
            SceneManager.sceneLoaded -= AoCarregarCena;
        }

        private void AoCarregarCena(Scene cena, LoadSceneMode modo) {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            GameEvents.Resetar();

            if (cena.name != CenaJogo) {
                return;
            }

            Canvas canvas = UiFactory.CriarCanvas("PizzaPanicHud", 50);

            canvas.gameObject.AddComponent<GameFeel>();
            canvas.gameObject.AddComponent<ComboHud>().Montar(canvas.transform);
            canvas.gameObject.AddComponent<PauseMenu>().Montar(canvas.transform);

            // O tutorial é o último para ficar por cima de tudo
            if (Tutorial.DeveMostrar) {
                canvas.gameObject.AddComponent<Tutorial>().Montar(canvas.transform);
            }
        }
    }
}
