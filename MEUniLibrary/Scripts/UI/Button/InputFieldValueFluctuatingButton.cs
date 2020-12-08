using UnityEngine;
using TMPro;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// 入力窓に入れられた値を決められた値上下させるボタンのクラス
    /// 入力された値は数値限定なことに注意
    /// </summary>
    public class InputFieldValueFluctuatingButton : ButtonBase {
        [SerializeField] private TMP_InputField inputField_;
        //一回ごとに上下する値
        //TODO：もう少し良い名前を考える
        [SerializeField] private double value_;

        private enum TYPE {
            PLUS,
            MINUS
        }
        [SerializeField] private TYPE type_;

        new private void Start() {
            base.Start();
            button_.onClick.AddListener(fluctuateValue);
        }

        /// <summary>
        /// 値を決められた値上下させる
        /// </summary>
        private void fluctuateValue() {
            //指定した値上下させる
            var number = double.Parse(inputField_.text);
            switch (type_) {
                case TYPE.PLUS:
                    number += value_;
                    break;
                case TYPE.MINUS:
                    number += value_;
                    break;
            }
            inputField_.text = number.ToString();

            //値の制限などあるかもしれないので編集扱いするために、onEndEditに登録されたメソッドを全て実行する
            //引数無しのInvokeが無かったので引数を入れているが、挙動に変わりは無いらしい？(少なくとも試した限りでは)
            //ちなみに引数はarg0と書いてあった、益々無いんじゃないか関係
            inputField_.onEndEdit.Invoke("");
        }
    }
}
