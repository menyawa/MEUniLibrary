using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 入力窓に入力した名前のシーンをロードするクラス
    /// </summary>
    public class LoadingInputSceneButton : LoadingSceneButton {
        [SerializeField] private TMP_InputField inputField_;

        protected override void loadScene() {
            sceneName_ = inputField_.text;

            //どうやらBuildSettingに登録されたシーン一覧を取得できないらしいので、名前比較でビルドに指定されたシーンが含まれているかどうかを見てエラーを弾くことは不可能
            //なので素直に入れられたテキストでシーンを愚直に呼びましょう
            base.loadScene();
        }
    }
}
