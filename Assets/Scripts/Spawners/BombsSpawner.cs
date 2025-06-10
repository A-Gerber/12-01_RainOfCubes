using UnityEngine;


public class BombsSpawner : Spawner<Bomb>
{
    [SerializeField] private CubesSpawner _cubesSpawner;
    [SerializeField] private Exploder _exploder;

    private Vector3 _position;

    private void OnEnable()
    {
        _cubesSpawner.Released += GetBomb;
    }

    private void OnDisable()
    {
        _cubesSpawner.Released -= GetBomb;
    }

    protected override void OnGet(Bomb bomb)
    {
        base.OnGet(bomb);
        bomb.transform.position = _position;

        bomb.ReleasedBomb += Release;
    }

    protected override void OnRelease(Bomb bomb)
    {
        base.OnRelease(bomb);

        bomb.ReleasedBomb -= Release;
    }

    protected override void Release(Bomb bomb)
    {
        base.Release(bomb);
        bomb.TransparencyChanger.SetDefault();

        _exploder.Explode(bomb);
    }

    private void GetBomb(Cube cube)
    {
        _position = cube.transform.position;

        Get();
    }
}