using UnityEngine;
using UnityEngine.InputSystem;

public class Exit : MonoBehaviour
{
    [SerializeField, Tooltip("Exit game button")]
    private InputAction _exitButton;

    private void Awake()
    {
        _exitButton.Enable();
        _exitButton.performed += _ => ExitGame();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
