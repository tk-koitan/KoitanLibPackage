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

        // タップ  
        public void OnPointerClick(PointerEventData eventData)
        {
            onClickCallBack?.Invoke();
        }
        // タップダウン  
        public void OnPointerDown(PointerEventData eventData) { }
        // タップアップ  
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


