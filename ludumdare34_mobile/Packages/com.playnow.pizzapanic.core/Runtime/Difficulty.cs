using UnityEngine;

namespace PizzaPanic {

    // Curva de dificuldade: cresce com o tempo de partida e com o número de entregas
    public static class Difficulty {

        public const int NivelMaximo = 12;

        public static int Nivel {
            get {
                int porTempo = (int)(Time.timeSinceLevelLoad / 25f);
                int porEntregas = GameEvents.Entregas / 8;
                return Mathf.Min(1 + porTempo + porEntregas, NivelMaximo);
            }
        }

        // Multiplicador da gravidade das pizzas que caem (1.0 no nível 1, até ~1.55)
        public static float FatorGravidade {
            get { return 1f + 0.05f * (Nivel - 1); }
        }

        // A partir do nível 3, às vezes caem duas pizzas ao mesmo tempo
        public static float ChanceSpawnDuplo {
            get { return Nivel < 3 ? 0f : Mathf.Min(0.12f * (Nivel - 2), 0.5f); }
        }

        // Sorteia o tipo da próxima pizza. No nível 1 só caem pizzas normais.
        public static PizzaEspecial.Tipo SortearTipo() {
            int nivel = Nivel;
            if (nivel < 2) {
                return PizzaEspecial.Tipo.Normal;
            }

            float queimada = Mathf.Min(0.05f + 0.01f * nivel, 0.15f);
            float dourada = 0.07f;
            // Só cai pizza de vida se o jogador já perdeu alguma
            float vida = GameEvents.VidasPerdidas > 0 ? 0.05f : 0f;

            float r = Random.value;
            if (r < queimada) {
                return PizzaEspecial.Tipo.Queimada;
            }
            r -= queimada;
            if (r < dourada) {
                return PizzaEspecial.Tipo.Dourada;
            }
            r -= dourada;
            if (r < vida) {
                return PizzaEspecial.Tipo.Vida;
            }
            return PizzaEspecial.Tipo.Normal;
        }

        // Conta as pizzas empilhadas na bandeja
        public static int PizzasNaBandeja(Transform bandeja) {
            if (bandeja == null) {
                return 0;
            }

            int total = 0;
            foreach (Transform filho in bandeja) {
                if (filho.CompareTag("pizza")) {
                    total++;
                }
            }
            return total;
        }

        // Quanto mais pizzas na bandeja, mais lento o jogador (1.0 sem peso, até 2.0)
        public static float FatorPeso(int pizzasNaBandeja) {
            return Mathf.Min(1f + 0.12f * pizzasNaBandeja, 2f);
        }
    }
}
