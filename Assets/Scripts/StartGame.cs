using UnityEngine;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    [SerializeField, Tooltip("Player Object")]
    private GameObject _player;
    [SerializeField, Tooltip("Click input")]
    private InputAction _pressAction;

    private void Start()
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
        _player.GetComponent<Movement>().enabled = true;
        Destroy(gameObject);
    }
}
