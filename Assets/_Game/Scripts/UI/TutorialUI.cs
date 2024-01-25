using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : UIBase
{
    [SerializeField]
    private TutorialSO tutorialSO;

    [SerializeField] private Button nextButton;
    [SerializeField] private Button previousButton;
    [SerializeField] private Image tutorialImage;
    [SerializeField] private TMP_Text tutorialDescription;
    [SerializeField] private Button playButton;

    private int currentTutorialIndex = 0;

    private void OnEnable()
    {
        this.currentTutorialIndex = 0;
        this.nextButton.interactable = true;
        this.previousButton.interactable = false;
        this.playButton.gameObject.SetActive(false);
        this.tutorialImage.sprite = this.tutorialSO.GetTutorialData(this.currentTutorialIndex).tutorialImage;
        this.tutorialDescription.text = this.tutorialSO.GetTutorialData(this.currentTutorialIndex).tutorialDescription;
    }

    private void Start()
    {
        this.nextButton.onClick.AddListener(NextTutorial);
        this.previousButton.onClick.AddListener(PreviousTutorial);
        this.playButton.onClick.AddListener(CloseTutorial);
    }

    private void NextTutorial()
    {
        if (this.tutorialSO.HasNextTutorial(this.currentTutorialIndex))
        {
            this.currentTutorialIndex++;
            this.tutorialImage.sprite = this.tutorialSO.GetTutorialData(this.currentTutorialIndex).tutorialImage;
            this.tutorialDescription.text = this.tutorialSO.GetTutorialData(this.currentTutorialIndex).tutorialDescription;
            this.previousButton.interactable = true;
        }

        if (!this.tutorialSO.HasNextTutorial(this.currentTutorialIndex))
        {
            this.nextButton.interactable = false;
            playButton.gameObject.SetActive(true);
        }
    }

    private void PreviousTutorial()
    {
        if (this.tutorialSO.HasPreviousTutorial(this.currentTutorialIndex))
        {
            this.currentTutorialIndex--;
            this.tutorialImage.sprite = this.tutorialSO.GetTutorialData(this.currentTutorialIndex).tutorialImage;
            this.tutorialDescription.text = this.tutorialSO.GetTutorialData(this.currentTutorialIndex).tutorialDescription;
            this.nextButton.interactable = true;
            this.playButton.gameObject.SetActive(false);
        }

        if (!this.tutorialSO.HasPreviousTutorial(this.currentTutorialIndex))
        {
            this.previousButton.interactable = false;
        }
    }

    private void CloseTutorial()
    {
        UIManager.Instance.ClosePopup(this);
    }
}
