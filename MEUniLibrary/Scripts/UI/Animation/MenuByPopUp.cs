using UnityEngine;
using DG.Tweening;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// ポップアップで出てくるメニューのクラス
    /// </summary>
    public class MenuByPopUp : DOTweenAnimationSequenceBase {
        [SerializeField] private Sprite[] menuSprites_;
        [SerializeField] private UnityEngine.UI.Image currentMenuImage_;

        private int index_;

        private void Start() {
            resetSprite();
        }

        /// <summary>
        /// メニューをスライドし、新しい前後のメニュー(カーソルを押した方向の)に切り替える
        /// 始点と終点が動くたびに変わるので、クラスの趣旨が違うと判断しSlidePointToPointUIは使わない
        /// </summary>
        /// <param name="callingMenuIsNext"></param>
        public void slide(bool callingMenuIsNext) {
            //右か左、どちらかカーソルが押された方の画面外に新メニューを生成
            //メニューの親オブジェクト(このオブジェクト)があるので、その子になるよう調整(背景パネルの上かつ、カーソルの下でないといけない)
            //メニューはメニューで一つ親オブジェクトを作ってそれだけでまとめておくことで、カーソル等のUIの順序を乱さない
            var position = callingMenuIsNext ? new Vector2(2000f, 0f) : new Vector2(-2000f, 0f);
            var newMenuImage = Instantiate(currentMenuImage_.gameObject, position, Quaternion.identity, transform).GetComponent<UnityEngine.UI.Image>();

            RectTransform currentMenuRectTransform = currentMenuImage_.transform as RectTransform;
            RectTransform newMenuRectTransform = newMenuImage.transform as RectTransform;
            newMenuRectTransform.localPosition = position; //もう一回代入しないと変な位置に生成される

            //呼ばれた前後のメニューの画像を取得
            //既にインデックスが端の場合、もう片方の端にする
            index_ = callingMenuIsNext ? index_ + 1 : index_ - 1;
            if (index_ >= menuSprites_.Length) index_ = 0;
            else if (index_ < 0) index_ = menuSprites_.Length - 1;
            newMenuImage.sprite = menuSprites_[index_];

            initSequence();
            var endPosX = 0.0f;
            //新しいメニューを中心にスライド
            sequence_.Append(newMenuRectTransform.DOLocalMoveX(endPosX, duration_).SetEase(Ease.InOutBack));

            //古いメニューを画面外にスライド
            //画面外にスライド後、古いメニューは破棄
            endPosX += callingMenuIsNext ? -2000f : 2000f;
            sequence_.Join(currentMenuRectTransform.DOLocalMoveX(endPosX, duration_).SetEase(Ease.InOutBack));
            var currentMenuObject = currentMenuImage_.gameObject;
            sequence_.OnComplete(() => Destroy(currentMenuObject));
            //現在のメニューを新しいものに入れ替え
            currentMenuImage_ = newMenuImage;
            play();
        }

        /// <summary>
        /// スプライトを最初のものにリセットする(付随してスプライトのインデックスもリセット)
        /// </summary>
        public void resetSprite() {
            index_ = 0;
            currentMenuImage_.sprite = menuSprites_[index_];
        }
    }
}
