using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace MEUniLibrary.UI.Animation {
    public class BoundSelectingUI : DOTweenAnimationSequenceBase, IPointerEnterHandler, IPointerExitHandler {
        //最初に動きを初期化する必要があるかどうか
        [SerializeField] private bool needInitAnimationInStart_;

        //バウンドできるかどうか
        //フラグは個別管理にしないと、「カテゴリボタンをクリックして出してる間にマウスアウトしたらそのまま動き続けた」なんてことが起きる
        private bool canBound_;

        private void Start() {
            if (needInitAnimationInStart_) resetMoving(true);
        }

        /// <summary>
        /// シーケンスの動きをセットし直す
        /// </summary>
        /// <param name="canBound"></param>
        public void resetMoving(bool canBound) {
            //この位置でフラグを更新しないと、バウンドの途中で座標が決まり少しスライド後の座標がズレてしまう可能性があるので注意
            canBound_ = canBound;
            //配列に格納された点を移動するパスの性質上、点が１つでも配列でないといけない
            //ループするのではなく、座標を明確に決めてバウンドさせる(そうでないと不自然な場所を初期位置として認識してしまうため)
            Vector3[] targetPos = { transform.localPosition, transform.localPosition };
            //今の位置を起点として、上に行く→元の位置に戻るを繰り返すため、１つ目の点の位置を操作する
            targetPos[0].y += 15f;
            initSequence();

            //選択時に無限ループで飛び跳ねさせる
            sequence_.Append(transform.DOLocalPath(targetPos, duration_, PathType.Linear));
            sequence_.OnComplete(() => sequence_.Restart());
            sequence_.Pause();
        }

        /// <summary>
        /// UIにカーソルが乗った際、バウンドを開始する
        /// </summary>
        /// <param name="pointerEventData"></param>
        public void OnPointerEnter(PointerEventData pointerEventData) {
            if (canBound_) {
                play();
            }
        }

        /// <summary>
        /// UIからカーソルが外れた際、バウンドを止める
        /// </summary>
        /// <param name="pointerEventData"></param>
        public void OnPointerExit(PointerEventData pointerEventData) {
            if (canBound_) {
                stopBound();
            }
        }

        /// <summary>
        /// バウンドを止め、初期位置に戻す
        /// </summary>
        public void stopBound() {
            sequence_.Restart();
            sequence_.Pause();
        }
    }
}
