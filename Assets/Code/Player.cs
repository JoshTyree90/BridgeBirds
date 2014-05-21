using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    private int _score = 0;
    private bool _isDead = false;
    private LeapInput _input;
    public GameObject PointPrefab;

    public AudioSource SfxPoint;
    public AudioSource SfxDie;

    /// <summary>
    /// Player's score
    /// </summary>
    public int Score { get { return _score; } set { _score = value; } }

    /// <summary>
    /// Is player alive?
    /// </summary>
    public bool IsDead { get { return _isDead; } set { _isDead = value; } }


    #region "Unity Callbacks"
    void Start () {
        _input = GetComponent<LeapInput>();
	}
	

	void Update () {
        if (_input != null && !IsDead) {
            if (_input.IsValid) {
                rigidbody2D.velocity = Vector2.zero;
                float adjustedHeight = _input.PalmPosition.y - 2.6f - 5.0f;
                transform.position = new Vector3(transform.position.x, adjustedHeight);
            }
        }

        if (IsDead && Input.GetKeyUp("space"))
            Application.LoadLevel(Application.loadedLevel);
	}


    void OnGUI() {
        if (IsDead) {
            GUI.Label(new Rect(10, 10, 300, 30), "Game Over! Your final score was " + Score);
            GUI.Label(new Rect(10, 40, 300, 30), "Press \"SPACE\" to restart");
        } else 
            GUI.Label(new Rect(10, 10, 300, 30), "Score: " + Score.ToString());
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (!IsDead)
            Died();
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (!IsDead) { 
            Score++;
            if (SfxPoint != null) {
                ((AudioSource)Instantiate(SfxPoint)).Play();
            }
        }
    }
    #endregion

    private void Died() {
        IsDead = true;

        bool result = System.Convert.ToBoolean(Mathf.Round(Random.value));

        // Make bird go crazy
        rigidbody2D.angularVelocity = (result) ? 3000.0f : -3000.0f;

        if(SfxDie != null)
            ((AudioSource)Instantiate(SfxDie)).Play();
    }

}
