
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BackgroundData", menuName = "ScriptableObjects/BackGroundSO")]
public class BackGroundSO : ScriptableObject
{
    [SerializeField] private List<BackGroundData> _listBackGround;

    public int BackgroundListCount()
    {
        return _listBackGround.Count;
    }
    public BackGroundData GetBackgroundByIndex(int index)
    {
        return _listBackGround[index];
    }
}
