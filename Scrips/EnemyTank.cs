using Godot;
using System;

public partial class EnemyTank : Area2D
{
    public CharacterBody2D target;

    public override void _Ready()
    {
        target = GetNode<CharacterBody2D>("Main/Player");
    }

    public override void _PhysicsProcess(double delta)
    {
        LookAtTarget();
    }

    public void LookAtTarget()
    {
 
        LookAt(target.GlobalPosition);
        Vector2 direction = (GlobalPosition - target.GlobalPosition).Normalized();

    }
}
