using Godot;
using System;

public partial class BulletEnemy : Area2D
{
    [Export] public float speed;

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(0,speed);
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
