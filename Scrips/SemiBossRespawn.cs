using Godot;
using System;

public partial class SemiBossRespawn : Node
{
    [Export] public PackedScene enemy;
    [Export] public int timeRespawn;
    private Marker2D _spawnEnemy;
    private bool _canSpawn = true;
    private float _pointX;
    private PlayerController _target;
    private Global _global;
    private float _positionY = 250;

    public override void _Ready()
    {
        _spawnEnemy = GetNode<Marker2D>("SpawnEnemy");
        _target = (PlayerController)GetParent().GetNode("Player");
        _global = GetNode<Global>("/root/Global");

    }

    public override void _PhysicsProcess(double delta)
    {
        _spawnEnemy.Position = new Vector2(240, 0);
        
        if(_target.isAlive == true && _global.positionScenarioY == _positionY)
        {
            SpawnEnemies();
            _positionY += 250;
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
