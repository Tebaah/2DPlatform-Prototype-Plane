using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
    private float _speed;
    private AnimatedSprite2D _spriteController;

    public override void _Ready()
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        _speed = random.RandiRange(1,3);

        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(0, _speed);
        if(Position.Y > 720)
        {
            QueueFree();
        }
    }

    public async void OnBodyEnteredShip(CharacterBody2D body)
    {
        if(body.IsInGroup("Bullet"))
        {
            _spriteController.Play("death");

            await ToSignal(GetTree().CreateTimer(0.3),"timeout");
            QueueFree();
        }
    }

}
