using UnityEngine;

namespace PizzaPanic {

    // Marca uma pizza como especial. Pizzas sem este componente são normais.
    public class PizzaEspecial : MonoBehaviour {

        public enum Tipo { Normal, Dourada, Vida, Queimada }

        public Tipo tipo = Tipo.Normal;

        public int Multiplicador {
            get { return tipo == Tipo.Dourada ? 5 : 1; }
        }

        // Pizza de vida e pizza queimada não custam vida ao cair no chão
        public static bool IgnoraChao(GameObject pizza) {
            PizzaEspecial especial = pizza.GetComponent<PizzaEspecial>();
            return especial != null && (especial.tipo == Tipo.Vida || especial.tipo == Tipo.Queimada);
        }

        public static Tipo TipoDe(GameObject pizza) {
            PizzaEspecial especial = pizza.GetComponent<PizzaEspecial>();
            return especial != null ? especial.tipo : Tipo.Normal;
        }

        public static Color CorDoTipo(Tipo tipo) {
            switch (tipo) {
                case Tipo.Dourada: return new Color(1f, 0.85f, 0.2f);
                case Tipo.Vida: return new Color(0.45f, 1f, 0.45f);
                case Tipo.Queimada: return new Color(0.3f, 0.22f, 0.2f);
                default: return Color.white;
            }
        }

        // Aplica a dificuldade atual e sorteia o tipo de um objeto recém-instanciado pelo spawner
        public static void Configurar(GameObject obj) {
            if (obj == null) {
                return;
            }

            Rigidbody2D corpo = obj.GetComponent<Rigidbody2D>();
            if (corpo != null) {
                corpo.gravityScale *= Difficulty.FatorGravidade;
            }

            if (!obj.CompareTag("pizza")) {
                return;
            }

            Tipo tipo = Difficulty.SortearTipo();
            if (tipo == Tipo.Normal) {
                return;
            }

            PizzaEspecial especial = obj.AddComponent<PizzaEspecial>();
            especial.tipo = tipo;

            SpriteRenderer desenho = obj.GetComponentInChildren<SpriteRenderer>();
            if (desenho != null) {
                desenho.color = CorDoTipo(tipo);
            }
        }
    }
}
