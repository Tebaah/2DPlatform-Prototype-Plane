using Godot;
using System;

public partial class BulletController : Area2D
{
    [Export] public float speed;
    public override void _PhysicsProcess(double delta)
    {
        Position -= new Vector2(0,speed);
        if(Position.Y < -2)
        {
            QueueFree();
        }
    }
    public void OnBodyEntered(CharacterBody2D body)
    {
        if(body.IsInGroup("Enemy"))
        {
            QueueFree();
        }
    }

    public void OnAreaEntered(Area2D area)
    {
        if(area.IsInGroup("Enemy"))
        {
            QueueFree();
        }
    }
}
