using System;
using UnityEngine;

namespace MEUniLibrary.UI.Ranking {
    /// <summary>
    /// ランキングを管理するクラス
    /// 部分クラスとしてテキストを管理するRankingTextを保持
    /// </summary>
    public class Ranking : MonoBehaviour {
        //指定したデータをセーブする際のキー、ファイルのパス
        [SerializeField] private string key_;
        [SerializeField] private string filePath_;

        [SerializeField] private RankingText rankingText_;

        /// <summary>
        /// ランキングを上書きして表示する
        /// 対象のデータを引数で渡すのではなく、取得するクラスをメンバにキャッシュしてそこから取得する方法は、ジェネリックを用いる関係上静的にTypeのデータ型を指定しなければならないため無理なことに注意
        /// </summary>
        public void overwriteRanking<Type>(Type currentResultValue) where Type : IComparable {
            var data = RankingData.saveLatestData<Type>(key_, filePath_, currentResultValue);
            rankingText_.drawData(data, currentResultValue);
        }
    }
}
