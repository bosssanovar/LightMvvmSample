# LightMvvmSample

---

## イントロ

---

## ソリューション構成

プロダクト ソリューション
　│
　├ CodeSnippets フォルダ：コードスニペット置き場
　│
　├ Tests フォルダ：テストプロジェクト置き場
　│　　│
　│　　└ Domain_Test プロジェクト：Domein プロジェクトのユニットテスト
　│
　├ Devices プロジェクト：実機との通信機能
　│
　├ Domain プロジェクト：ドメインロジック（アプリの本質的な機能）
　│
　├ Repository プロジェクト：永続データ管理機能
　│
　└ プロダクト プロジェクト：エントリーポイント、各種UI

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

---
