using System.IO;
using UnityEngine;

namespace MEUniLibrary.UI.Image {
    /// <summary>
    /// スクリーンショットのクラス
    /// 描画や画像のズームも実現するため、ImagePanelを継承
    /// </summary>
    public class ScreenShot : ImagePanel {
        /// <summary>
        /// スクリーンショットを撮り、保存してパスを返す
        /// </summary>
        /// <returns></returns>
        public string capture(string imageTitle) {
            var timeStamp = System.DateTime.Now.ToString("yyyyMMddHHmmss");
            var directoryPath = Application.dataPath + "/ScreenShots/";
            var title = imageTitle + "_" + timeStamp + ".png";
            var filePath = directoryPath + title;
            //ディレクトリが無かったら作成しておく
            if (Directory.Exists(directoryPath) == false) {
                Directory.CreateDirectory(directoryPath);
            }
            ScreenCapture.CaptureScreenshot(filePath);

            Debug.Log("Capture ScreenShot!");
            return filePath;
        }
    }
}
