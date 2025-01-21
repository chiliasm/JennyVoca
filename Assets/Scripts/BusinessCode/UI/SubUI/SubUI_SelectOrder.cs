using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class SubUI_SelectOrder : SubUI
    {
        public class SelectOrderScrollItemData : BaseScrollItemData
        {
            public string Name;
            public int Count;

            public SelectOrderScrollItemData(string name, int count)
            {
                Name = name;
                Count = count;
            }
        }
    }
}
