using Godot;
using System;

public partial class BulletTank : Area2D
{
    private CharacterBody2D _target;
    private Vector2 _direction;

    public override void _Ready()
    {
        _target = (CharacterBody2D)GetParent().GetNode("Player");
        LookAt(_target.Position);
        _direction = Position.DirectionTo(_target.Position);
    }
    public override void _PhysicsProcess(double delta)
    {          
        Position += _direction * 2;
        if(Position.Y > 730)
        {
            QueueFree();
        }
    }

    public void OnBodyEntered(CharacterBody2D body)
    {
        if(body.Name == "Player")
        {
            QueueFree();
        }
    }
}
