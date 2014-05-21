using UnityEngine;
using System.Collections;

public class Pipeset : MonoBehaviour {
    public float SpawnRangeMax = 2.8f;
    public float SpawnRangeMin = -2.6f;
    public Vector2 Speed = new Vector2(-7, 0);

    private Player _player;

	void Start () {
        _player = (GameObject.Find("Player")).GetComponent<Player>();
        // Add random heights
        transform.position = new Vector3(
            transform.position.x,
            Random.Range(SpawnRangeMin, SpawnRangeMax),
            transform.position.z
        );
        // Make pipes move
        rigidbody2D.velocity = Speed;
	}
	

	void Update () {
        if (_player == null || _player.IsDead)
            rigidbody2D.velocity = Vector2.zero;
	}

}
