using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
    private float _speed;

    public override void _Ready()
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        _speed = random.RandiRange(1,3);
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(0, _speed);
        if(Position.Y > 720)
        {
            QueueFree();
        }
    }

    public void OnBodyEnteredShip(CharacterBody2D body)
    {
        if(body.IsInGroup("Bullet"))
        {
            QueueFree();
        }
    }

}
