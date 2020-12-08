using UnityEngine;

namespace MEUniLibrary.Moving {
    /// <summary>
    /// 目標オブジェクトの手前までオブジェクトを動かすクラス
    /// </summary>
    public class MoveObjectInFrontOfTargetObject : MonoBehaviour {
        [SerializeField] private Transform targetObjectTransform_;
        //目標オブジェクトとの距離をどのくらい取るか
        [SerializeField] private float distanceToTargetObject_;
        [SerializeField] private float speed_;

        //どの軸を基準に距離を取るか
        private enum AXIS {
            X,
            Y,
            Z
        }
        [SerializeField] private AXIS axis_;
        //プラス・マイナスどちらの方向に距離を取るか
        private enum DISTANCE_VEC {
            PLUS,
            MINUS
        }
        [SerializeField] private DISTANCE_VEC distanceVec_;

        private Vector3 initialPosition_;
        //動作中かどうか
        public bool moving_ { private set; get; }

        private void Start() {
            initialPosition_ = transform.localPosition;
        }

        private void Update() {
            if (moving_) move();
        }

        /// <summary>
        /// 目標オブジェクトの手前までこのオブジェクトを動かす
        /// </summary>
        public void move() {
            var targetPosition = targetObjectTransform_.localPosition;
            //指定した軸で目標オブジェクトの指定したベクトルの方向に目標地点を置く
            //ローカル座標で取っているため、目標オブジェクトとこのオブジェクトが同じオブジェクトの子でないと座標が狂うので注意
            switch (axis_) {
                case AXIS.X:
                    targetPosition.x += distanceVec_ == DISTANCE_VEC.PLUS ? distanceToTargetObject_ : -distanceToTargetObject_;
                    break;
                case AXIS.Y:
                    targetPosition.y += distanceVec_ == DISTANCE_VEC.PLUS ? distanceToTargetObject_ : -distanceToTargetObject_;
                    break;
                case AXIS.Z:
                    targetPosition.z += distanceVec_ == DISTANCE_VEC.PLUS ? distanceToTargetObject_ : -distanceToTargetObject_;
                    break;
            }

            //フレーム間の時間も加味して最終的な移動スピードを算出する
            var speed = speed_ * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed);

            //到着したら動作フラグを折る
            if(transform.localPosition == targetPosition) moving_ = false;
        }

        /// <summary>
        /// 動作を開始する
        /// </summary>
        public void beginMoving() {
            moving_ = true;
        }

        /// <summary>
        /// 最初の位置にこのオブジェクトの位置を修正する
        /// </summary>
        public void resetPos() {
            transform.localPosition = initialPosition_;
        }
    }
}