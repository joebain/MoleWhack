using System;
using UnityEngine;

namespace MoleBash
{
    public enum GameControllerState
    {
        Menu,
        Playing,
        GameOver
    }

    public class GameController : MonoBehaviour
    {
        public MoleController[] Moles;

        public AnimationCurve MoleIntervals, MoleDurations;
        
        public float Duration = 90;

        private float LastMoleAt = 0;

        private float GameStartTime;

        public GameControllerState State { get; private set; }
        public int Score { get; private set; }
        public float TimeRemaining { get; private set; }

        public VuforiaController Vuforia;
        public UIController UI;

        void Start()
        {
            GameStartTime = 0;
            Score = 0;
            State = GameControllerState.Menu;
        }

        void Update()
        {
            if (State == GameControllerState.Playing)
            {
                TimeRemaining = Duration - (Time.time - GameStartTime);
                if (TimeRemaining <= 0)
                {
                    State = GameControllerState.GameOver;
                    HideMoles();
                }
                else {
                    if (Moles.Length > 0)
                    {
                        float elapsed = Time.time - GameStartTime;
                        float moleInterval = MoleIntervals.Evaluate(elapsed / Duration);
                        if (Time.time - LastMoleAt > moleInterval)
                        {
                            MoleController randomMole = Moles[Mathf.FloorToInt(UnityEngine.Random.value * Moles.Length)];
                            if (randomMole.Hidden)
                            {
                                LastMoleAt = Time.time;
                                float runTime = Time.time - GameStartTime;
                                float moleDuration = MoleDurations.Evaluate(Mathf.Clamp01(runTime / Duration));
                                randomMole.Show(moleDuration);
                            }
                        }
                    }
                    else
                    {
                        if (Vuforia.TargetCopy != null)
                        {
                            Moles = Vuforia.TargetCopy.GetComponentsInChildren<MoleController>();
                        }
                    }
                    foreach (MoleController mole in Moles)
                    {
                        if (mole.Hit)
                        {
                            Score++;
                        }
                    }
                }
            }
        }

        private void HideMoles()
        {
            foreach (MoleController mole in Moles)
            {
                mole.gameObject.SetActive(false);
            }
        }

        private void ShowMoles()
        {
            foreach (MoleController mole in Moles)
            {
                mole.gameObject.SetActive(true);
                mole.Hide(); // this hides them in their holes so they are ready to start the game
            }
        }


        public void StartGame()
        {
            GameStartTime = Time.time;
            State = GameControllerState.Playing;
            Score = 0;
            ShowMoles();
            if (Vuforia != null && Vuforia.isActiveAndEnabled && !Vuforia.HasTarget)
            {
                Vuforia.BuildTarget();
            }
        }

        public void GoToMenu()
        {
            State = GameControllerState.Menu;
            Moles = new MoleController[0];
            Vuforia.DestroyTarget();
        }

        private static GameController Instance;
        public static GameController Get()
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<GameController>();
            }
            return Instance;
        }
    }
}
