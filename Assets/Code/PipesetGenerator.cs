using UnityEngine;
using System.Collections;

public class PipesetGenerator : MonoBehaviour {
    private Player _player;

    public GameObject Prefab;
    public float SpawnRate = 1.5f;


	void Start () {
        _player = (GameObject.Find("Player")).GetComponent<Player>();
        InvokeRepeating("SpawnSet", 1f, SpawnRate);
	}

    void Update() {
        if (_player == null || _player.IsDead)
            CancelInvoke();
    }


    void SpawnSet() {
        Instantiate(Prefab);
    }
}
