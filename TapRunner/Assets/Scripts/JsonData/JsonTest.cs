using System;
using System.Collections;

[Serializable]
public class Records
{
    public Record[] records; // ���R�[�h�̔z��
}

[Serializable]
public class Record
{
    public string address;  // ���[�U�[�̃A�h���X
    public string name;     // ���[�U�[�̖��O
    public int score;       // ���[�U�[�̃X�R�A
    public string created;  // ���R�[�h�̍쐬����
    public string updated;  // ���R�[�h�̍X�V����
}