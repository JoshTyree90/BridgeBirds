using Leap;
using UnityEngine;
using System.Collections;

public class LeapInput : MonoBehaviour {
    #region "Config Vars"
    public bool DrawDebug = true;
    #endregion

    private Controller _leapController;
    private Frame _frame = Frame.Invalid;
    private Vector3 _palmPosition = Vector3.zero;

    public bool IsValid {
        get { return _frame.IsValid && _frame.Hands.Count > 0; }
    }

    public Vector3 PalmPosition { get { return _palmPosition; } }

    #region "Unity callbacks"
    void Start () {
        _leapController = new Controller();
	}
	
	
	void Update () {
        if (_leapController.IsConnected) {
            _frame = _leapController.Frame();
            if (_frame.IsValid) {
                if (_frame.Hands.Count > 0) {
                    Hand hand = _frame.Hands[0];
                    if (hand.IsValid) {
                        _palmPosition = hand.PalmPosition.ToUnityScaled();
                    }
                }
            }
        }
    }

    void OnGUI() {
        if (DrawDebug) {
            GUI.Label(new Rect(400, 10, 200, 30), "Connected?: " + _leapController.IsConnected);
            GUI.Label(new Rect(400, 40, 200, 30), "Frame Valid?: " + _frame.IsValid);
            GUI.Label(new Rect(400, 70, 200, 30), "Has Hand?: " + (_frame.Hands.Count > 0));
            GUI.Label(new Rect(400, 100, 200, 30), "Palm: " + PalmPosition.ToString());
        }
    }
    #endregion
}
