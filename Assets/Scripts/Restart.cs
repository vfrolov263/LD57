using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    [SerializeField, Tooltip("Click input")]
    private InputAction _pressAction;

    private void Awake()
    {
        _pressAction.Enable();
        _pressAction.performed += _ => Click();
    }

    void OnDestroy()
    {
        _pressAction.performed -= _ => Click();
        _pressAction.Disable();
    }

    private void Click()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}