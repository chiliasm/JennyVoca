using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    [System.Serializable]
    public class VocaContainer
    {
        public List<VocaOrder> DataList = new();
    }

    [System.Serializable]
    public class VocaOrder
    {
        public string OrderName;
        public List<VocaInfo> InfoList = new();

        public VocaOrder(string orderName)
        {
            OrderName = orderName;
        }
    }

    [System.Serializable]
    public class VocaInfo
    {
        public string En;
        public string Kr;

        public VocaInfo(string en, string kr)
        {
            En = en;
            Kr = kr;
        }
    }
}
