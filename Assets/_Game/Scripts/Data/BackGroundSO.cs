
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundData", menuName = "ScriptableObjects/BackGroundSO")]
public class BackGroundSO : ScriptableObject
{
    [SerializeField] private List<BackgroundData> _listBackGround;

    public int BackgroundListCount()
    {
        return _listBackGround.Count;
    }
    public BackgroundData GetBackgroundByIndex(int index)
    {
        return _listBackGround[index];
    }
}
