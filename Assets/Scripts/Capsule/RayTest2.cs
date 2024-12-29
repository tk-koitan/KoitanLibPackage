using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest2 : MonoBehaviour
{
    Vector3 mousePos;
    [SerializeField]
    float radius = 1f;
    float distance = 10f;
    Vector3 hitPoint;
    [SerializeField]
    Vector3 size;
    Color gizmosColor;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            radius += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            radius -= Time.deltaTime;
        }

        mousePos = Input.mousePosition;
        mousePos.z = 5f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        RaycastHit hit;
        Vector3 direction = Camera.main.transform.forward;
        Ray ray = new Ray(mousePos, direction);
        if (Physics.SphereCast(ray, radius, Mathf.Infinity))
        {
            gizmosColor = Color.red;
        }
        else
        {
            gizmosColor = Color.white;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireSphere(mousePos, Mathf.Abs(radius));
    }
}
