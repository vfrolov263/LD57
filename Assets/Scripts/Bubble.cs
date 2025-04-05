using UnityEngine;

public class Bubble : MonoBehaviour
{
    private void Start()
    {
        float animTime = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        Destroy(gameObject.transform.parent.gameObject, animTime);
    }
}
