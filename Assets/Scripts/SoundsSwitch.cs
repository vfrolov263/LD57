using UnityEngine;

public class SoundsSwitch : MonoBehaviour
{
    [SerializeField]
    private AudioSource _inhale, _diving, _underWater;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Air")
        {
            _diving.Stop();
            _underWater.Stop();
            _inhale.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Air")
        {
            _inhale.Stop();
            _diving.Play();
            _underWater.Play();
        }
    }
}