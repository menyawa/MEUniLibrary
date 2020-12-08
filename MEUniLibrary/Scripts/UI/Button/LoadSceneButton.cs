using UnityEngine;
using UnityEngine.SceneManagement;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 予めセットした名前のシーンをロードするボタン
    /// </summary>
    public class LoadSceneButton : ButtonBase {
        [SerializeField] private string loadSceneName_;

        new protected void Start() {
            base.Start();
            button_.onClick.AddListener(loadScene);
        }

        private void loadScene() {
            SceneManager.LoadScene(loadSceneName_);
        }
    }
}
