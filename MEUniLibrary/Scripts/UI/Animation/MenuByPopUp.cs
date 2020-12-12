using UnityEngine;
using DG.Tweening;

namespace MEUniLibrary.UI.Animation {
    /// <summary>
    /// ポップアップで出てくるメニューのクラス
    /// </summary>
    public class MenuByPopUp : DOTweenAnimationSequenceBase {
        [SerializeField] private Sprite[] menuSprites_;
        //どのスプライトを指しているかのインデックス
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
            //動的取得とすることで、同じImageを使い回せる
            //フィールドの現在のImageの参照を入れ替える方法では、使い回すと他のメニューを入れ替えた際、他のメニューの参照まで消えてしまう
            //そして次に開いた際に参照が消えているため、弄くろうとしてnull参照となるため、動的取得が有利
            //また、行数も削減できる(フィールドの参照を入れ替える作業が不要になる)
            //一番手前のImageが一番最新なので、それを取得する
            //さもないと複数のメニューが画面内に残留してしまう
            var currentMenuObject = transform.GetChild(transform.childCount - 1);
            var currentMenuImage = currentMenuObject.GetComponent<UnityEngine.UI.Image>();
            //右か左、どちらかカーソルが押された方の画面外に新メニューを生成
            //メニューの親オブジェクト(このオブジェクト)があるので、その子になるよう調整(背景パネルの上かつ、カーソルの下でないといけない)
            //メニューはメニューで一つ親オブジェクトを作ってそれだけでまとめておくことで、カーソル等のUIの順序を乱さない
            var position = callingMenuIsNext ? new Vector2(2000f, 0f) : new Vector2(-2000f, 0f);
            var newMenuImage = Instantiate(currentMenuImage.gameObject, position, Quaternion.identity, transform).GetComponent<UnityEngine.UI.Image>();

            var currentMenuRectTransform = currentMenuImage.transform as RectTransform;
            var newMenuRectTransform = newMenuImage.transform as RectTransform;
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
            sequence_.OnComplete(() => Destroy(currentMenuObject));
            play();
        }

        /// <summary>
        /// スプライトを最初のものにリセットする(付随してスプライトのインデックスもリセット)
        /// </summary>
        public void resetSprite() {
            index_ = 0;
            var currentMenuImage = transform.GetComponentInChildren<UnityEngine.UI.Image>();
            currentMenuImage.sprite = menuSprites_[index_];
        }
    }
}
