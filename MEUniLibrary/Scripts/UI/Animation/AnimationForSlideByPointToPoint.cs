using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// ２点間の今いる点からもう一方にスライドさせるクラス
    /// </summary>
    public class AnimationForSlideByPointToPoint : PluralObjectAnimationBase {
        //選択したオブジェクトのみ引っ込めることもあるため、全てまとめた親オブジェクトを移動させるのではなく、個別に移動を管理する必要がある。
        //なので配列で管理
        [SerializeField] private RectTransform[] rectTransforms_;
        //動く際のx, yの方向
        [SerializeField] private Vector3 movementVec_;
        //移動の際の距離
        [SerializeField] private float length_;
        //開始をこの時間分遅延させる
        [SerializeField] private float firstDelay_;
        //最初に位置を初期化した後、オブジェクトを動かす必要があるかどうか
        [SerializeField] private bool needDisplayInAwake_;

        //隠す際(引っ込める際)・出す際(画面に映す際)の位置のリスト
        private Vector3[] firstPositionList_;
        private Vector3[] secondPositionList_;

        private void Awake() {
            initPositionList();
            //UIによっては最初にsecondPosへの移動を行うことがある
            if (needDisplayInAwake_) slide(false);
        }

        /// <summary>
        /// 隠す際の位置、出す際の位置を初期化する
        /// </summary>
        private void initPositionList() {
            firstPositionList_ = new Vector3[rectTransforms_.Length];
            secondPositionList_ = new Vector3[rectTransforms_.Length];

            //UIの出し入れに使う場合初期位置→隠す位置になるため、初めからUIを隠す位置に置いておく必要があることに注意
            for (int index = 0; index < rectTransforms_.Length; index++) {
                firstPositionList_[index] = rectTransforms_[index].localPosition;
                secondPositionList_[index] = firstPositionList_[index] + movementVec_ * length_;
            }
        }

        /// <summary>
        /// オブジェクトを全てスライドする
        /// </summary>
        /// <param name="isDisplayed"></param>
        public void slide(bool isDisplayed) {
            slide(isDisplayed, Enumerable.Range(0, rectTransforms_.Length).ToList()); //配列の要素を全て選択して動作を委託する
        }

        /// <summary>
        /// 選んだインデックスのオブジェクトのみスライドする
        /// </summary>
        /// <param name="isDisplayed"></param>
        /// <param name="selectIndexList"></param>
        public void slide(bool isDisplayed, List<int> selectIndexList) {
            initSequence();
            sequence_.AppendInterval(firstDelay_);

            //現在出ているかどうかのフラグを見て、隠す・出すどちらかのポジションを終点とする
            //反転する場合、背景パネルが最後にならないと不自然なため、順番を逆順で引っ込める
            if (isDisplayed) selectIndexList.Reverse();
            var endPosList = isDisplayed ? firstPositionList_ : secondPositionList_;

            //終点に向けて、選んだUIをスライドする
            foreach (int index in selectIndexList) {
                switch (type_) {
                    case TYPE.IN_ORDER:
                        sequence_.Append(rectTransforms_[index].DOLocalMove(endPosList[index], duration_).SetEase(Ease.InOutBack));
                        break;
                    case TYPE.SAME_TIME:
                        sequence_.Join(rectTransforms_[index].DOLocalMove(endPosList[index], duration_).SetEase(Ease.InOutBack));
                        break;
                }
            }
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
    }
}