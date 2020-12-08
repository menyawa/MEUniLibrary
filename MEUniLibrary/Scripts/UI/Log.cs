using UnityEngine;
using TMPro;

namespace MEUniLibrary.UI {
    /// <summary>
    /// 下に向けて更新されていくログのクラス
    /// </summary>
    public class Log : MonoBehaviour {
        [SerializeField] private TextMeshProUGUI text_;

        private void Start() {
            resetLog();
        }

        /// <summary>
        /// ログの末尾に文字列を付け足す
        /// </summary>
        /// <param name="logStr"></param>
        public void add(string logStr) {
            text_.text = logStr + "\n" + text_.text;
        }

        /// <summary>
        /// ログの文字列をリセットする
        /// </summary>
        public void resetLog() {
            text_.text = "";
        }
    }
}
