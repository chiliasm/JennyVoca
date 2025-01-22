using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace Jenny
{
    public class DataManager : BaseMonoSingleton<DataManager>
    {
        #region // [Var] Data //
        VocaContainer mVocaContainer = new();

        const string VOCA_DATA_FILENAME = "VocaData";
        #endregion


        #region // [Func] Singleton //
        public override void OnCreateSingleton() 
        { 
            base.OnCreateSingleton();

            LoadAll();
        }
        #endregion

        #region // [Func] Init //
        void LoadAll()
        {
            var vocaPath = GetVocaFilePath();
            if (File.Exists(vocaPath))
                mVocaContainer = Load<VocaContainer>(vocaPath);
        }
        #endregion

        #region // [Func] Voca //
        public VocaContainer GetVocaContainer()
        {
            return mVocaContainer;
        }

        public VocaOrder GetVocaOrder(string orderName)
        {
            if (string.IsNullOrWhiteSpace(orderName))
                return null;
            foreach (var it in mVocaContainer.DataList)
            {
                if (it.OrderName == orderName)
                    return it;
            }
            return null;
        }

        public void SaveVocaData()
        {
            if (mVocaContainer != null)
            {
                var vocaPath = GetVocaFilePath();
                var json = Object2Json(mVocaContainer);

                Save(vocaPath, json);
            }
        }

        public void AddVocaOrder(VocaOrder data)
        {
            var order = GetVocaOrder(data.OrderName);
            if (order != null)
                RemoveVocaOrder(order);

            mVocaContainer.DataList.Add(data);
        }

        public bool RemoveVocaOrder(VocaOrder order)
        {
            return mVocaContainer.DataList.Remove(order);
        }

        public bool RemoveVocaOrder(string orderName)
        {
            var order = GetVocaOrder(orderName);
            if (order != null)
                return RemoveVocaOrder(order);
            return false;
        }

        public List<VocaInfo> GetVocaInfoList(string orderName)
        {
            var order = GetVocaOrder(orderName);
            if (order != null)
                return order.InfoList;
            return null;
        }
        #endregion

        #region // [Func] Load, Save //
        T Load<T>(string filePath)
        {
            FileStream fileStream = new(filePath, FileMode.Open);
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();

            var json = Encoding.UTF8.GetString(data);
            return Json2Object<T>(json);
        }

        void Save(string filePath, string json)
        {
            FileStream fileStream = new(filePath, FileMode.Create);
            var data = Encoding.UTF8.GetBytes(json);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }
        #endregion

        #region // [Func] Convert //
        string Object2Json(object o)
        {
            return JsonUtility.ToJson(o);
        }

        T Json2Object<T>(string json)
        {
            return JsonUtility.FromJson<T>(json);
        }
        #endregion

        #region // [Func] FilePath //
        string GetVocaFilePath()
        {
            return string.Format("{0}/{1}.json", Application.persistentDataPath, VOCA_DATA_FILENAME);
        }
        #endregion
    }
}
