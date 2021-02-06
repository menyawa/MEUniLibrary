using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace MEUniLibrary.Encription {
    /// <summary>
    /// AESを用いて暗号化・復号化をする静的クラス
    /// インスタンスを用いる必要が無いため、静的クラスにしている
    /// 参考URL：https://www.paveway.info/entry/2019/04/08/csharp_encrypt
    /// </summary>
    public static class AesUtil {
        //ディレクトリのパスとファイル名
        private const string DIRECTORY_NAME = "config";
        private const string FILE_NAME = "IV_Data.txt";
        //使用するEncoding
        private static readonly Encoding ENCODING = Encoding.UTF8;

        /// <summary>
        /// 渡された文字列を暗号化し、暗号化後の文字列として返す
        /// </summary>
        /// <param name="plainStr"></param>
        /// <returns></returns>
        public static string encrypt(string plainStr) {
            var aes = createAesManaged(true);
            //復号化の際読み出せるように、初期化ベクトルをファイルとして保存しておく
            saveIV(aes.IV);

            var byteValue = ENCODING.GetBytes(plainStr);
            using (var encryptor = aes.CreateEncryptor()) {
                var encryptValue = encryptor.TransformFinalBlock(byteValue, 0, byteValue.Length);
                var base64Value = Convert.ToBase64String(encryptValue);

                return base64Value;
            }
        }

        /// <summary>
        /// 渡された暗号化文字列を復号し、プレーンな文字列として返す
        /// </summary>
        /// <param name="encryptedStr"></param>
        /// <returns></returns>
        public static string decrypt(string encryptedStr) {
            var aes = createAesManaged(false);
            //うまく復号化できなさそうなら(nullなら)復号化せずnullを返す
            if (aes == null) return null;

            //バイト列にし、復号、文字列に再変換して返す
            var byteValue = Convert.FromBase64String(encryptedStr);
            using (var decryptor = aes.CreateDecryptor()) {
                var decryptValue = decryptor.TransformFinalBlock(byteValue, 0, byteValue.Length);

                var plainStr = ENCODING.GetString(decryptValue);
                Debug.Log($"PlainStr: {plainStr}");
                return plainStr;
            }
        }

        /// <summary>
        /// 調整したAesManagedを生成、取得する
        /// 暗号化・復号どちらの際もほぼ処理が共通のためメソッドにまとめ上げて生成している
        /// </summary>
        /// <returns></returns>
        private static AesManaged createAesManaged(bool isEncryption) {
            var aes = new AesManaged();
            aes.KeySize = 128;
            //ブロック長は128が最大のため、256とかにするとエラーが出るので注意
            aes.BlockSize = 128;
            const string KEY = "xwsRfECJ_yohfUow?QE@3h,fb.NcYtok";
            //暗号化の際に新たに初期化ベクトルを生成する
            if (isEncryption) aes.GenerateIV();
            else {
                //復号の際にはセーブされたファイルからIVを読み出す
                //ファイルが無かった場合は読み取れないということでnullを返す
                var iv = loadIV();
                if (iv != null) aes.IV = iv;
                else return null;
            }

            aes.Key = ENCODING.GetBytes(KEY);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            return aes;
        }

        /// <summary>
        /// IV(初期化ベクトル)をファイルからロードする
        /// </summary>
        /// <returns></returns>
        private static byte[] loadIV() {
            //ファイルが存在するなら、IVをロードして返す
            var filePath = $"{Application.dataPath}/{DIRECTORY_NAME}/{FILE_NAME}";
            if (Directory.Exists($"{Application.dataPath}/{DIRECTORY_NAME}") && File.Exists(filePath)) {
                return File.ReadAllBytes(filePath);
            }

            //ファイルが存在しないならnullを返す
            return null;
        }

        /// <summary>
        /// IV(初期化ベクトル)をファイルとして保存する
        /// </summary>
        /// <param name="iv"></param>
        private static void saveIV(byte[] iv) {
            //ディレクトリが無かったら作成
            var directoryPath = $"{Application.dataPath}/{DIRECTORY_NAME}";
            if (Directory.Exists(directoryPath) == false) Directory.CreateDirectory(directoryPath);

            //ファイルを新しいIVで上書きする
            //バイト配列をそのまま書き込む
            File.WriteAllBytes($"{directoryPath}/{FILE_NAME}", iv);
        }
    }
}
