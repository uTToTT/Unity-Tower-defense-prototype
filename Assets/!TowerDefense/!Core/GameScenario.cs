using NaughtyAttributes;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GameScenario", menuName = "Game/GameScenario")]
public class GameScenario : ScriptableObject
{
    [Expandable]
    [SerializeField] private EnemyWave[] _waves;

    public State Begin() => new State(this);

    [Serializable]
    public struct State
    {
        private int _index;

        private GameScenario _scenario;
        private EnemyWave.State _wave;

        public State(GameScenario scenario)
        {
            _scenario = scenario;
            _index = 0;
            Debug.Assert(scenario._waves.Length > 0, "Empty scenario!");
            _wave = _scenario._waves[0].Begin();
        }

        public bool Progress()
        {
            float deltaTime = _wave.Progress(Time.deltaTime);
            while (deltaTime >= 0f)
            {
                if (++_index >= _scenario._waves.Length)
                {
                    return false;
                }

                _wave = _scenario._waves[_index].Begin();
                deltaTime = _wave.Progress(deltaTime);
            }

            return true;
        }
    }
}
