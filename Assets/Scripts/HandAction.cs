using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandAction : MonoBehaviour
{
    [SerializeField, Tooltip("Hand cursor image")] 
    private GameObject _cursor;
    [SerializeField, Tooltip("Pushing force")] 
    private float _force = 100f;
    [SerializeField, Tooltip("Pushing distance")] 
    private float _pushDistance = 1.8f;
    [SerializeField, Tooltip("Numer of coins on the level")]
    private int _coinsNumber = 5;
    [SerializeField, Tooltip("Text for scores output")]
    private TMP_Text _scoresText;
    [SerializeField, Tooltip("Audio of collect item")]
    private AudioSource _itemCollect;
    [SerializeField, Tooltip("Click input")]
    private InputAction _pressAction;
    private GameObject _target;
    private int _coins;

    private void Awake()
    {
        _pressAction.Enable();
        _pressAction.performed += _ => Click();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _coins = 0;
    }

    private void Click()
    {
        if (_target == null)
            return;

        if (_target.tag == "Physical")
            _target.GetComponent<Rigidbody>().AddForce(transform.forward * _force);
        else
        {
            _coins++;

            if (_coins < _coinsNumber)
                _scoresText.text = $"{_coins}/{_coinsNumber}";
            else
                _scoresText.text = "Nice! Go to the boat.";

            Detection.GetInstance().RemoveTarget(_target.transform);
            Destroy(_target);
            _target = null;
            _itemCollect.Play();
        }
    }

    private void FixedUpdate()
    {
        if (_cursor.activeSelf)
            _cursor.SetActive(false);

        _target = null;

        RaycastHit raycastHit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out raycastHit, _pushDistance))
        {
            if (raycastHit.transform == null)
                return;
            
            GameObject target = raycastHit.transform.gameObject;

            if (target.tag == "Physical" || target.tag == "Coin")
            {
                if (!_cursor.activeSelf)
                    _cursor.SetActive(true);

                _target = target;

                if (target.tag == "Coin")
                    target.GetComponent<Outline>().enabled = true;
            }
        }
    }    
}
