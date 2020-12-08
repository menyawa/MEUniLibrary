using DG.Tweening;
using UnityEngine;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// 複数のDOTweenのアニメーションからなるシークエンスを使うクラスの基底クラス
    /// </summary>
    public abstract class DOTweenAnimationSequenceBase : MonoBehaviour {
        protected Sequence sequence_;
        //アニメーションごとに必要な時間
        [SerializeField] protected float duration_;

        protected void initSequence() {
            sequence_ = DOTween.Sequence();
        }

        /// <summary>
        /// 具体的な実装は子クラスに回す
        /// シーケンスの初期化とアニメーションの追加は同じメソッド内で行わないとシーケンスが死んで動作がおかしくなるので注意
        /// </summary>
        public virtual void play() {
            sequence_.Play();
        }

        /// <summary>
        /// シークエンスにかかる時間を取得
        /// このシークエンスが終わった後に次のシークエンスを行う，などといった処理をする際これとコルーチンを用いて制御する
        /// </summary>
        /// <returns></returns>
        public float getDurationForSequence() {
            return sequence_.Duration();
        }

        /// <summary>
        /// シーケンスが存在しているかどうか
        /// </summary>
        /// <returns></returns>
        public bool sequenceExists() {
            return sequence_ != null;
        }

        /// <summary>
        /// シーケンスを完了させる
        /// </summary>
        public void completeSequence() {
            sequence_.Complete();
        }
    }
}