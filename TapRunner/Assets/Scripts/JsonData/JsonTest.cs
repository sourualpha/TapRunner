using System;
using System.Collections;

[Serializable]
public class Records
{
    public Record[] records; // レコードの配列
}

[Serializable]
public class Record
{
    public string address;  // ユーザーのアドレス
    public string name;     // ユーザーの名前
    public int score;       // ユーザーのスコア
    public string created;  // レコードの作成日時
    public string updated;  // レコードの更新日時
}