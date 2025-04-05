using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    [SerializeField, Tooltip("Player Object")]
    private GameObject _player;
    [SerializeField, Tooltip("Click input")]
    private InputAction _pressAction;

    private void Awake()
    {
        _pressAction.Enable();
        _pressAction.performed += _ => Click();   
    }

    private void Click()
    {
        _player.GetComponent<Movement>().enabled = true;
        gameObject.SetActive(false);
    }
}
