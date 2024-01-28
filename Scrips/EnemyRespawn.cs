using Godot;
using System;

public partial class EnemyRespawn : Node
{
    [Export] public PackedScene enemy;
    [Export] public int timeRespawn;
    private Marker2D _spawnEnemy;
    private bool _canSpawn = true;
    private float _pointX;
    private PlayerController _target;

    public override void _Ready()
    {
        _spawnEnemy = GetNode<Marker2D>("SpawnEnemy");
        _target = (PlayerController)GetParent().GetNode("Player");

    }

    public override void _PhysicsProcess(double delta)
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        _pointX = random.RandiRange(25,455);
        _spawnEnemy.Position = new Vector2(_pointX, 0);
        
        if(_target.isAlive == true)
        {
            SpawnEnemies();
        }        
    }

    public async void SpawnEnemies()
    {        
        if(_canSpawn == true)
        {
            Area2D newEnemy = (Area2D)enemy.Instantiate();
            newEnemy.GlobalPosition = _spawnEnemy.GlobalPosition;
            GetParent().AddChild(newEnemy);

            _canSpawn = false;

            await ToSignal(GetTree().CreateTimer(timeRespawn), "timeout");
            _canSpawn = true;
        }
    }
}
