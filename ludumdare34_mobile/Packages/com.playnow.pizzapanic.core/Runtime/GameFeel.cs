using UnityEngine;

namespace PizzaPanic {

    // Feedback de entrega: tremor de câmera e explosão de partículas, mais fortes conforme o combo
    public class GameFeel : MonoBehaviour {

        private Material _materialParticula;

        private Transform _camera;
        private Vector3 _origemCamera;
        private bool _tremendo;
        private float _tremorFim;
        private float _tremorDuracao = 0.01f;
        private float _intensidade;

        private void OnEnable() {
            GameEvents.Entrega += AoEntregar;
        }

        private void OnDisable() {
            GameEvents.Entrega -= AoEntregar;
        }

        private void AoEntregar(EntregaInfo entrega) {
            if (entrega.tipo == PizzaEspecial.Tipo.Queimada) {
                Tremer(0.25f, 0.3f);
                Explodir(entrega.posicao, new Color(0.35f, 0.3f, 0.3f), 8);
                return;
            }

            Tremer(Mathf.Min(0.04f + 0.02f * entrega.combo, 0.2f), 0.2f);

            Color cor = entrega.tipo == PizzaEspecial.Tipo.Dourada ? PizzaEspecial.CorDoTipo(PizzaEspecial.Tipo.Dourada)
                      : entrega.tipo == PizzaEspecial.Tipo.Vida ? PizzaEspecial.CorDoTipo(PizzaEspecial.Tipo.Vida)
                      : new Color(1f, 0.6f, 0.15f);
            Explodir(entrega.posicao, cor, 10 + 4 * Mathf.Min(entrega.combo, 8));
        }

        private void Tremer(float intensidade, float duracao) {
            if (_camera == null) {
                Camera principal = Camera.main;
                if (principal == null) {
                    return;
                }
                _camera = principal.transform;
            }

            if (!_tremendo) {
                _origemCamera = _camera.localPosition;
                _tremendo = true;
            }

            _intensidade = Mathf.Max(intensidade, _intensidade);
            _tremorDuracao = duracao;
            _tremorFim = Time.time + duracao;
        }

        private void LateUpdate() {
            if (!_tremendo) {
                return;
            }

            if (_camera == null) {
                _tremendo = false;
                return;
            }

            float restante = _tremorFim - Time.time;
            if (restante <= 0f) {
                _camera.localPosition = _origemCamera;
                _tremendo = false;
                _intensidade = 0f;
                return;
            }

            Vector2 deslocamento = Random.insideUnitCircle * _intensidade * (restante / _tremorDuracao);
            _camera.localPosition = _origemCamera + new Vector3(deslocamento.x, deslocamento.y, 0f);
        }

        private Material ObterMaterial() {
            if (_materialParticula != null) {
                return _materialParticula;
            }

            Shader shader = Shader.Find("Sprites/Default");
            if (shader != null) {
                _materialParticula = new Material(shader);
            } else {
                // Plano B: reaproveita o material de qualquer sprite da cena
                SpriteRenderer qualquer = FindFirstObjectByType<SpriteRenderer>();
                if (qualquer != null) {
                    _materialParticula = qualquer.sharedMaterial;
                }
            }
            return _materialParticula;
        }

        private void Explodir(Vector3 posicao, Color cor, int quantidade) {
            Material material = ObterMaterial();
            if (material == null) {
                return;
            }

            GameObject go = new GameObject("FxEntrega");
            go.transform.position = posicao;

            ParticleSystem particulas = go.AddComponent<ParticleSystem>();
            particulas.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

            ParticleSystem.MainModule principal = particulas.main;
            principal.loop = false;
            principal.playOnAwake = false;
            principal.startLifetime = 0.7f;
            principal.startSpeed = new ParticleSystem.MinMaxCurve(2f, 5f);
            principal.startSize = new ParticleSystem.MinMaxCurve(0.1f, 0.25f);
            principal.startColor = cor;
            principal.gravityModifier = 1f;

            ParticleSystem.EmissionModule emissao = particulas.emission;
            emissao.rateOverTime = 0f;
            emissao.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0f, (short)quantidade) });

            ParticleSystem.ShapeModule forma = particulas.shape;
            forma.shapeType = ParticleSystemShapeType.Circle;
            forma.radius = 0.2f;

            ParticleSystemRenderer desenho = go.GetComponent<ParticleSystemRenderer>();
            desenho.material = material;
            desenho.sortingOrder = 100;

            particulas.Play();
            Destroy(go, 2f);
        }
    }
}
