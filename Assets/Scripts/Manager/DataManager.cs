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

        public void SaveVocaData()
        {
            if (mVocaContainer != null)
            {
                var vocaPath = GetVocaFilePath();
                var json = Object2Json(mVocaContainer);

                Save(vocaPath, json);
            }
        }

        public bool AddVocaData(VocaData data)
        {
            if (mVocaContainer == null || data == null)
                return false;

            foreach (var it in mVocaContainer.DataList)
            {
                if (it.OrderName == data.OrderName)
                    return false;
            }
            mVocaContainer.DataList.Add(data);
            return true;
        }

        public bool RemoveVocaData(VocaData data)
        {
            if (mVocaContainer == null || data == null)
                return false;

            int removeIdx = -1;
            for (int i=0; i<mVocaContainer.DataList.Count; i++)
            {
                if (mVocaContainer.DataList[i].OrderName == data.OrderName)
                {
                    removeIdx = i;
                    break;
                }
            }
            if (removeIdx < 0 || removeIdx >= mVocaContainer.DataList.Count)
                return false;

            mVocaContainer.DataList.RemoveAt(removeIdx);
            return true;
        }

        public List<VocaInfo> GetVocaInfoList(string orderName)
        {
            if (mVocaContainer == null || string.IsNullOrWhiteSpace(orderName))
                return null;

            foreach (var it in mVocaContainer.DataList)
            {
                if (it.OrderName == orderName)
                    return it.InfoList;
            }
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
            return string.Format("{0}/{1}.json", Application.streamingAssetsPath, VOCA_DATA_FILENAME);
        }
        #endregion
    }
}
