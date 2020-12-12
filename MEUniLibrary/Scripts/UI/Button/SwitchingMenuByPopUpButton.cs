using UnityEngine;
using MEUniLibrary.UI.Animation;
using System.IO;

namespace MEUniLibrary.UI.Button {
    /// <summary>
    /// ポップアップメニューの開閉を切り替えするボタンのクラス
    /// ボタンによってはフラグを見て開かないこともある
    /// そのため、AnimationForScaleButtonと併用は不可(自分で拡大・縮小する)
    /// 同様に、FadeBackgroundPanelBySwitchOpeningMenuButtonとも併用不可(これは抽象クラスなのでそもそもコンポーネント付加できないが)
    /// </summary>
    public class SwitchingMenuByPopUpButton : FadeBackgroundPanelBySwitchOpeningMenuButton {
        [SerializeField] private MenuByPopUp menuByPopUp_;
        [SerializeField] private AnimationForScale animationForMenuScale_;

        //メニューによっては、一度開いたらもう開かないという類のものもある
        //その際はファイルにフラグをセーブしておき、それを見て開く開かないを変更する
        //そのためのフラグをチェックするかどうかのフラグ、フォルダ名・ファイル名・セーブのキー
        //そのような類のメニューでも明示的にユーザが見たい場合は見せる、ということもあるため、メニュー側でなくボタン側で制御
        [SerializeField] private bool needCheckWatchingList_;
        [SerializeField] private string directoryName_;
        [SerializeField] private string fileName_;
        [SerializeField] private string key_;
        //どこのパスに保存されているかのタイプ
        private enum PATH_TYPE {
            DATA_PATH,
            PERSISTENT_DATA_PATH,
            TEMPORARY_CACHE_PATH,
            STREAMING_ASSETS_PATH,
        }
        [SerializeField] private PATH_TYPE pathType_;

        new private void Start() {
            base.Start();

            button_.onClick.AddListener(openMenu);
        }

        /// <summary>
        /// メニューを開く
        /// </summary>
        private void openMenu() {
            var path = getPathForWatchingList();
            switch (type_) {
                case TYPE.OPEN:
                    if (needCheckWatchingList_) {
                        //一度見たらもう開かないという類のメニューのボタンだった場合、フラグをチェックして既に見ていたら開かない
                        if (ES3.FileExists(path)) {
                            if (ES3.KeyExists(key_, path))
                                if (ES3.Load<bool>(key_, path)) return;
                        }
                        //そうでないなら、見たというフラグをセーブする
                        ES3.Save<bool>(key_, true, path);
                    }

                    //開く前に、一番最初の画像にリセットしておく
                    menuByPopUp_.resetSprite();
                    animationForMenuScale_.expand();
                    break;
                case TYPE.CLOSE:
                    //一度見たらもう開かないという類のメニューのボタンだった場合、見たというフラグをセーブする
                    if (needCheckWatchingList_) ES3.Save<bool>(key_, true, path);

                    animationForMenuScale_.minimize();
                    break;
            }

            //同時並行で背景パネルをフェードする
            fadeBackgroundPanel();
        }

        /// <summary>
        /// リストのファイルのパスを取得する
        /// </summary>
        /// <returns></returns>
        private string getPathForWatchingList() {
            var path = "";
            switch (pathType_) {
                case PATH_TYPE.DATA_PATH:
                    path += Application.dataPath;
                    break;
                case PATH_TYPE.PERSISTENT_DATA_PATH:
                    path += Application.persistentDataPath;
                    break;
                case PATH_TYPE.TEMPORARY_CACHE_PATH:
                    path += Application.dataPath;
                    break;
                case PATH_TYPE.STREAMING_ASSETS_PATH:
                    path += Application.streamingAssetsPath;
                    break;
            }
            path += $"{directoryName_}/";
            //フォルダがなかったら生成しておく
            if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
            path += $"{fileName_}.es3";

            return path;
        }
    }
}
