using UnityEngine;

namespace PizzaPanic {

    // Volume geral e mudo, salvos em PlayerPrefs
    public static class VolumeSettings {

        private const string ChaveVolume = "volume";
        private const string ChaveMudo = "mudo";

        public static float Volume { get; private set; }
        public static bool Mudo { get; private set; }

        public static void Carregar() {
            Volume = PlayerPrefs.GetFloat(ChaveVolume, 1f);
            Mudo = PlayerPrefs.GetInt(ChaveMudo, 0) == 1;
            Aplicar();
        }

        public static void AlterarVolume(float delta) {
            Volume = Mathf.Clamp01(Mathf.Round((Volume + delta) * 10f) / 10f);
            Salvar();
        }

        public static void AlternarMudo() {
            Mudo = !Mudo;
            Salvar();
        }

        private static void Salvar() {
            PlayerPrefs.SetFloat(ChaveVolume, Volume);
            PlayerPrefs.SetInt(ChaveMudo, Mudo ? 1 : 0);
            PlayerPrefs.Save();
            Aplicar();
        }

        private static void Aplicar() {
            AudioListener.volume = Mudo ? 0f : Volume;
        }
    }
}
