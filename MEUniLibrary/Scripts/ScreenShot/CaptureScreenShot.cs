using System.IO;
using UnityEngine;

namespace MEUniLibrary.ScreenShot {
    /// <summary>
    /// スクリーンショットを撮り保存するクラス
    /// </summary>
    public static class CaptureScreenShot {
        /// <summary>
        /// スクリーンショットを撮り、保存してパスを返す
        /// </summary>
        /// <returns></returns>
        public static string capture(string stageName) {
            var timeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            var directoryPath = Application.dataPath + "/ScreenShots/";
            var imageTitle = stageName + "_" + timeStamp + ".png";
            if (Directory.Exists(directoryPath) == false)
                Directory.CreateDirectory(directoryPath);

            var filePath = directoryPath + imageTitle;
            ScreenCapture.CaptureScreenshot(filePath);

            Debug.Log("Capture ScreenShot!");
            return filePath;
        }
    }
}
