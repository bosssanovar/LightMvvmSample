# LightMvvmSample

## 概要

軽量なMVVMのソリューションサンプル。

業務範囲で必要とされていないViewModelのテスト容易性や、Viewの再利用性と切り替え可能性、Viewのコードビハインドに極力機能実装しないプラクティス等を切り捨てることで、正規のMVVMで必須になるMessengerやTrigger/Action, Behavior等の学習工ストの高い要素を排除した。

また一方で、アーキテクチャの境界を明確化するために、各カテゴリを別プロジェクトとして実装する。これにより、循環参照や越境設計のリスクを完全に排除し、モジュールの独立性を高めた。

## ソリューション構成

プロダクト ソリューション  
　│  
　├─ CodeSnippets フォルダ：コードスニペット置き場  
　│  
　├─ Infrastructure フォルダ：インフラ　プロジェクト　置き場  
　│　　│  
　│　　├─ DataStore プロジェクト：設定データ永続化機能  
　│　　│  
　│　　├─ Device プロジェクト：実機との通信機能  
　│　　│  
　│　　└─ Network プロジェクト：実機との通信のためのネットワークの足回り機能（資産があれば置き換え推奨）  
　│  
　├─ Tests フォルダ：テストプロジェクト置き場  
　│　　│  
　│　　└─ Entity_Test プロジェクト：Entity プロジェクトのユニットテスト  
　│  
　├─ Entiry プロジェクト：ドメインロジック（情報の本質的な機能）  
　│  
　├─ Usecase プロジェクト：アプリケーションロジック（アプリの本質的な機能）  
　│  
　├─ Repository プロジェクト：永続データアクセス機能  
　│  
　└─ プロダクト プロジェクト：エントリーポイント、各種UI  

## アーキテクチャ設計

MVVM  
View : 内部値に影響を受けない表示ロジック  
ViewModel : 内部値に影響を受ける表示ロジック、ViewとModelの設定値変換  
Model : ビジネスロジック（ドメインロジック）  

Clean Architecture簡易版  
プロダクト　─┬→　Device　───┬──→　Domain  
　　　　　　　│ 　　　│　　　　│     
　　　　　　　│ 　　　↓　　　　│  
　　　　　　　└→　Repository　┘

## 変更通知機構 Reactive Property

変更通知機構として、Reactive Propertyを採用している。  
Livetの候補であったが、コーディング量が少ないのでこちらを採用。（Livetの開発が止まっているという見方もあるが、必要な機能実装が完了しているのであって、オワコンではなく完成形。新しいVisual Studio環境への適応をマイクロソフトの中の人がメンターとなって運用されているため問題なし。現役バリバリで使える。）  
詳細はググること。

## コードスニペット

定型の実装を繰り返している場合には、「コードスニペット」を利用して、自動入力する。  
独自のコードスニペットを追加運用しているので、欲しいものは作って、全員で共有する。  
独自コードスニペット置き場は、ソリューション直下の「CodeSnippets」フォルダ内。そのフォルダもソース管理(git)上に置いているので、欲しいものを作ってコミットすれば、全員の資産となる。  
コードスニペットの記載方法はググれば出てくるので、そちらを。  
初期手順として、ソリューション直下の「CodeSnippets」フォルダ自体を Visual Studio に登録する必要があるが、そちらもググれば情報がある。  

### 運用ルール

* 独自コードスニペット置き場は、ソリューション直下の「CodeSnippets」フォルダ内。
* 仕様上、１つのコードスニペットファイルに複数のスニペットを定義可能であるが、運用面を重視して、１スニペット１ファイルとする。
* 独自コードスニペット追加時は、PC 上で「CodeSnippets」フォルダ内に直接ファイルを作成し、それファイルを Visual Studio で「追加」>「既存の項目」を行う。（PC 上フォルダにスニペットファイルが存在すれば、コードスニペットの利用に十分だが、目につきやすいソリューションエクスプローラ上に見える化する。）

### 実装ルール

* 独自のフォーマットにアクセスしやすいよう、接頭辞「cs\_」をつける。

### サンプル説明

* cs_regions：クラスのアウトラインとなるリージョン
* cs_detailViewModelTemplete：DataGrid等の行詳細ViewModelのテンプレート
* cs_reactiveProperty：Reactive Property Slimのプロパティ定義
* cs_viewClassTemplate：Viewコードビハインドのテンプレート
* cs_viewModelClassTemplete：Viewの分割クラスとして実装するViewModelのテンプレート

## ユニットテスト

xUnitフレームワークを採用。  
値オブジェクトと、コントローラーのテストを書く、最低限の運用で十分かと思っている。  
詳細はググる。

## コーディング規約

StyleCop.Analyzersを採用。  
暫定的に、最低限のルールを整備。  
プロジェクトごとに.editorconfigを設定する。  

https://qiita.com/YoshijiGates/items/d0d11582eec936807951  
https://yamaccu.github.io/tils/20210925-Csharp-StyleCop

## ミニトピック
* Regionを一気に閉じるため「Collapse All Regions」をVisual Studioの拡張機能で追加。「Ctrl+M, Ctrl+R」で一気に閉じる。一気に開くときは標準の「Ctrl+M, Ctrl+L」で。  
Collapse All Regions : https://marketplace.visualstudio.com/items?itemName=EngineDesigns.CollapseAllRegions
* 