using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    [System.Serializable]
    public class VocaContainer
    {
        public List<VocaData> DataList = new();
    }

    [System.Serializable]
    public class VocaData
    {
        public string OrderName;
        public List<VocaInfo> InfoList = new();

        public VocaData(string orderName)
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
