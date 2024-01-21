using Godot;
using System;

public partial class powerUpController : CharacterBody2D
{
    [Export] public int speed;

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(0,speed);
        if(Position.Y > 730)
        {
            QueueFree();
        }
    }
}
