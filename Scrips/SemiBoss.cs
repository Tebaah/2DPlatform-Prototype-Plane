using Godot;
using System;

public partial class SemiBoss : Area2D
{
    // Variables
    private AnimatedSprite2D _spriteController;
    private CollisionShape2D _myCollision;
    private bool _isDead = false;
    private Global _global;
    [Export]public PackedScene bulletEnemy;
    private bool _canShoot = true;
    private Marker2D _spawnBulletEnemy;
    private int _life = 5;

    // Methods

    public override void _Ready()
    {
        _myCollision = GetNode<CollisionShape2D>("CollisionShape2D");
        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _spawnBulletEnemy = GetNode<Marker2D>("SpawnBullet");
        _global = GetNode<Global>("/root/Global");
    }


    public override void _PhysicsProcess(double delta)
    {
        if(Position.Y < 300)
        {
            Position += new Vector2(0, 1);
        }

        Shoot();
    }

    public async void OnBodyEnteredShip(Area2D area)
    {
        if(area.IsInGroup("Bullet"))
        {
            _life -= 1;
            
            if(_life == 0)
            {
                _myCollision.Visible = false;   
                _spriteController.Play("death");

                _global.pointsPlayer += 10;
                
                _isDead = true;
                await ToSignal(GetTree().CreateTimer(0.3),"timeout");
                QueueFree();
            }

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

        public async void Shoot()
    {
        if(_isDead == false)
        {
            if(_canShoot == true)
            {
                Area2D newBulettEnemy = (Area2D)bulletEnemy.Instantiate();
                newBulettEnemy.GlobalPosition = _spawnBulletEnemy.GlobalPosition;
                GetParent().AddChild(newBulettEnemy);

                _canShoot = false;

                await ToSignal(GetTree().CreateTimer(2.0f), "timeout");
                _canShoot = true;
            }
        }
    }
}
