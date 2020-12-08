using UnityEngine;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// 複数のオブジェクトを操作するアニメーションの親クラス
    /// </summary>
    public abstract class PluralObjectAnimationBase : DOTweenAnimationSequenceBase {
        //順番に表示するか、全て同時に表示するかのタイプ
        protected enum TYPE {
            IN_ORDER,
            SAME_TIME
        }
        [SerializeField] protected TYPE type_;

        /// <summary>
        /// タイプをもう一方に変更する
        /// </summary>
        public void switchType() {
            type_ = type_ == TYPE.IN_ORDER ? TYPE.SAME_TIME : TYPE.IN_ORDER;
        }
    }
}
