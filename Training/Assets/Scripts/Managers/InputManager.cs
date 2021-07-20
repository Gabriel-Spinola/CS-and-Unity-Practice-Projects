using UnityEngine;
using UnityEngine.SceneManagement;

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

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.RightAlt))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }
}
