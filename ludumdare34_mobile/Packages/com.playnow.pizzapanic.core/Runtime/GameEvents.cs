using System;
using UnityEngine;

namespace PizzaPanic {

    // Dados de uma entrega feita no baú do motoboy
    public struct EntregaInfo {
        public int combo;
        public int pontos;
        public float janela;
        public Vector3 posicao;
        public PizzaEspecial.Tipo tipo;
    }

    // Ponto único de comunicação entre o gameplay (Bau, Vidas...) e os sistemas de HUD/feedback/dificuldade
    public static class GameEvents {

        public static event Action<EntregaInfo> Entrega;

        public static int Entregas { get; private set; }
        public static int VidasPerdidas { get; private set; }

        public static void Resetar() {
            Entregas = 0;
            VidasPerdidas = 0;
        }

        public static void NotificarEntrega(int combo, int pontos, float janela, Vector3 posicao, PizzaEspecial.Tipo tipo) {
            if (tipo != PizzaEspecial.Tipo.Queimada) {
                Entregas++;
            }

            Action<EntregaInfo> ouvintes = Entrega;
            if (ouvintes != null) {
                EntregaInfo info = new EntregaInfo();
                info.combo = combo;
                info.pontos = pontos;
                info.janela = janela;
                info.posicao = posicao;
                info.tipo = tipo;
                ouvintes(info);
            }
        }

        public static void NotificarVidaPerdida() {
            VidasPerdidas++;
        }

        public static void NotificarVidaGanha() {
            if (VidasPerdidas > 0) {
                VidasPerdidas--;
            }
        }
    }
}
