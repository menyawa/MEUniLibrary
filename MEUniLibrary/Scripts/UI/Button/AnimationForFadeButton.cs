using MEUniLibrary.UI.Animation;
using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// AnimationForFadeを用いてUIをフェードイン・フェードアウトするクラス
    /// </summary>
    public class AnimationForFadeButton : SwitchOpeningAndClosingOfUIButtonBase {
        [SerializeField] private AnimationForFade animationForFade_;

        new private void Start() {
            base.Start();

            button_.onClick.AddListener(fade);
        }

        private void fade() {
            switch (type_) {
                case TYPE.OPEN:
                    animationForFade_.fade();
                    break;
                case TYPE.CLOSE:
                    animationForFade_.fade(0.0f);
                    break;
            }
        }
    }
}
