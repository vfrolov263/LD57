using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField, Tooltip("End screen")]
    private GameObject _endScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Exit" && Detection.GetInstance().AllTargetCollected())
        {
            _endScreen.SetActive(true);
            transform.parent.GetComponent<Movement>().enabled = false;
        }
    }
}
