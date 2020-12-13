using UnityEngine;
using UnityEngine.SceneManagement;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 予めセットした名前のシーンをロードするボタン
    /// </summary>
    public class LoadingSceneButton : ButtonBase {
        [SerializeField] protected string sceneName_;
        //ロード前に遅延を挟める
        [SerializeField] private float delay_;

        new protected void Start() {
            base.Start();
            button_.onClick.AddListener(loadScene);
        }

        protected virtual void loadScene() {
            SceneManager.LoadScene(sceneName_);
        }
    }
}
