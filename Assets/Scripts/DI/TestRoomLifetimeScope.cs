using UnityEngine;
using VContainer;
using VContainer.Unity;

public class TestRoomLifetimeScope : LifetimeScope
{
    [SerializeField] GameObject _playerPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        var newPlayer = Container.Instantiate(_playerPrefab, Vector3.zero, Quaternion.identity);
    }
}
