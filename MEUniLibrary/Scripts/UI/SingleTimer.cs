using UnityEngine;
using TMPro;
using MEUniLibrary.UI.Ranking;

namespace MEUniLibrary.UI {
    /// <summary>
    /// 単独で動作するタイマーのクラス
    /// </summary>
    public class SingleTimer : MonoBehaviour, ICurrentResultDataGettable<float> {
        //カウントアップかカウントダウンどちらのタイプか
        [SerializeField]
        private enum TYPE {
            COUNT_UP,
            COUNT_DOWN
        }
        [SerializeField] private TYPE type_;
        [SerializeField] private TextMeshProUGUI text_;
        //分、秒の初期値
        [SerializeField] private int initialMinute_;
        [SerializeField] private float initialSecond_;

        //現在の分、秒の値
        private int currentMinute_;
        private float currentSecond_;

        //タイマーを動かせるか否か(諸事情でストップさせたい時はfalseに)
        public bool canRun_ { set; private get; }


        private void Start() {
            resetTime();
        }

        private void Update() {
            if (canRun_) {
                printTime();
                updateTime();
            }
        }

        /// <summary>
        /// 対象のテキストにタイムを描画する(10:00のように分、秒どちらも整数2桁で表記)
        /// </summary>
        public void printTime() {
            var currentSecondForInt = Mathf.RoundToInt(currentSecond_);
            text_.text = $"{currentMinute_.ToString("D2")}:{currentSecondForInt.ToString("D2")}";
        }

        /// <summary>
        /// タイムを更新する
        /// </summary>
        private void updateTime() {
            const float MINUTE_TO_SECOND = 60.0f; //1分あたりの秒
            switch (type_) {
                case TYPE.COUNT_UP:
                    currentSecond_ += Time.deltaTime;
                    //秒が60秒を超えたら分に変換
                    while (currentSecond_ >= MINUTE_TO_SECOND) {
                        currentSecond_ -= MINUTE_TO_SECOND;
                        currentMinute_++;
                    }
                    break;
                case TYPE.COUNT_DOWN:
                    currentSecond_ -= Time.deltaTime;
                    while (currentSecond_ < 0.0f) {
                        //秒が0秒以下だったら分がまだ余っている場合秒に変換、もうタイム切れの場合00:00とする
                        if (currentMinute_ >= 1) {
                            currentMinute_--;
                            currentSecond_ += MINUTE_TO_SECOND;
                        } else {
                            currentSecond_ = 0.0f; //秒が0以下だと見栄えが悪いので、0で止めておく
                            break;
                        }
                    }
                    break;
            }
        }

        /// <summary>
        /// タイムをリセットする
        /// </summary>
        public void resetTime() {
            currentMinute_ = initialMinute_;
            currentSecond_ = initialSecond_;
        }

        /// <summary>
        /// 今回のタイムの記録を取得する
        /// 主にリザルトや、ランキング用
        /// </summary>
        /// <returns></returns>
        public float getCurrentResultData() {
            //分も秒に直し、合計して返す
            return currentMinute_ * 60.0f + currentSecond_;
        }
    }
}
