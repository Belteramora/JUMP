using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RhythmGameStarter
{
    [HelpURL("https://bennykok.gitbook.io/rhythm-game-starter/hierarchy-overview/stats-system")]
    public class StatsSystem : MonoBehaviour
    {
        [Comment("Responsible for advance stats' config and events.", order = 0)]
        [Title("Hit Level Config", false, 2, order = 1)]
        [Tooltip("Config the hit distance difference for each level, such as Perfect,Ok etc")]
        public HitLevelList levels;

        [Title("Events", 2)]
        public Slider comboSlider;
        [CollapsedEvent]
        public StringEvent onComboStatusUpdate;
        [CollapsedEvent]
        public StringEvent onScoreUpdate;
        [CollapsedEvent]
        public StringEvent onMaxComboUpdate;
        [CollapsedEvent]
        public StringEvent onMissedUpdate;

        #region RUNTIME_FIELD
        [NonSerialized] public float combo = 3;
        [NonSerialized] public float maxCombo;
        [NonSerialized] public int missed;
        [NonSerialized] public int score;
        [NonSerialized] public float comboMultiplier = 1;
        #endregion

        #region XDComboStats

        private enum Scale
        {
            ZeroLife = 0,
            FirstLife = 1,
            SecondLife = 2,
            ThirdLife = 3,
            TwoXCombo = 6,
            FourXCombo = 12
        }

        private float lastCombo;

        #endregion

        [Serializable]
        public class HitLevelList : ReorderableList<HitLevel> { }

        [Serializable]
        public class HitLevel
        {
            public string name;
            public float threshold;
            [HideInInspector]
            public int count;
            public float scorePrecentage = 1;
            public float comboCount;
            public StringEvent onCountUpdate;
        }

        public void AddMissed(int addMissed)
        {
            missed += addMissed;
            if(combo > (int)Scale.SecondLife)
            {
                combo = (int)Scale.SecondLife;
            }
            else if(combo > (int)Scale.FirstLife)
            {
                combo = (int)Scale.FirstLife;
            }
            else
            {
                combo = (int)Scale.ZeroLife;
                Debug.Log("gameEnded");
            }

            comboSlider.value = combo;

            onMissedUpdate.Invoke(missed.ToString());
        }

        void Start()
        {
            UpdateScoreDisplay();
            combo = 3;
        }

        public void AddCombo(int addCombo, float deltaDiff, int addScore)
        {
            // print(deltaDiff);

            for (int i = 0; i < levels.values.Count; i++)
            {
                var x = levels.values[i];
                if (deltaDiff <= x.threshold)
                {
                    x.count++;
                    score += (int)(addScore * x.scorePrecentage * comboMultiplier);
                    lastCombo = combo;
                    combo += x.comboCount;

                    if (combo >= (int)Scale.TwoXCombo && combo <= (int)Scale.FourXCombo)
                    {
                        comboMultiplier = 2f;
                    }
                    else if (combo >= (int)Scale.FourXCombo)
                    {
                        comboMultiplier = 4f;
                        combo = (int)Scale.FourXCombo;
                    }
                    else
                    {
                        comboMultiplier = 1f;
                    }

                    comboSlider.value = combo;

                    x.onCountUpdate.Invoke(x.count.ToString());
                    UpdateScoreDisplay();
                    onComboStatusUpdate.Invoke(x.name);
                    // print(x.name);
                    return;
                }
            }

            //When no level matched
            onComboStatusUpdate.Invoke("");

        }

        public void UpdateScoreDisplay()
        {
            onScoreUpdate.Invoke(score.ToString());

        }
    }
}