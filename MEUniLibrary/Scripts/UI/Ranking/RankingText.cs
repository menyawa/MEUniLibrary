using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace MEUniLibrary.UI.Ranking {
    /// <summary>
    /// ランキングのテキストを司る(ランキングの描画、ランクインした記録のテキストの取得、ハイライト)クラス
    /// テキストの参照をインスペクタから得なければいけない関係上MonoBehaviourを継承してはいるが、Rankingクラスから見てhas-Aの関係(部分クラス)
    /// </summary>
    public class RankingText : MonoBehaviour {
        //該当するランキングのテキスト群
        [SerializeField] private TextMeshProUGUI[] texts_;

        //現在のテキストがランクインした場合の該当記録のテキストの参照
        private TextMeshProUGUI rankInText_;

        /// <summary>
        /// ランキングデータを描画する
        /// </summary>
        public void drawData<Type>(List<Type> data, Type currentResultValue) {
            rankInText_ = getRankInText(data, currentResultValue);
            printText<Type>(data);
            highLightRankInText();
        }

        /// <summary>
        /// 今回の記録がランキング入りしたかどうか確認し、入っていたらその記録のテキストの参照を取得する
        /// タイムはfloat型、モノ数はint型なのでジェネリックを用いる
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="data"></param>
        /// <param name="currentResultValue"></param>
        /// <returns></returns>
        private TextMeshProUGUI getRankInText<Type>(List<Type> data, Type currentResultValue) {
            //同じ値で並んだ場合下位扱いにするため、逆から見ていく
            //配列概参照をしないよう(片方はリストだが)、セーブされている記録の要素数かランキングのテキスト数、どちらか小さい方に合わせる
            int rankingRange = texts_.Length < data.Count ? texts_.Length : data.Count;
            for (int index = rankingRange - 1; index >= 0; index--) {
                if (data[index].Equals(currentResultValue))
                    return texts_[index];
            }

            return null; //ランキング入りしなかった場合はnullを返す
        }

        /// <summary>
        /// ランキングをテキストに描画する
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="data"></param>
        private void printText<Type>(List<Type> data) {
            //配列外参照をしないよう(片方はリストだが)、セーブされている記録の要素数かランキングのテキスト数、どちらか小さい方に合わせる
            int rankingRange = texts_.Length < data.Count ? texts_.Length : data.Count;
            for (int index = 0; index < rankingRange; index++) {
                texts_[index].text = data[index].ToString();
            }
        }

        /// <summary>
        /// ランクインした記録があれば、テキストをハイライトする
        /// </summary>
        private void highLightRankInText() {
            var rankInTextColor = new Color(244f / 255f, 67f / 255f, 57f / 255f);
            if (currentResultIsRankIn())
                rankInText_.color = rankInTextColor;
        }

        /// <summary>
        /// ランクインした記録のテキストを元の色に戻し、参照を外す
        /// </summary>
        public void undoRankInText() {
            var nomalTextColor = new Color(24f / 255f, 24f / 255f, 24f / 255f);

            if (currentResultIsRankIn())
                rankInText_.color = nomalTextColor;
            rankInText_ = null;
        }

        /// <summary>
        /// 今回の記録がランクインしたかどうか
        /// </summary>
        /// <returns></returns>
        public bool currentResultIsRankIn() {
            return rankInText_ != null;
        }

        /// <summary>
        /// ランクインした今回の記録のテキストの参照を返す
        /// </summary>
        /// <returns></returns>
        public string getRankInStr() {
            return rankInText_.text;
        }
    }
}
