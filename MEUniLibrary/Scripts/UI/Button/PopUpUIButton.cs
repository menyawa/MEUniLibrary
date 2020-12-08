using UnityEngine;
using DG.Tweening;
using MEUniLibrary.UI.Animation;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 指定したUIをポップアップする・閉じるボタンのクラス
    /// </summary>
    public class PopUpUIButton : SwitchOpeningAndClosingOfUIButtonBase {
        [SerializeField] private AnimationForPopUp[] popUpMenuAnimations_;

        new protected void Start() {
            base.Start();
            button_.onClick.AddListener(popUp);
        }

        /// <summary>
        /// メニューの要素を全てアニメーションを付けてポップアップ・閉じる
        /// また、背景パネルも表示する
        /// </summary>
        private void popUp() {
            Sequence sequence = DOTween.Sequence();
            switch (type_) {
                case TYPE.OPEN:
                    foreach (var animation in popUpMenuAnimations_)
                        animation.joinPopUp(sequence);
                    break;
                case TYPE.CLOSE:
                    foreach (var animation in popUpMenuAnimations_)
                        animation.joinClose(sequence);
                    break;
            }
            sequence.Play();
        }
    }
}
