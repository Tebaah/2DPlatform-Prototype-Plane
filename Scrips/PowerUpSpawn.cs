using Godot;
using System;

public partial class PowerUpSpawn : Node
{
    // Variables 

    [Export] public PackedScene powerUp;
    private Marker2D _spawnPowerUp;
    private bool _canSpawn = true;
    private float _pointX;

    // Methods

    public override void _Ready()
    {
        _spawnPowerUp = GetNode<Marker2D>("SpawnPowerUp");
    }

    public override void _PhysicsProcess(double delta)
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        _pointX = random.RandiRange(25, 455);
        _spawnPowerUp.Position = new Vector2(_pointX, 0);
        SpawnPowerUps(); 
    }

    public async void SpawnPowerUps()
    {
        if(_canSpawn == true)
        {
            CharacterBody2D newPowerUp = (CharacterBody2D)powerUp.Instantiate();
            newPowerUp.GlobalPosition = _spawnPowerUp.GlobalPosition;
            GetParent().AddChild(newPowerUp);

            _canSpawn = false;

            await ToSignal(GetTree().CreateTimer(10), "timeout");
            _canSpawn = true;
        }
    }
}
