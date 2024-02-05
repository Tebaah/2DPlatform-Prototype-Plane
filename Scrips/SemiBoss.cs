using Godot;
using System;

public partial class SemiBoss : Area2D
{
    // Variables
    private AnimatedSprite2D _spriteController;
    private CollisionShape2D _myCollision;
    private bool _isDead = false;
    private Global _global;

    // Methods

    public override void _Ready()
    {
        _myCollision = GetNode<CollisionShape2D>("CollisionShape2D");
        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _global = GetNode<Global>("/root/Global");
    }


    public override void _PhysicsProcess(double delta)
    {
        if(Position.Y < 300)
        {
            Position += new Vector2(0, 1);
        }

    }

       public async void OnBodyEnteredShip(Area2D area)
    {
        if(area.IsInGroup("Bullet"))
        {
            _myCollision.Visible = false;   
            _spriteController.Play("death");

            _global.pointsPlayer += 10;
            
            _isDead = true;
            await ToSignal(GetTree().CreateTimer(0.3),"timeout");
            QueueFree();
        }
    }
    public async void OnBodyEntered(CharacterBody2D body)
    {
        if(body.Name == "Player")
        {
            _myCollision.Visible = false;   
            _spriteController.Play("death");
            _isDead = true;
            await ToSignal(GetTree().CreateTimer(0.3),"timeout");
            QueueFree();
        }
    }
}
