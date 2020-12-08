namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// ゲームを終了するボタンのクラス
    /// </summary>
    public class ExitGameButton : ButtonBase {
        new private void Start() {
            base.Start();
            button_.onClick.AddListener(exitGame);
        }

        private void exitGame() {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #elif UNITY_STANDALONE
                UnityEngine.Application.Quit();
            #endif
        }
    }
}
