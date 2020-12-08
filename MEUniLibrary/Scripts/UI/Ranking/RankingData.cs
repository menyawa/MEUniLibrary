using System;
using System.Collections.Generic;

namespace MEUniLibrary.UI.Ranking {
    /// <summary>
    /// ランキングのデータを管理するクラス
    /// RankingTextと違いランキングごとにメンバの違いが存在せず、全データにおいておなじメソッドの処理を行うため、通常のクラスでなく静的クラス化
    /// </summary>
    public static class RankingData {
        /// <summary>
        /// 渡された今回の記録を加えた最新のランキングをセーブする
        /// 記録のタイプはまちまちなので、ジェネリックを用いる
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="key"></param>
        /// <param name="currentResultValue"></param>
        public static List<Type> saveLatestData<Type>(string key, string filePath, Type currentResultValue) where Type : IComparable {
            //セーブデータをロードする(なかったら初期化する)
            var data = ES3.KeyExists(key, filePath) ? ES3.Load<List<Type>>(key, filePath) : new List<Type>();
            //今回の値を加えた上でソートすればランキングとなって揃う
            data.Add(currentResultValue);
            data.Sort();
            ES3.Save<List<Type>>(key, data, filePath);
            return data;
        }

        /// <summary>
        /// ランキングの削除(正確にはキーを削除するだけだが、これでアクセスできなくなるのでOK)
        /// </summary>
        /// <param name="key"></param>
        /// <param name="filePath"></param>
        public static void deleteData(string key, string filePath) {
            //キーが無いのに削除しようとするとIOExceptionを吐くので注意
            //キーを削除すればその記録にはアクセスできなくなるし、再度セーブされるときには上書きされる
            if (ES3.KeyExists(key, filePath))
                ES3.DeleteKey(key, filePath);
        }
    }
}
