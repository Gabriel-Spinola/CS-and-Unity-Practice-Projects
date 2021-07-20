using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager _I;

    public float xAxis;

    public bool keyJumping;
    public bool keyJumpingHold;

    private void Awake()
    {
        if (_I != null) {
            Destroy(gameObject);
        }
        else {
            DontDestroyOnLoad(gameObject);
            _I = this;
        }
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        keyJumping = Input.GetKeyDown(KeyCode.Space);
        keyJumpingHold = Input.GetKey(KeyCode.Space);
    }
}
