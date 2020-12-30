using DG.Tweening;
using System;
using UnityEngine;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// UIを１つずつ順番に、あるいは全て同時にフェードイン・アウトするクラス
    /// CanvasGroupを使った方法では全てを同時にフェードするしかなくなってしまい、また全ての対象UIが同じオブジェクトを親としている保証もないので、Graphicの配列を参照していく方法で行う
    /// </summary>
    public class AnimationForFade : PluralObjectAnimationBase {
        [SerializeField] private UnityEngine.UI.Graphic[] graphics_;
        //透明度の終了値
        [SerializeField] private float endValue_;
        //開始と同時にアニメーションが必要かどうか
        [SerializeField] private bool needFadeInStart_;
        //アニメーション前後でアクティブ・非アクティブを切換える必要があるか
        [SerializeField] private bool needSwitchingActive_;

        //どんなときにスキップするかのタイプ
        private enum SKIP_TYPE {
            NONE,
            LEFT_CLICK,
            RIGHT_CLICK,
            MIDDLE_CLICK
        }
        [SerializeField] private SKIP_TYPE skipType_;

        private void Start() {
            if (needFadeInStart_) fade();
        }

        private void Update() {
            //左クリックでアニメーションをスキップする
            if (skipSequenceCommandIsEntered()) {
                completeSequence();
            }
        }

        /// <summary>
        /// UIをフェードイン・アウトする(事前設定された値に)
        /// </summary>
        public void fade() {
            fade(endValue_);
        }

        /// <summary>
        /// UIをフェードイン・アウトする(引数で指定された値に)
        /// </summary>
        /// <param name="endValue"></param>
        public void fade(float endValue) {
            //最初の要素をとりあえずサンプルとして今の状態を取る
            var active = graphics_[0].IsActive();
            //必要があるなら、アニメーションをする前にアクティブにする
            if (needSwitchingActive_) {
                if (active == false) {
                    foreach (var graphic in graphics_) {
                        graphic.gameObject.SetActive(true);
                    }
                }
            }

            initSequence();
            //開始の遅延も可能
            sequence_.AppendInterval(firstDelay_);

            //UIを順番か、同時に表示していく
            var graphics = graphics_;
            //逆順でアニメーションする場合は逆にする
            if (orderType_ == ORDER_TYPE.REVERSE_ORDER) Array.Reverse(graphics);
            foreach (var graphic in graphics) {
                switch (type_) {
                    case TYPE.IN_ORDER:
                        sequence_.Append(graphic.DOFade(endValue, duration_));
                        break;
                    case TYPE.SAME_TIME:
                        sequence_.Join(graphic.DOFade(endValue, duration_));
                        break;
                }
            }
            //アニメーションが終わったあと、必要があるなら非アクティブにする
            sequence_.OnComplete(() => {
                if (needSwitchingActive_) {
                    if (active) {
                        foreach (var graphic in graphics_) {
                            graphic.gameObject.SetActive(false);
                        }
                    }
                }
            });

            play();
        }

        /// <summary>
        /// 最初の遅延を設定
        /// インスペクタから設定できるようにするため(シリアライズ化するため)プロパティではなくセッターをメソッドとして作成
        /// まあプロパティ別に作れば良いんだけども…わかりやすいよねこの方が
        /// </summary>
        /// <param name="delay"></param>
        public void setFirstDelay(float delay) {
            firstDelay_ = delay;
        }

        /// <summary>
        /// シーケンスをスキップするボタンが入力されているかどうか
        /// </summary>
        /// <returns></returns>
        private bool skipSequenceCommandIsEntered() {
            switch (skipType_) {
                case SKIP_TYPE.NONE:
                    return false;
                case SKIP_TYPE.LEFT_CLICK:
                    return Input.GetMouseButton(0);
                case SKIP_TYPE.RIGHT_CLICK:
                    return Input.GetMouseButton(1);
                case SKIP_TYPE.MIDDLE_CLICK:
                    return Input.GetMouseButton(2);
                default:
                    return false;
            }
        }
    }
}