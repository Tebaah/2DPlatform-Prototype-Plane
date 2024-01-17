using Godot;
using System;

public partial class EnemyRespawn : Node
{
    [Export] public PackedScene enemy;
    private Marker2D _spawnEnemy;
    private bool _canSpawn = true;
    private float _pointX;

    public override void _Ready()
    {
        _spawnEnemy = GetNode<Marker2D>("SpawnEnemy");

    }

    public override void _PhysicsProcess(double delta)
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        _pointX = random.RandiRange(25,455);
        _spawnEnemy.Position = new Vector2(_pointX, 0);
        SpawnEnemies();
    }

    public async void SpawnEnemies()
    {        
        if(_canSpawn == true)
        {
            CharacterBody2D newEnemy = (CharacterBody2D)enemy.Instantiate();
            newEnemy.GlobalPosition = _spawnEnemy.GlobalPosition;
            GetParent().AddChild(newEnemy);

            _canSpawn = false;

            await ToSignal(GetTree().CreateTimer(5), "timeout");
            _canSpawn = true;
        }
    }
}
