using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class DBManager : Singleton<DBManager>
{
    // 이후 수정
    // 쓸려나?
    [SerializeField][ReadOnly] public List<DBEnemy> dbEnemy;
    [SerializeField][ReadOnly] public List<DBItem> dbItem;
}
