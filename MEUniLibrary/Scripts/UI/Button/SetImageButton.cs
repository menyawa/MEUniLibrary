using UnityEngine;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 対象のImageに指定した画像をセットするボタンのクラス
    /// </summary>
    public class SetImageButton : ButtonBase {
        [SerializeField] private UnityEngine.UI.Image image_;
        [SerializeField] private Sprite sprite_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(setImage);
        }

        private void setImage() {
            image_.sprite = sprite_;
        }
    }
}
