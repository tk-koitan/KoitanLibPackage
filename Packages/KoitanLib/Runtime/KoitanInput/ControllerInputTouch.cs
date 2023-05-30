using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    public class ControllerInputTouch : ControllerInput
    {
        [SerializeField]
        GameObject bigCircle, smallCircle;
        Vector3 touchPos, startPos;
        Vector2 input;
        [SerializeField]
        float radius = 1f;


        private void Awake()
        {
            controllerName = "TouchPad";
            KoitanInput.JoinController(this);
        }

        public override void UpdateInput()
        {
            touchPos = Input.mousePosition;
            touchPos.z = 10f;
            touchPos = Camera.main.ScreenToWorldPoint(touchPos);
            if (Input.GetMouseButtonDown(0))
            {
                startPos = touchPos;
                bigCircle.SetActive(true);
                smallCircle.SetActive(true);
                bigCircle.transform.position = startPos;
            }
            if (Input.GetMouseButton(0))
            {
                input = touchPos - startPos;
                if (input.magnitude > radius)
                {
                    input = input.normalized;
                }
                else
                {
                    input /= radius;
                }
                smallCircle.transform.position = startPos + (Vector3)input * radius;
            }
            if (Input.GetMouseButtonUp(0))
            {
                bigCircle.SetActive(false);
                smallCircle.SetActive(false);
                input = Vector2.zero;
            }
            stick[StickCode.LeftStick] = input;
        }
    }
}
