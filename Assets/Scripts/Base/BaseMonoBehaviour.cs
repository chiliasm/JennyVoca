using UnityEngine;

namespace Jenny
{
    public class BaseMonoBehaviour : MonoBehaviour
    {
        #region // [Var] Unity //
        GameObject _go;
        protected GameObject Go
        {
            get
            {
                if (_go == null)
                    _go = gameObject;
                return _go;
            }
        }

        Transform _tr;
        protected Transform Tr
        {
            get
            {
                if (_tr == null)
                    _tr = transform;
                return _tr;
            }
        }
        #endregion


        #region // [Func] Unity //
        protected virtual void Start() { }

        protected virtual void OnEnable() { }

        protected virtual void OnDisable() { }

        protected virtual void Update() { }
        #endregion

        #region // [Func] Singleton //
        public virtual void OnCreateSingleton() { }
        #endregion

        #region // [Func] Util //
        protected bool ClearCoroutine(ref Coroutine c)
        {
            if (c != null)
            {
                StopCoroutine(c);
                c = null;
            }
            return isActiveAndEnabled;
        }
        #endregion
    }
}
