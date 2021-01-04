using UnityEngine;
using MEUniLibrary.UI.Animation;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 押した際にAnimationForScaleを起動するボタン
    /// </summary>
    public class AnimationForScaleButtton : SwitchOpeningAndClosingOfUIButtonBase {
        [SerializeField] private AnimationForScale animationForScale_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(startAnimation);
        }

        private void startAnimation() {
            //開くボタンか閉じるボタンかで最大化・最小化を変える
            switch (type_) {
                case TYPE.OPEN:
                    animationForScale_.expand();
                    break;
                case TYPE.CLOSE:
                    animationForScale_.minimize();
                    break;
            }
        }
    }
}
