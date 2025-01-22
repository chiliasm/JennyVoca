using System.Collections.Generic;
using UnityEngine;

namespace Jenny
{
    public class ResourceManager : BaseMonoSingleton<ResourceManager>
    {
        #region // [Var] Data //
        readonly Dictionary<string, Object> mCachingDic = new();
        #endregion


        #region // [Func] Load //
        public Object Load(string path)
        {
            if (mCachingDic.TryGetValue(path, out var o) == false)
            {
                o = Resources.Load(path);
                mCachingDic[path] = o;
            }
            return o;
        }
        #endregion

        #region // [Func] Unload //
        public void Unload(string path)
        {
            if (string.IsNullOrEmpty(path))
                return;

            if (mCachingDic.TryGetValue(path, out var o))
                Resources.UnloadAsset(o);
            mCachingDic.Remove(path);

            Resources.UnloadUnusedAssets();
        }

        public void UnloadAll()
        {
            foreach (var it in mCachingDic)
                Resources.UnloadAsset(it.Value);
            mCachingDic.Clear();

            Resources.UnloadUnusedAssets();
        }
        #endregion
    }
}
