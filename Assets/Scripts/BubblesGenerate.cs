using System.Collections;
using UnityEngine;

public class BubblesGenerate : MonoBehaviour
{
    [SerializeField, Tooltip("Bubble prefab")]
    private GameObject _bubble;
    [SerializeField] 
    private float _secondsBetweenBubbles = 1f;

    private void OnEnable()
    {
        StartCoroutine(CreateBubble());
    }

    private void OnDisable()
    {
        StopCoroutine(CreateBubble());
    }

    private IEnumerator CreateBubble()
    {
        while (true)
        {
            Instantiate(_bubble, transform);
            RectTransform bubbleRect = _bubble.GetComponent<RectTransform>();
            float maxOffset = Screen.width / 2f;
            bubbleRect.anchoredPosition = new Vector2(Random.Range(-maxOffset, maxOffset), 0f);
            yield return new WaitForSeconds(_secondsBetweenBubbles);
        }
    }
}