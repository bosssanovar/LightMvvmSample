# LightMvvmSample

軽量なMVVMのソリューションサンプル。  

---

- [概要](#概要)
- [ソリューション構成](#ソリューション構成)
- [アーキテクチャ設計](#アーキテクチャ設計)
  - [MVVM](#mvvm)
  - [簡易版 Clean Architecture](#簡易版-clean-architecture)
- [変更通知機構 Reactive Property](#変更通知機構-reactive-property)
- [依存性の注入](#依存性の注入)
- [コードスニペット](#コードスニペット)
  - [運用ルール](#運用ルール)
  - [実装ルール](#実装ルール)
  - [サンプル説明](#サンプル説明)
- [ユニットテスト](#ユニットテスト)
- [テストカバレッジ](#テストカバレッジ)
- [コーディング規約](#コーディング規約)
- [設計](#設計)
- [Visual Studio環境構築](#visual-studio環境構築)
  - [Notifier 2022](#notifier-2022)
  - [Solution Error Notifier](#solution-error-notifier)
  - [Collapse All Regions](#collapse-all-regions)
- [チェックリスト](#チェックリスト)
- [ミニトピック](#ミニトピック)

---

## 概要

業務範囲で必要とされていないViewModelのテスト容易性や、Viewの再利用性と切り替え可能性、Viewのコードビハインドに極力機能実装しないプラクティス等を切り捨てることで、正規のMVVMで必須になるMessengerやTrigger/Action, Behavior等の学習工ストの高い要素を排除した。

また一方で、アーキテクチャの境界を明確化するために、各カテゴリを別プロジェクトとして実装する。これにより、循環参照や越境設計のリスクを完全に排除し、モジュールの独立性を高めた。  

---

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
　│　　└─ Network プロジェクト：実機との通信のためのネットワークの足回り機能  
　│　　　　　　　　　　　　　　（資産があれば置き換え推奨）  
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

---

## アーキテクチャ設計

ビューロジックアーキテクチャとしてMVVMを、ドメインロジックアーキテクチャとして簡易版のClean Architectureを採用している。

### MVVM

ビューとロジックを分離するために、MVVMを採用する。  

View  
内部値に影響を受けない表示ロジック  

ViewModel  
内部値に影響を受ける表示ロジック、ViewとModelの設定値変換  

Model  
ビジネスロジック（ドメインロジック）  

上記の一般的なプラクティスを考え方のベースとして、本プロジェクトでは、その一般的なプラクティスから以下の点が異なる。

- ViewとViewModeを別クラスとせず、partialクラスとしてファイルを分けることにとどめる。それにより、ViewModelからViewの操作を可能とする。

### 簡易版 Clean Architecture

機能の実装箇所の明確化のため、ドメインロジックをClean Architectureに発想を得た簡易版を採用する。  
真正のClean Architectureは、設計としては申し分なくできるが、学習コストが非常に高く、当該プロジェクトにおいて品質過剰(※)であるため、採用を見送っている。

※Clean Architectureは、ドメイン外部の要因は状況により変化しやすい部分、ドメイン内部の要因は変化しにくい部分として、ドメイン内部の要因をドメイン外部の要因から隔離することで、ドメイン外部の要因の変化に強い設計を実現することを念頭に置かれた設計である。当該プロジェクトにおいては、ウォーターフォール開発であることから、そこまでドメイン外部の変化が生じないことが予想されるため、品質過剰と判断した。

![Architecture_Design](https://github.com/bosssanovar/LightMvvmSample/assets/19525768/b109558e-56f7-43ed-a6e4-96440c4d3d53)

Entity  
値オブジェクトやエンティティの定義を行う。

Repository  
Entityオブジェクトを保持し、その取得や保存のみを行う。  
受け持つEntityの中の部分的な取得や保存の機能は持たず、受け持つEntity全体の取得保存だけを行う。  
DDDにおける本来のRepositoryは、その裏に永続化の手段（オンメモリ、DB、クラウド等）を隠ぺいするための機能を有するが、本設計においてはオンメモリだけの簡略版となっている。  
Entityオブジェクト置き場としての役割のみとなるが、画面とデータを分離するための大切な設計箇所である。

Usecase  
ユースケース単位で、Repositoryを利用して、Entityオブジェクトを取得し、ドメインロジックを実行する。

Infrastructure  
外部要因の実装を行う。

App  
アプリUIの実装（MVVM）を行う。  

---

## 変更通知機構 Reactive Property

変更通知機構として、Reactive Propertyを採用している。  
Livetも候補であったが、コーディング量が少ないのでこちらを採用。（Livetの開発が止まっているという見方もあるが、必要な機能実装が完了しているのであって、オワコンではなく完成形。新しいVisual Studio環境への適応をマイクロソフトの中の人がメンターとなって運用されているため問題なし。現役バリバリで使える。）  
詳細はググること。  

---

## 依存性の注入

RepositoryとUsecaseはインターフェースによる設計を行い、実行時のインスタンスの生成はDIコンテナを利用している。  

インターフェースによる設計を行うことにより、部分実装作る込みが可能となる。  
インターフェースを境界にそこにモックを差し込める。  
例えば画面実装中に、Usecaseコンポーネントの実装を待たずに、インターフェースを実装するモッククラスを自分で用意して所望のUsecase動作を実装すれば、Usecaseが居る前提での画面側作り込みが可能となる。

利用したDIコンテナは、「Microsoft.Extensions.DependencyInjection」  
サービスコレクションの登録、プロバイダからの具象クラスインスタンス取得は、「AppServiceProvider」クラスにカプセル化している。

---

## コードスニペット

定型の実装を繰り返している場合には、「コードスニペット」を利用して、自動入力する。  
独自のコードスニペットを追加運用しているので、欲しいものは作って、全員で共有する。  
独自コードスニペット置き場は、ソリューション直下の「CodeSnippets」フォルダ内。そのフォルダもソース管理(git)上に置いているので、欲しいものを作ってコミットすれば、全員の資産となる。  
コードスニペットの記載方法はググれば出てくるので、そちらを。  
初期手順として、ソリューション直下の「CodeSnippets」フォルダ自体を Visual Studio に登録する必要があるが、そちらもググれば情報がある。  

### 運用ルール

- 独自コードスニペット置き場は、ソリューション直下の「CodeSnippets」フォルダ内。
- 仕様上、１つのコードスニペットファイルに複数のスニペットを定義可能であるが、運用面を重視して、１スニペット１ファイルとする。
- 独自コードスニペット追加時は、PC 上で「CodeSnippets」フォルダ内に直接ファイルを作成し、それファイルを Visual Studio で「追加」>「既存の項目」を行う。（PC 上フォルダにスニペットファイルが存在すれば、コードスニペットの利用に十分だが、目につきやすいソリューションエクスプローラ上に見える化する。）

### 実装ルール

- 独自のフォーマットにアクセスしやすいよう、接頭辞「cs\_」をつける。

### サンプル説明

- cs_regions：クラスのアウトラインとなるリージョン
- cs_detailViewModelTemplete：DataGrid等の行詳細ViewModelのテンプレート
- cs_reactiveProperty：Reactive Property Slimのプロパティ定義
- cs_viewClassTemplate：Viewコードビハインドのテンプレート
- cs_viewModelClassTemplete：Viewの分割クラスとして実装するViewModelのテンプレート  

---

## ユニットテスト

xUnitフレームワークを採用。  
EntityやDomainSeriveceのテストを書く程度の最低限の運用で十分かと思っている。  
Usecaseは、起動するまでにオブジェクト構築が複雑であること、Usecaseの振る舞い自体はEntityとDomainService機能の組み合わせになるので、テスト対象としていない。  
詳細はググる。  

---

## テストカバレッジ

Fine Code Coverageを採用。  
<https://qiita.com/imp-kawano/items/1177b3f6ca1fb2107ba6>  

ユニットテスト実行時に、カバレッジを取得する。  

---

## コーディング規約

StyleCop.Analyzersを採用。  
暫定的に、最低限のルールを整備。  
プロジェクトごとに.editorconfigを設定する。  

<https://qiita.com/YoshijiGates/items/d0d11582eec936807951>  
<https://yamaccu.github.io/tils/20210925-Csharp-StyleCop>

---

## 設計

[設計資料はこちら](Documents/Designs/Design.md)

---

## Visual Studio環境構築

効率的に開発を進めるために、Visual Studioの機能拡張はもはや必須。  
プレーンで使っているなんて怠惰ですね。の勢いで、地味に作業効率と開発快適性が上がる、もはや無くてはならないレベル。

### [Notifier 2022](https://github.com/NDiiong/Notifier)

ビルドが完了したことを、ウィンドウズの通知で教えてくれる。  
TDDしていると、いつテストが終わったのかわかりにくいが、これを入れていると、ビルドが終わったことがぼーっとしていても知れるので、そこからテストが始まるのがわかり、意識的にテスト完了待ちをする必要がある時間が削減で着て、省エネ。

![sample](https://github.com/bosssanovar/LightMvvmSample/assets/19525768/f254098d-3a2e-4edb-8b19-98460a53dbfd)

### [Solution Error Notifier](https://marketplace.visualstudio.com/items?itemName=ShemeerNS.SolutionErrorNotifierX86)

エラー、警告、情報の件数が、コードエディタ上に表示される。  
エラーウィンドウを開く手間が減るので省エネ。  
エラーウィンドウ開いてみたら大惨事、ってことがなくなり、いつでもエラーが1件でも出た瞬間に知れるので、ストレス軽減。

![ErrorNotifierExtension](https://github.com/bosssanovar/LightMvvmSample/assets/19525768/f4c02db9-7c15-4b02-a202-1f76baf8c291)

### [Collapse All Regions](https://marketplace.visualstudio.com/items?itemName=EngineDesigns.CollapseAllRegions)

Regionを一気に閉じるため「Collapse All Regions」をVisual Studioの拡張機能で追加。  
「Ctrl+M, Ctrl+R」で一気に閉じる。一気に開くときは標準の「Ctrl+M, Ctrl+L」で。  

---

## チェックリスト

[こちら](Documents/Processs/チェックリスト.md)

---

## ミニトピック

- コードレビューの視点
  - 登録されたイベントハンドラを解除しているか
  - ReactivePropertyをCompositeDisposableに追加しているか
- 値オブジェクトはC#言語仕様（[レコード型](https://ufcpp.net/study/csharp/datatype/record/)）で実現する。
