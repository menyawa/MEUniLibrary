using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 入力窓に入力した名前のシーンをロードするクラス
    /// </summary>
    public class LoadInputSceneButton : ButtonBase {
        [SerializeField] private TMP_InputField inputField_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(loadScene);
        }

        private void loadScene() {
            //どうやらBuildSettingに登録されたシーン一覧を取得できないらしいので、名前比較でビルドに指定されたシーンが含まれているかどうかを見てエラーを弾くことは不可能
            //なので素直に入れられたテキストでシーンを愚直に呼びましょう
            SceneManager.LoadScene(inputField_.text);
        }
    }
}
