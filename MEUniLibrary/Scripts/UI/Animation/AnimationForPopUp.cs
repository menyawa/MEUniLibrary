using UnityEngine;
using DG.Tweening;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// ポップアップ・縮小(ポップダウン？)のアニメーションをまとめたクラス
    /// </summary>
    public class AnimationForPopUp : MonoBehaviour {
        [SerializeField] private float duration_;
        //ポップアップしたあと、最小化する必要があるかどうか
        [SerializeField] private bool needMinimalize_;

        /// <summary>
        /// 引数で渡されたSequenceの一番最後にポップアップするアニメーションを追加
        /// フラグによっては、一定時間表示した後フェードアウト(縮小)する
        /// </summary>
        /// <param name="sequence"></param>
        public void appendPopUp(Sequence sequence) {
            Vector3 maximizeSize = new Vector3(1.0f, 1.0f, 1.0f);
            sequence.Append(transform.DOScale(maximizeSize, duration_).SetEase(Ease.OutElastic));
            if (needMinimalize_) {
                const float WAITING_TIME = 1.0f;
                sequence.AppendInterval(WAITING_TIME);
                appendClose(sequence);
            }
        }

        /// <summary>
        /// 引数で渡されたSequenceの一番最後に渡された時間でポップアップするアニメーションを追加
        /// フラグによっては、一定時間表示した後フェードアウト(縮小)する
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="animationTime"></param>
        public void appendPopUp(Sequence sequence, float animationTime) {
            Vector3 maximizeSize = new Vector3(1.0f, 1.0f, 1.0f);
            sequence.Append(transform.DOScale(maximizeSize, animationTime).SetEase(Ease.OutElastic));
            if (needMinimalize_) {
                const float WAITING_TIME = 1.0f;
                sequence.AppendInterval(WAITING_TIME);
                appendClose(sequence, animationTime);
            }
        }

        /// <summary>
        /// 引数で渡されたSequenceの一番最後にポップアップするアニメーションを同時に実行する形で追加
        /// フラグによっては、一定時間表示した後フェードアウト(縮小)する
        /// </summary>
        /// <param name="sequence"></param>
        public void joinPopUp(Sequence sequence) {
            Vector3 maximizeSize = new Vector3(1.0f, 1.0f, 1.0f);
            sequence.Join(transform.DOScale(maximizeSize, duration_).SetEase(Ease.OutElastic));
            if (needMinimalize_) {
                const float WAITING_TIME = 1.0f;
                sequence.AppendInterval(WAITING_TIME);
                appendClose(sequence);
            }
        }

        /// <summary>
        /// 引数で渡されたSequenceの一番最後に渡された時間でポップアップするアニメーションを同時に実行する形で追加
        /// フラグによっては、一定時間表示した後フェードアウト(縮小)する
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="animationTime"></param>
        public void joinPopUp(Sequence sequence, float animationTime) {
            Vector3 maximizeSize = new Vector3(1.0f, 1.0f, 1.0f);
            sequence.Join(transform.DOScale(maximizeSize, animationTime).SetEase(Ease.OutElastic));
            if (needMinimalize_) {
                const float WAITING_TIME = 1.0f;
                sequence.AppendInterval(WAITING_TIME);
                appendClose(sequence, animationTime);
            }
        }

        /// <summary>
        /// 渡されたSequenceの最後にフェードアウト(縮小)するアニメーションを追加する
        /// </summary>
        /// <param name="sequence"></param>
        public void appendClose(Sequence sequence) {
            Vector3 minimizeSize = new Vector3(0.0f, 0.0f, 0.0f);
            sequence.Append(transform.DOScale(minimizeSize, duration_).SetEase(Ease.OutElastic));
        }

        /// <summary>
        /// 渡されたSequenceの最後に渡された時間でフェードアウト(縮小)するアニメーションを追加する
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="animationTime"></param>
        public void appendClose(Sequence sequence, float animationTime) {
            Vector3 minimizeSize = new Vector3(0.0f, 0.0f, 0.0f);
            sequence.Append(transform.DOScale(minimizeSize, animationTime).SetEase(Ease.InOutElastic));
        }

        /// <summary>
        /// 渡されたSequenceの最後にフェードアウト(縮小)するアニメーションを同時に実行する形で追加する
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="animationTime"></param>
        public void joinClose(Sequence sequence) {
            var minimizeSize = new Vector3(0.0f, 0.0f, 0.0f);
            sequence.Join(transform.DOScale(minimizeSize, duration_).SetEase(Ease.InOutElastic));
        }

        /// <summary>
        /// 渡されたSequenceの最後に渡された時間でフェードアウト(縮小)するアニメーションを同時に実行する形で追加する
        /// </summary>
        /// <param name="sequence"></param>
        /// <param name="animationTime"></param>
        public void joinClose(Sequence sequence, float animationTime) {
            var minimizeSize = new Vector3(0.0f, 0.0f, 0.0f);
            sequence.Join(transform.DOScale(minimizeSize, animationTime).SetEase(Ease.InOutElastic));
        }
    }
}
