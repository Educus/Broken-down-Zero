using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    // ���� ����
    // ������?
    [SerializeField][ReadOnly] public List<DBEnemy> dbEnemy;
    [SerializeField][ReadOnly] public List<DBItem> dbItem;
}
