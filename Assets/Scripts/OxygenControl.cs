using System.Collections;
using UnityEngine;

public class OxygenControl : MonoBehaviour
{
    [SerializeField, Tooltip("Maximum oxygen scores: 8 or less")]
    private int _maxOxygenScores = 8;
    [SerializeField, Tooltip("Seconds per one score")]
    private float _oxygenScoreTime = 1f;
    [SerializeField, Tooltip("Oxygen panel object")]
    private GameObject _oxygenPanel;
    [SerializeField, Tooltip("Restart screen")]
    private GameObject _restartScreen;
    private int _oxygenScores;
    private Coroutine _useOxygen;

    private void Start()
    {
        int oxygenImgs = _oxygenPanel.transform.childCount;

        for (int i = _maxOxygenScores; i < oxygenImgs; i++)
            _oxygenPanel.transform.GetChild(i).gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Air")
        {
            if (_useOxygen != null) StopCoroutine(_useOxygen);
            _oxygenScores = _maxOxygenScores;
            
            for (int i = 0; i < _maxOxygenScores; i++)
                _oxygenPanel.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Air")
            _useOxygen = StartCoroutine(UseOxygen());
    }

    private IEnumerator UseOxygen()
    {
        while (_oxygenScores > 0)
        {
            _oxygenScores--;
            _oxygenPanel.transform.GetChild(_oxygenScores).gameObject.SetActive(false);
            yield return new WaitForSeconds(_oxygenScoreTime);
        }

        transform.parent.GetComponent<Movement>().enabled = false;
        _restartScreen.SetActive(true);
    }
}