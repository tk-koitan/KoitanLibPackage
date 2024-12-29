using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoitanCapsule : MonoBehaviour
{
    [SerializeField]
    MeshRenderer upperHalfSphere, lowerHalfSphere, cylinder;
    [SerializeField]
    float radius = 1f;
    [SerializeField]
    float cylinderLength = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnValidate()
    {
        upperHalfSphere.transform.localPosition = new Vector3(0, cylinderLength, 0);
        Vector3 scale = Vector3.one * radius;
        lowerHalfSphere.transform.localScale = scale;
        upperHalfSphere.transform.localScale = scale;
        scale.y = cylinderLength;
        cylinder.transform.localScale = scale;
    }

    public void DrawCapsule(Vector3 point1,Vector3 point2)
    {

    }
}
