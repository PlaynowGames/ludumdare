using UnityEngine;
using System.Collections;

public class MotoboySpawner : MonoBehaviour {


    public GameObject motoboys;
    public GameObject[] spawnPrefabs;
    public float[] respectivePositions;
    public float[] respectiveTargetPositions;
    public float height;
    public float motoboySpeed, motoboyAcceleration;


    private GameObject currentObj;
    private float timeOfLastSpawn;
    private Vector3 currentSpawnPos;

    // Use this for initialization
    void Start() {
        currentSpawnPos = new Vector3(0, height);
        Spawn();
    }

    // Update is called once per frame
    void Update() {

    }

    // Cria e possiciona o motoboy no cenário
    public void Spawn() {
        int i = Random.Range(0, respectivePositions.Length);

        currentSpawnPos.x = respectivePositions[i];

        // Configura a instância, nunca o prefab (senão o asset é alterado no editor)
        currentObj = Instantiate(spawnPrefabs[i], currentSpawnPos, transform.rotation) as GameObject;
        if (motoboys)
            currentObj.transform.parent = motoboys.transform;

        MotoboyController controller = currentObj.GetComponent<MotoboyController>();

        if (currentSpawnPos.x <= respectiveTargetPositions[i]) {
            controller.speed = motoboySpeed;
            controller.acelleration = -motoboyAcceleration;
        } else {
            controller.speed = -motoboySpeed;
            controller.acelleration = +motoboyAcceleration;
        }

        controller.targetPositionX = respectiveTargetPositions[i];
        controller.isGoing = true;
        controller.isDelivering = false;
        controller.spawner = gameObject;
    }
}
