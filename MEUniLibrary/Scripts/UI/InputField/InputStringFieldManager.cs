using UnityEngine;
using TMPro;

namespace MEUniLibrary.UI.InputField {
    /// <summary>
    /// 入力窓に入力された文字列を修正するクラス
    /// </summary>
    public class InputStringFieldManager : MonoBehaviour {
        [SerializeField] protected TMP_InputField inputField_;
        //入力されていなかった場合の既定値
        [SerializeField] protected string defaultValue_;

        private void Start() {
            inputField_.onEndEdit.AddListener(fixInputValue);
            fixInputValue(defaultValue_);
        }

        /// <summary>
        /// 入力窓に入力された値を規定の条件に沿って修正する
        /// </summary>
        /// <param name="text"></param>
        protected virtual void fixInputValue(string text) {
            //入力されていなかったら、既定値を入力
            if (inputField_.text == "") inputField_.text = defaultValue_;
        }
    }
}
