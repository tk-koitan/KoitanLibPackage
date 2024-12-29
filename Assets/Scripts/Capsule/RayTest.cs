using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    Vector3 mousePos;
    [SerializeField]
    float radius = 1f;
    float distance = 1f;
    Vector3 hitPoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        mousePos.z = 10f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit hit;
        Vector3 direction = (mousePos - transform.position).normalized;
        distance = (mousePos - transform.position).magnitude;
        Ray ray = new Ray(transform.position, direction);
        if (Physics.SphereCast(ray, radius, out hit, distance))
        {
            hitPoint = transform.position + direction * hit.distance;
        }
        else
        {
            hitPoint = transform.position + direction * distance;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawLine(transform.position, hitPoint);
        Gizmos.DrawWireSphere(hitPoint, radius);
    }
}
