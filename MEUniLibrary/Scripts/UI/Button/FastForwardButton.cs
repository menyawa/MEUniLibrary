using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// Time.timeScaleを弄ってゲーム全体を早送りにするボタンのクラス
    /// </summary>
    public class FastForwardButton : ButtonBase {
        //何倍早送りにするかの倍率
        [SerializeField] private float multiplyingFactor_;
        [SerializeField] private bool canUseShortCutKey_;

        public bool isFastForwarded_ { private set; get; }

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(switchFastForward);
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.F) && canUseShortCutKey_)
                switchFastForward();
        }

        /// <summary>
        /// 早送りの開始・解除の切り替え
        /// </summary>
        public void switchFastForward() {
            const float NOMAL_SPEED = 1.0f;
            Time.timeScale = isFastForwarded_ ? NOMAL_SPEED : multiplyingFactor_;
            isFastForwarded_ = !isFastForwarded_;
        }
    }
}
