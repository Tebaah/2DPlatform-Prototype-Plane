using Godot;
using System;

public partial class EnemyTank : Area2D
{
    public CharacterBody2D target;
    private AnimatedSprite2D _spriteController;
    private CollisionShape2D _collisionController;
    private bool _isDead = false;
    private float _rotation;
    [Export] public PackedScene bulletEnemy;
     private bool _canShoot = true;
     private Marker2D _spawnBulletEnemy;
    public override void _Ready()
    {
        target = (CharacterBody2D)GetParent().GetNode("Player");
        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collisionController = GetNode<CollisionShape2D>("CollisionShape2D");

        _spawnBulletEnemy = GetNode<Marker2D>("SpawnBullet");
    }

    public override void _PhysicsProcess(double delta)
    {
        Shoot();
        LookAtTarget();
        
    }

    public void LookAtTarget()
    {
        LookAt(target.Position);

    }

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
