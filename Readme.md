## このライブラリは
---
「あんなこといいな」「できたらいいな」を集めたUnity用の自作汎用ライブラリです。
UI関連が多いです。    
マサカリは大歓迎です。  

## 使用法
---
クラスのみ取り出してUnityプロジェクトのスクリプトフォルダに突っ込んで使ってください。  
ゲームオブジェクトに付加する前提のクラス(MonoBehaviourを継承するクラス)が多数ありますので、DLLにするとそれらのクラスが使用できません(スクリプト上からは使用できますが、オブジェクトへの付加はできません)  

## 必要な外部ライブラリ
---
### Unity公式
---
色々書いてありますが、とりあえずUnityプロジェクトのDLL参照と同じものを用意してくれればOKです。   
編集せず利用のみの場合は気にしないでUnityプロジェクトに突っ込んでもらえれば動くと思います。  

・Unityの新規プロジェクトを作成した際に参照に追加される各種DLL  
  現状のバージョンは2019.4.1f1のものを使用しています。  
・ScriptAssemblies  
  こちらもUnityプロジェクトと同じものを用意し、このプロジェクトのひとつ上のフォルダに置いてください。そうすればパスが通るはずです。  

### アセットストアより
---
このプロジェクトのひとつ上のフォルダに「Plugins_for_Library」というフォルダを生成し、そこに全部突っ込んでおけばパスが通るはずです。  

・DOTween  
・Easy Save 3  

### その他利用させていただいているクラス
---
・SingletonMonoBehaviour(「Plugins_for_Library/DesignPatterns/」に入れてください)  
  https://qiita.com/okuhiiro/items/3d69c602b8538c04a479