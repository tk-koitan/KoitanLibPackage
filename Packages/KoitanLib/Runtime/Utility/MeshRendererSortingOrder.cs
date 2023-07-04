using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoitanLib
{
    [RequireComponent(typeof(MeshRenderer))]
    public class MeshRendererSortingOrder : MonoBehaviour
    {
        [SerializeField]
        int sortingOrder;

        private void OnValidate()
        {
            GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        }
    }
}
