using UnityEngine;
using UnityEngine.UI;

namespace MoleBash
{
    public class UIController : MonoBehaviour
    {
        public GameObject Logo;
        public Button StartGameButton;
        public Text StartGameButtonText;
        public Text Score, Time;
        public Text FinalScoreText;

        private Quaternion InitialLogoRotation;

        public GameObject GameOverUI, MenuUI, GameUI;

        void Start()
        {
            Input.gyro.enabled = true;
            InitialLogoRotation = Logo.transform.localRotation;
            Debug.Log("got logo rotation " + InitialLogoRotation.eulerAngles);
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

        void Update()
        {
            GameControllerState gameState = GameController.Get().State;
            if (gameState == GameControllerState.Playing)
            {
                HideMenu();
                ShowGameUI();
                HideGameOverUI();

                Score.text = GameController.Get().Score.ToString();
                Time.text = GameController.Get().TimeRemaining.ToString("n0");
            }
            else if (gameState == GameControllerState.Menu)
            {
                ShowMenu();
                HideGameUI();
                HideGameOverUI();

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
                HideGameUI();
                HideMenu();
                ShowGameOverUI();

                FinalScoreText.text = GameController.Get().Score.ToString();
            }
        }
    }
}
