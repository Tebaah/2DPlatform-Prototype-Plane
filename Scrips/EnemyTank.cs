using Godot;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime;

public partial class EnemyTank : Area2D
{
    public CharacterBody2D target;
    private AnimatedSprite2D _spriteController;
    private CollisionShape2D _collisionController;
    private bool _isDead = false;
    private float _startRotation = 0f;
    public override void _Ready()
    {
        target = (CharacterBody2D)GetParent().GetNode("Player");
        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collisionController = GetNode<CollisionShape2D>("CollisionShape2D");

        _startRotation = Rotation;
    }

    public override void _PhysicsProcess(double delta)
    {
        LookAtTarget(target);
    }

    public void LookAtTarget(CharacterBody2D target)
    {
        var direction = (target.GlobalPosition - GlobalPosition);
        var angle = Transform.X.AngleTo(direction);
        Rotate(angle)
;    }

    public async void OnAreaEnteredTank(Area2D area)
    {

        if(area.IsInGroup("Bullet"))
        {
            _collisionController.Visible = false;
            _spriteController.Play("death");
            _isDead = true;

            await ToSignal(GetTree().CreateTimer(0.3),"timeout");
            QueueFree();
        }
    }
}
