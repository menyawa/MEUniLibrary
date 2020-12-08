using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 指定したUIのグループをCanvasGroupを用いて入力を受け付けるかどうかを切り替えるボタン
    /// </summary>
    public class SwitchInteractableForUIGroupButton : ButtonBase {
        [SerializeField] private CanvasGroup canvasGroup_;

        new protected void Start() {
            base.Start();
            button_.onClick.AddListener(switchUIGroupInteractable);
        }

        public void switchUIGroupInteractable() {
            canvasGroup_.interactable = !canvasGroup_.interactable;
        }
    }
}