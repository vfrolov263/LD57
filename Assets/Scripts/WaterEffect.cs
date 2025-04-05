using UnityEngine;
using UnityEngine.UI;

public class WaterEffect : MonoBehaviour
{
    [SerializeField, Tooltip("Object with water mask")]
    GameObject _waterMask;
    [SerializeField, Tooltip("Object with drops animation")]
    GameObject _drops;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Air")
        {
            _waterMask.SetActive(false);
            _drops.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Air")
        {
            _waterMask.SetActive(true);
            _drops.SetActive(false);
        }
    }
}
