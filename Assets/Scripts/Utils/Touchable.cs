using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Jenny
{
    public class Touchable : Text
    {
        public override bool raycastTarget { get => base.raycastTarget; set => base.raycastTarget = value; }

        protected override void OnEnable()
        {
            raycastTarget = true;
            base.OnEnable();
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(Touchable))]
    public class Touchable_Editor : Editor
    {
        public override void OnInspectorGUI()
        {
            //  do nothing.
        }
    }
#endif
}
