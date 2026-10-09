using UnityEngine;
using System.Collections;
using PizzaPanic;

public class DiscreteSpawner : MonoBehaviour {

    public GameObject[] spawnPrefabs;
    public float[] positions;
    public float height;
    public float timeBetweenSpawns;
    public float minTimeBetweenSpawns;
    public float reduceTimeRatio;

    private GameObject currentObj;
    private float timeOfLastSpawn;
    private Vector3 currentSpawnPos;
    private int lastPositionIndex = -1;

    // Use this for initialization
    void Start() {
        timeOfLastSpawn = Time.timeSinceLevelLoad;
        currentSpawnPos = new Vector3(0, height);
    }

    // Update is called once per frame
    void Update() {
        if (Time.timeSinceLevelLoad >= timeOfLastSpawn + timeBetweenSpawns) {

            Spawn();
            // Em níveis mais altos caem duas pizzas ao mesmo tempo
            if (Random.value < Difficulty.ChanceSpawnDuplo) {
                Spawn();
            }

            timeOfLastSpawn = Time.timeSinceLevelLoad;
        }
        timeBetweenSpawns = Mathf.Clamp(timeBetweenSpawns - reduceTimeRatio * Time.deltaTime, minTimeBetweenSpawns, timeBetweenSpawns);
    }

    // Cria uma pizza numa posição diferente da anterior, já com a dificuldade e o tipo sorteados
    private void Spawn() {
        int index = Random.Range(0, positions.Length);
        if (positions.Length > 1 && index == lastPositionIndex) {
            index = (index + 1 + Random.Range(0, positions.Length - 1)) % positions.Length;
        }
        lastPositionIndex = index;

        currentSpawnPos.x = positions[index];
        GameObject obj = Instantiate(spawnPrefabs[Random.Range(0, spawnPrefabs.Length)], currentSpawnPos, transform.rotation) as GameObject;
        PizzaEspecial.Configurar(obj);
    }
}
