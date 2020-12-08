using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 指定したオブジェクトの表示・非表示を切り替えるボタンのクラス
    /// </summary>
    public class SwitchObjectsActiveButton : ButtonBase {
        [SerializeField] private GameObject[] gameObjects_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(switchObjectActive);
        }

        private void switchObjectActive() {
            foreach (var gameObject in gameObjects_) {
                gameObject.SetActive(!gameObject.activeSelf);
            }
        }
    }
}
