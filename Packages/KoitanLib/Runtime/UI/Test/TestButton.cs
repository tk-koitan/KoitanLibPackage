using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KoitanLib
{
    public class TestButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        public Action onClickCallBack;

        // �^�b�v  
        public void OnPointerClick(PointerEventData eventData)
        {
            onClickCallBack?.Invoke();
        }
        // �^�b�v�_�E��  
        public void OnPointerDown(PointerEventData eventData) { }
        // �^�b�v�A�b�v  
        public void OnPointerUp(PointerEventData eventData) { }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}


