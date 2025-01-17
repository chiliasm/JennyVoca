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
    }

    [System.Serializable]
    public class VocaInfo
    {
        public string En;
        public string Kr;
    }
}
