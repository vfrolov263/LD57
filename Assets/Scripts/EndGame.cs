using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField, Tooltip("End screen")]
    private GameObject _endScreen;
    [SerializeField, Tooltip("End sound")]
    private AudioSource _endSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Exit" && Detection.GetInstance().AllTargetCollected())
        {
            _endScreen.SetActive(true);
            _endSound.Play();
            transform.parent.GetComponent<Movement>().enabled = false;
        }
    }
}
