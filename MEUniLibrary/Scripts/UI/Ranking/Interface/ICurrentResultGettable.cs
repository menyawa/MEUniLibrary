using System;

namespace MEUniLibrary.UI.Ranking {
    /// <summary>
    /// 今回の記録を取得できるクラス用のインターフェース
    /// ランキングを付ける際の記録取得に用いるため順序付けをする必要があり、IComparableを実装していることが条件
    /// </summary>
    /// <typeparam name="Type"></typeparam>
    public interface ICurrentResultDataGettable<Type> where Type : IComparable {
        Type getCurrentResultData();
    }
}
