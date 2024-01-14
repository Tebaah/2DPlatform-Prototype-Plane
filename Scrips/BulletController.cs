using Godot;
using System;

public partial class BulletController : CharacterBody2D
{
    [Export] public float speed;
    public override void _PhysicsProcess(double delta)
    {
        Position -= new Vector2(0,speed);
    }
}
