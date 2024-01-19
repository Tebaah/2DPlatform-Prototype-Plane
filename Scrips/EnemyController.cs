using Godot;
using System;

public partial class EnemyController : CharacterBody2D
{
    private float _speed;
    private AnimatedSprite2D _spriteController;
    [Export] public PackedScene bulletEnemy;
    private bool _canShoot = true;
    private Marker2D _spawnBulletEnemy;
    private bool _isDead = false;
    public override void _Ready()
    {
        var random = new RandomNumberGenerator();
        random.Randomize();
        _speed = random.RandiRange(1,3);

        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _spawnBulletEnemy = GetNode<Marker2D>("SpawnBullet");
    }

    public override void _PhysicsProcess(double delta)
    {
        Position += new Vector2(0, _speed);
        if(Position.Y > 720)
        {
            QueueFree();
        }
        Shoot();
    }

    public async void OnBodyEnteredShip(CharacterBody2D body)
    {
        if(body.IsInGroup("Bullet"))
        {
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
                CharacterBody2D newBulettEnemy = (CharacterBody2D)bulletEnemy.Instantiate();
                newBulettEnemy.GlobalPosition = _spawnBulletEnemy.GlobalPosition;
                GetParent().AddChild(newBulettEnemy);

                _canShoot = false;

                await ToSignal(GetTree().CreateTimer(2.0f), "timeout");
                _canShoot = true;
            }
        }
    }

}
