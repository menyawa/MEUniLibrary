using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// �w�肵��UI�̃O���[�v��CanvasGroup��p���ē��͂��󂯕t���邩�ǂ�����؂�ւ���{�^��
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