using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFromPitch : MonoBehaviour
{
    
    [SerializeField] private Vector3 minY;
    [SerializeField] private Vector3 maxY;
    [SerializeField] private WhistleDetector detector;

    [SerializeField] private float sensitivity;
    [SerializeField] private float threshold;
    
    void Update()
    {
        // Debug.Log(detector.pitchMap);
        transform.position = Vector3.Lerp(minY, maxY, detector.pitchMap);
    }
}
