using UnityEngine;

public class Game : MonoBehaviour, IInitializable
{
    [SerializeField] private Transform _spawnPoint;

    [SerializeField ] private GameScenario _scenario;
    [SerializeField] private MoveManager _moveManager;

    private GameScenario.State _activeScenario;

    private static Game _instance;

    public bool IsInitialized {  get; private set; }

    public bool Init()
    {
        _instance = this;
        _activeScenario = _scenario.Begin();

        IsInitialized = true;
        return IsInitialized;
    }

    public static void SpawnEnemy(EnemyFactory factory)
    {
        factory.Init();
        Enemy enemy = factory.Create();
        Vector2Int v = new ((int)_instance._spawnPoint.position.x, (int)_instance._spawnPoint.position.y);
        enemy.transform.SetIntPosition(v);
    }

    private void Update()
    {
        if (!IsInitialized) return;

        _activeScenario.Progress();

        _moveManager.GameUpdate();
    }
}
