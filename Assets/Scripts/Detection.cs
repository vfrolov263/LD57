using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detection : MonoBehaviour
{
    [Header("Base detector settings")]
    [SerializeField, Tooltip("Target for detection")]
    private List<Transform> _targets;
    [SerializeField, Tooltip("Detector scale arrow")]
    private RectTransform _arrow;
    [SerializeField, Tooltip("Maximum work ditance")]
    private float _maxDetectDistance = 100f;

    [Header("Detector audio settings")]
    [SerializeField, Tooltip("Detector peep sound")]
    private AudioSource _detectorSound;
    [SerializeField, Tooltip("Minimum seconds between peeps")]
    private float _minSecondsBetweenPeeps = 0.5f;
    [SerializeField, Tooltip("Maximum seconds between peeps")]
    
    private float _maxSecondsBetweenPeeps = 5f;
    private float _maxPeepDistance;
    private float _secondsBetweenPeeps = -1;

    private static Detection m_instance;

    public static Detection GetInstance()
    {
        return m_instance;
    }

    public void RemoveTarget(in Transform target)
    {
        _targets.Remove(target);
    }

    public bool AllTargetCollected()
    {
        return _targets.Count == 0;
    }

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
    }

    private void Start()
    {
        _maxPeepDistance = _maxDetectDistance / 2f;
        StartCoroutine(Peep());
    }

    private void Update()
    {  
        if (transform.hasChanged)
        {
            float distance = GetMinTargetDistance();
            SetArrow(distance);
            SetPeepFrequency(distance);
        }
    }

    private float GetMinTargetDistance()
    {
        float distance, minDistance = 10000f;

        foreach (var target in _targets)
        {
            distance = Vector3.Distance(transform.position, target.position);
            
            if (minDistance > distance)
                minDistance = distance;
        }

        return minDistance;
    }

    private void SetArrow(float distance)
    {
        float angle = distance >= _maxDetectDistance ? 90f : (distance / _maxDetectDistance * 180f) - 90f;
        _arrow.eulerAngles = new Vector3(0f, 0f, angle);
    }

    private void SetPeepFrequency(float distance)
    {
        if (distance >= _maxPeepDistance)
            _secondsBetweenPeeps = -1;
        else
        {
            float delta = _maxSecondsBetweenPeeps - _minSecondsBetweenPeeps;
            _secondsBetweenPeeps = _minSecondsBetweenPeeps + (float)Math.Pow(distance / _maxPeepDistance, 2.0) * delta;

        }
    }

    private IEnumerator Peep()
    {
        while (true)
        {
            if (_secondsBetweenPeeps <= 0)
                yield return new WaitForSeconds(_maxSecondsBetweenPeeps);
            else
            {
                _detectorSound.Play();
                yield return new WaitForSeconds(_secondsBetweenPeeps);
            }
        }
    }
}