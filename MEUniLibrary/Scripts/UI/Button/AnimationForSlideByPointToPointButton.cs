using UnityEngine;
using MEUniLibrary.UI.Animation;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// ボタンを押した際AnimationForSlideByPointToPointを起動するボタン
    /// </summary>
    public class AnimationForSlideByPointToPointButton : SwitchOpeningAndClosingOfUIButtonBase {
        [SerializeField] private AnimationForSlideByPointToPoint animationForSlideByPointToPoint_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(startAnimation);
        }

        private void startAnimation() {
            switch (type_) {
                case TYPE.OPEN:
                    animationForSlideByPointToPoint_.slide(false);
                    break;
                case TYPE.CLOSE:
                    animationForSlideByPointToPoint_.slide(true);
                    break;
            }
        }
    }
}
