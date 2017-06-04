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
        private float CurrentDuration;
        public float TimeRangeForMoleAppearences = 120;

        private float LastMoleAt = 0;

        private float GameStartTime;

        public GameControllerState State { get; private set; }
        public int Score { get; private set; }
        public float TimeRemaining { get; private set; }

        public VuforiaController Vuforia;
        public UIController UI;

        private float IceMoleCounter = 0;
        public float IceMoleInterval = 20;
        public int IceMoleTimeExtend = 20;
        public float IceMoleDuration = 1f;
        public float FireMoleChance = 0.2f;
        public float FireMoleTimePenalty = 10;
        public int FireMoleScorePenalty = 5;
        public float EarthMoleChance = 0.05f;
        public int EarthMoleScore = 5;
        
        void Start()
        {
            GameStartTime = 0;
            Score = 0;
            State = GameControllerState.Menu;
            
            UI.WipeMid += WipeMidCallback;
        }

        void Update()
        {
            if (State == GameControllerState.Playing)
            {
                float TimeElapsed = Time.time - GameStartTime;
                TimeRemaining = CurrentDuration - TimeElapsed;
                IceMoleCounter -= Time.deltaTime;
                if (TimeRemaining <= 0)
                {
                    bool allHidden = true;
                    for (int m = 0; m < Moles.Length; m++)
                    {
                        MoleController mole = Moles[m];
                        if (!mole.Hidden)
                        {
                            allHidden = false;
                            break;
                        }
                    }
                    if (allHidden)
                    {
                        State = GameControllerState.GameOver;
                    }
                }
                else {
                    if (Moles.Length > 0)
                    {
                        float moleInterval = MoleIntervals.Evaluate(TimeElapsed / CurrentDuration);
                        if (Time.time - LastMoleAt > moleInterval)
                        {
                            MoleController randomMole = Moles[Mathf.FloorToInt(UnityEngine.Random.value * Moles.Length)];
                            if (randomMole.Hidden && randomMole.isActiveAndEnabled)
                            {
                                LastMoleAt = Time.time;
                                float moleDuration = MoleDurations.Evaluate(Mathf.Clamp01(TimeElapsed / TimeRangeForMoleAppearences));

                                MoleType type = MoleType.Normal;
                                float rand = UnityEngine.Random.value;
                                if (IceMoleCounter < 0)
                                {
                                    IceMoleCounter = IceMoleInterval;
                                    type = MoleType.Ice;
                                    moleDuration = IceMoleDuration;
                                }
                                else if (rand < FireMoleChance)
                                {
                                    type = MoleType.Fire;
                                }
                                else if (rand < EarthMoleChance)
                                {
                                    type = MoleType.Earth;
                                }

                                randomMole.Show(moleDuration, type);
                            }
                        }
                    }
                    else
                    {
                        if (Vuforia.TargetCopy != null)
                        {
                            Moles = Vuforia.TargetCopy.GetComponentsInChildren<MoleController>();
                            HideMoles();
                        }
                    }
                    foreach (MoleController mole in Moles)
                    {
                        if (mole.Hit)
                        {
                            if (mole.Type == MoleType.Normal)
                            {
                                Score++;
                            }
                            else if (mole.Type == MoleType.Ice)
                            {
                                CurrentDuration += IceMoleTimeExtend;
                            }
                            else if (mole.Type == MoleType.Earth)
                            {
                                Score += EarthMoleScore;
                            }
                            else if (mole.Type == MoleType.Fire)
                            {
                                CurrentDuration -= FireMoleTimePenalty;
                                Score -= FireMoleScorePenalty;
                            }
                        }
                    }
                }
            }
        }

        private void WipeMidCallback()
        {
            if (State == GameControllerState.GameOver)
            {
                HideMoles();
            }
            else if (State == GameControllerState.Playing)
            {
                ShowMoles();
                GameStartTime = Time.time;
                IceMoleCounter = IceMoleInterval;
            }
        }

        private void HideMoles()
        {
            foreach (MoleController mole in Moles)
            {
                mole.gameObject.SetActive(false);
                mole.Hit = false;
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
            State = GameControllerState.Playing;
            Score = 0;
            CurrentDuration = Duration;
            GameStartTime = Time.time;
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
