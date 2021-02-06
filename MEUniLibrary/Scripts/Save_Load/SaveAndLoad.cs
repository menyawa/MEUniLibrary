using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using MEUniLibrary.Encription;

namespace MEUniLibrary.SaveAndLoad {
    /// <summary>
    /// 渡されたインスタンスをセーブ・ロードする静的クラス
    /// このクラスを用いる際は、DataContractJsonSerializerを使用する関係で必ずMonoビルドを用いること
    /// また、同様の理由でセーブするインスタンスのクラスにはDataContractやDataMemberの属性を用いること
    /// </summary>
    public static class SaveAndLoad {
        //保存ファイルのディレクトリのパス、ファイルの名前
        private static readonly string DIRECTORY_NAME = "data";
        private static readonly string FILE_NAME = "data.json";

        /// <summary>
        /// 渡されたデータを渡されたディレクトリ、名前でセーブする
        /// 暗号化・非暗号化の制御が可能(デフォルトは暗号化)
        /// セーブディレクトリはApplication.dataPathに作られる
        /// </summary>
        public static void saveData<Type>(Type data, string directoryName, string fileName, bool encrypt = true) {
            Debug.Log("プレイヤーデータをセーブします");
            //渡されたデータが不正だったらメッセージを出して返す
            if(data == null) {
                Debug.Log("渡されたデータがnullのためセーブできません");
                return;
            }
            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrWhiteSpace(directoryName)) {
                Debug.Log("渡されたディレクトリの名前が不正なためセーブできません");
                return;
            }
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName)) {
                Debug.Log("渡されたファイルの名前が不正なためセーブできません");
                return;
            }

            //ディレクトリがないなら作っておく
            var directoryPath = $"{Application.dataPath}/{directoryName}";
            if (Directory.Exists(directoryPath) == false) Directory.CreateDirectory(directoryPath);

            //データのインスタンスをシリアライズし、書き出す
            using (var memoryStream = new MemoryStream()) {
                //System.Text.Json.JsonSerializerは使えないことに注意
                var serializer = new DataContractJsonSerializer(typeof(Type));

                //IL2CPPビルドを用いると、ビルド環境のみここでNullReferenceExceptionを吐かれるので特に注意
                //「必ず」Monoビルドを用いること、そしたら通る
                //try-catchして確かめたところ、memoryStreamとserializerのnullチェックはどちらもfalse
                //となるとWriteObject起因か？
                //一応try-catchでエラーを握りつぶせばIL2CPPでも正常に動くらしいけど、流石にやめよう
                serializer.WriteObject(memoryStream, data);

                string filePath = $"{directoryPath}/{fileName}";
                //メモリーストリームのカーソル位置をリセットする
                memoryStream.Position = 0;

                using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8)) {
                    //データのJsonを暗号化して書き込む
                    var str = encrypt ? AesUtil.encrypt(streamReader.ReadToEnd()) : streamReader.ReadToEnd();
                    File.WriteAllText(filePath, str, Encoding.UTF8);
                }
            }
        }

        /// <summary>
        /// 渡されたデータを予めクラスに設定されたディレクトリ、名前でセーブする
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <param name="data"></param>
        public static void saveData<Type>(Type data) {
            //渡されたデータが不正だったらメッセージを出して返す
            if (data == null) {
                Debug.Log("渡されたデータがnullのためセーブできません");
                return;
            }

            saveData<Type>(data, DIRECTORY_NAME, FILE_NAME);
        }

        /// <summary>
        /// 渡されたディレクトリ、ファイルの名前でデータをロードする
        /// ファイルが暗号化されているかどうかは指定する(デフォルトは暗号化)
        /// </summary>
        public static Type loadData<Type>(string directoryName, string fileName, bool fileIsEncrypted = true) {
            Debug.Log("プレイヤーデータをロードします");
            //渡された名前が不正ならメッセージを出してデフォルトデータを返す
            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrWhiteSpace(directoryName)) {
                Debug.Log("渡されたディレクトリの名前が不正なためロードできません");
                return default(Type);
            }
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrWhiteSpace(fileName)) {
                Debug.Log("渡されたファイルの名前が不正なためロードできません");
                return default(Type);
            }

            var directoryPath = $"{Application.dataPath}/{directoryName}";
            var filePath = $"{directoryPath}/{directoryName}";
            //存在するならセーブファイルをデシリアライズして読み出し、データのインスタンスに格納する
            if (Directory.Exists(directoryPath) && File.Exists(filePath)) {
                //文字列をファイルから読み取り、復号する
                var fileStr = File.ReadAllText(filePath, Encoding.UTF8);
                var plainStr = fileIsEncrypted ? AesUtil.decrypt(fileStr) : fileStr;
                //文字列が不正なら復号できないということなので、データをリセットして返す
                if (string.IsNullOrEmpty(plainStr) || string.IsNullOrWhiteSpace(plainStr)) {
                    Debug.Log("データが読み取れないためデフォルトデータを返します");
                    return default(Type);
                }

                //複合された文字列からデータのインスタンスを復元してセットする
                //復号前の文字列を間違って入れないように注意
                using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(plainStr))) {
                    var serializer = new DataContractJsonSerializer(typeof(Type));
                    //構造体はnull非許容のため、そこも考えるとas演算子でのキャストはできないことに注意
                    var deserializedData = (Type)serializer.ReadObject(memoryStream);
                    return deserializedData;
                }
            } else {
                //ディレクトリもしくはセーブファイルが存在しないならデータをリセットする
                Debug.Log("ファイルが存在しないのでデフォルトデータを返します");
                return default(Type);
            }
        }

        /// <summary>
        /// 予め設定されたパスのデータをロードする
        /// </summary>
        /// <typeparam name="Type"></typeparam>
        /// <returns></returns>
        public static Type loadData<Type>() {
            return loadData<Type>(DIRECTORY_NAME, FILE_NAME);
        }
    }
}
