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

    public void OnBodyEntered(Area2D area)
    {
        if(area.Name == "Player")
        {
            QueueFree();
        }
    }
}
