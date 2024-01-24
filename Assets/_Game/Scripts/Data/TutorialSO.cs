using UnityEngine;

[CreateAssetMenu(fileName = "TutorialData", menuName = "ScriptableObjects/TutorialSO")]
public class TutorialSO : ScriptableObject
{
    [SerializeField]
    private TutorialData[] tutorialData;

    public bool HasNextTutorial(int index)
    {
        return index < this.tutorialData.Length - 1;
    }

    public bool HasPreviousTutorial(int index)
    {
        return index > 0;
    }

    public TutorialData GetTutorialData(int index)
    {
        return this.tutorialData[index];
    }
}
