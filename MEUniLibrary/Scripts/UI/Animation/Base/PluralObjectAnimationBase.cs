using UnityEngine;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// 複数のオブジェクトを操作するアニメーションの親クラス
    /// </summary>
    public abstract class PluralObjectAnimationBase : DOTweenAnimationSequenceBase {
        //開始をこの時間分遅延させる
        [SerializeField] protected float firstDelay_;

        //順番に表示するか、全て同時に表示するかのタイプ
        //TODO:名前の変更
        protected enum TYPE {
            IN_ORDER,
            SAME_TIME
        }
        [SerializeField] protected TYPE type_;

        //アニメーションの順番を最初からやるか、逆順でやるか
        public enum ORDER_TYPE {
            IN_ORDER,
            REVERSE_ORDER
        }
        [SerializeField] protected ORDER_TYPE orderType_;

        /// <summary>
        /// タイプをもう一方に変更する
        /// </summary>
        public void switchType() {
            type_ = type_ == TYPE.IN_ORDER ? TYPE.SAME_TIME : TYPE.IN_ORDER;
        }

        /// <summary>
        /// 順番のタイプをもう一方に変更する
        /// </summary>
        public void switchOrderType() {
            orderType_ = orderType_ == ORDER_TYPE.IN_ORDER ? ORDER_TYPE.REVERSE_ORDER : ORDER_TYPE.IN_ORDER;
        }

        /// <summary>
        /// 順番のタイプをセットする
        /// </summary>
        /// <param name="type"></param>
        public void setOrderType(ORDER_TYPE type) {
            orderType_ = type;
        }
    }
}
