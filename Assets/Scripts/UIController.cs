using UnityEngine;
using UnityEngine.UI;

namespace MoleBash
{
    public delegate void WipeEventHandler();

    public class UIController : MonoBehaviour
    {
        public GameObject Logo;
        public Button StartGameButton;
        public Text StartGameButtonText;
        public Text Score, Time;
        public Text FinalScoreText;
        public Animator Wipe;

        private int ScoreValue, TimeValue;

        private int frameTicker = 0;

        private Quaternion InitialLogoRotation;

        public GameObject GameOverUI, MenuUI, GameUI;
        private Animator ScoreAnim, TimeAnim;

        private GameControllerState prevState;

        public event WipeEventHandler WipeStart, WipeMid, WipeEnd;

        void Start()
        {
            Input.gyro.enabled = true;
            InitialLogoRotation = Logo.transform.localRotation;

            ScoreAnim = Score.GetComponent<Animator>();
            TimeAnim = Time.GetComponent<Animator>();

            prevState = GameController.Get().State;

            HideGameOverUI();
            HideGameUI();
            ShowMenu();
        }

        private void ShowMenu()
        {
            MenuUI.SetActive(true);
        }

        private void HideMenu()
        {
            MenuUI.SetActive(false);
        }

        private void ShowGameUI()
        {
            GameUI.SetActive(true);
        }

        private void HideGameUI()
        {
            GameUI.SetActive(false);
        }

        private void ShowGameOverUI()
        {
            GameOverUI.SetActive(true);
        }

        private void HideGameOverUI()
        {
            GameOverUI.SetActive(false);
        }

        public void TriggerWipeEnd()
        {
            if (WipeEnd != null) WipeEnd();
        }
        public void TriggerWipeMid()
        {
            if (WipeMid != null) WipeMid();

            var state = GameController.Get().State;
            if (state == GameControllerState.Playing)
            {
                HideMenu();
                HideGameOverUI();
                ShowGameUI();
            }
            else if (state == GameControllerState.GameOver)
            {
                HideGameUI();
                HideMenu();
                ShowGameOverUI();
            }
            else if (state == GameControllerState.Menu)
            {
                HideGameOverUI();
                HideGameUI();
                ShowMenu();
            }
        }
        public void TriggerWipeStart()
        {
            if (WipeStart != null) WipeStart();
        }

        void Update()
        {
            GameControllerState gameState = GameController.Get().State;
            if (gameState != prevState)
            {
                prevState = gameState;
                Wipe.SetTrigger("Wipe");
            }
            if (gameState == GameControllerState.Playing)
            {
                //HideMenu();
                //ShowGameUI();
                //HideGameOverUI();
                frameTicker++;
                if (frameTicker >= 8)
                {
                    frameTicker = 0;

                    int scoreNow = GameController.Get().Score;
                    int scoreDiff = ScoreValue - scoreNow;
                    if (Mathf.Abs(scoreDiff) > 30)
                    {
                        ScoreValue = scoreNow;
                    }
                    else if (scoreDiff < 0)
                    {
                        ScoreValue++;
                        ScoreAnim.SetTrigger("Pulse");
                    }
                    else if (scoreDiff > 0)
                    {
                        ScoreValue--;
                        ScoreAnim.SetTrigger("Pulse");
                    }

                    int timeNow = Mathf.RoundToInt(Mathf.Max(GameController.Get().TimeRemaining, 0));
                    int timeDiff = TimeValue - timeNow;
                    if (Mathf.Abs(timeDiff) > 20)
                    {
                        TimeValue = timeNow;
                    }
                    else if (timeDiff < 0)
                    {
                        TimeValue++;
                        if (timeDiff < -1)
                        {
                            TimeAnim.SetTrigger("Pulse");
                        }
                    }
                    else if (timeDiff > 0)
                    {
                        TimeValue--;
                        if (timeDiff > 1)
                        {
                            TimeAnim.SetTrigger("Pulse");
                        }
                    }

                    Score.text = ScoreValue.ToString();
                    Time.text = TimeValue.ToString();
                }
            }
            else if (gameState == GameControllerState.Menu)
            {
                //ShowMenu();
                //HideGameUI();
                //HideGameOverUI();

                if (GameController.Get().Vuforia.TrackingState == TrackingState.LowQuality)
                {
                    StartGameButton.interactable = false;
                    StartGameButtonText.text = "No clear view";
                }
                else
                {
                    StartGameButton.interactable = true;
                    StartGameButtonText.text = "Start Game!";
                }

                var x = Mathf.Rad2Deg * Input.gyro.rotationRate.x;
                var y = -Mathf.Rad2Deg * Input.gyro.rotationRate.y;
                
                Logo.transform.Rotate(new Vector3(x, y, 0)*UnityEngine.Time.deltaTime);
                Logo.transform.localRotation = Quaternion.RotateTowards(Logo.transform.localRotation, InitialLogoRotation, 0.2f);
            }
            else if (gameState == GameControllerState.GameOver)
            {
                //HideGameUI();
                //HideMenu();
                //ShowGameOverUI();

                FinalScoreText.text = GameController.Get().Score.ToString();
            }
        }
    }
}
