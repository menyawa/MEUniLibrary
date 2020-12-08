using UnityEngine;

namespace MEUniLibrary.Collisions {
    /// <summary>
    /// 指定したオブジェクトの指定された軸のサイズにコライダーを合わせるクラス
    /// </summary>
    public class ResizeBoxCollider2DToObjectSize : MonoBehaviour {
        //サイズの参考にするオブジェクトのRectTransformを取ることで、コライダーが貼り付いているオブジェクトのsizeを弄る必要がなくなる
        [SerializeField] private RectTransform referenceObjectRectTransform_;
        [SerializeField] private BoxCollider2D boxCollider2D_;
        //どの軸をリサイズするかのフラグ
        private enum MATCHING_AXIS {
            NONE,
            X,
            Y,
            ALL
        }
        [SerializeField] private MATCHING_AXIS matchingAxis_;

        public void Update() {
            if (isMissMatching()) resize();
        }

        /// <summary>
        /// コライダーのサイズをリサイズする
        /// </summary>
        public void resize() {
            var colliderSize = boxCollider2D_.size;
            var objectSize = referenceObjectRectTransform_.sizeDelta;
            switch (matchingAxis_) {
                case MATCHING_AXIS.X:
                    colliderSize.x = objectSize.x;
                    break;
                case MATCHING_AXIS.Y:
                    colliderSize.y = objectSize.y;
                    break;
                case MATCHING_AXIS.ALL:
                    colliderSize = objectSize;
                    break;
            }
            boxCollider2D_.size = colliderSize;
        }

        /// <summary>
        /// オブジェクトのwidth・height(スケールではないことに注意)とコライダーの大きさがあって「いないか」どうか
        /// </summary>
        /// <returns></returns>
        private bool isMissMatching() {
            var colliderSize = boxCollider2D_.size;
            var objectSize = referenceObjectRectTransform_.sizeDelta;
            switch (matchingAxis_) {
                case MATCHING_AXIS.NONE:
                    return true;
                case MATCHING_AXIS.X:
                    if (objectSize.x != colliderSize.x) return true;
                    break;
                case MATCHING_AXIS.Y:
                    if (objectSize.y != colliderSize.y) return true;
                    break;
                case MATCHING_AXIS.ALL:
                    if (objectSize != colliderSize) return true;
                    break;
            }

            return false;
        }
    }
}
