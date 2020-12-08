using UnityEngine;

namespace MEUniLibrary.UI.InputField {
    /// <summary>
    /// 入力窓に入力された数値を補正するクラス
    /// </summary>
    public class InputNumberFieldManager : InputStringFieldManager {
        //範囲の最大値、最低値
        [SerializeField] private int max_;
        [SerializeField] private int min_;

        private void Start() {
            inputField_.onEndEdit.AddListener(fixInputValue);
            fixInputValue(defaultValue_);
        }

        protected override void fixInputValue(string text) {
            base.fixInputValue(text);

            //最大値、最低値の範囲を超えたら修正する
            int inputNumber = int.Parse(inputField_.text);
            if (inputNumber > max_) inputField_.text = max_.ToString();
            if (inputNumber < min_) inputField_.text = min_.ToString();
        }
    }
}
