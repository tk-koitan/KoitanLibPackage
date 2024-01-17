using KoitanLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvexHull : MonoBehaviour
{
    [SerializeField]
    Vector2 a, b;
    [SerializeField]
    float atan2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        GizmosExtensions2D.DrawArrow2D(Vector2.zero, a);
        Gizmos.color = Color.blue;
        GizmosExtensions2D.DrawArrow2D(Vector2.zero, b);
        atan2 = Mathf.Atan2(a.y, a.x);
    }
}
