
using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// UIの開閉を司るボタンの親クラス
    /// </summary>
    public abstract class SwitchOpeningAndClosingOfUIButtonBase : ButtonBase {
        //開けるボタン、閉じるボタンどちらか
        protected enum TYPE {
            OPEN,
            CLOSE
        }
        [SerializeField] protected TYPE type_;
    }
}
