using Godot;
using System;

public partial class EnemyController : Area2D
{
    // Variables
    private float _speed; // Velocidad de movimiento
    private AnimatedSprite2D _spriteController; // Controlador de animaciones
    [Export] public PackedScene bulletEnemy; // Municion de la nave
    private bool _canShoot = true; // Nos permite disparar o no 
    private Marker2D _spawnBulletEnemy; // Mira de la nave
    private bool _isDead = false; // Nos permite saber si esta viva o no 
    private CollisionShape2D _myCollision;
  
    // Methods
    public override void _Ready()
    {
        // Obtenemos un valor rando entre 1 y 3 para asiganar a la velocidad 
        var random = new RandomNumberGenerator();
        random.Randomize();
        _speed = random.RandiRange(1,3);

        // Inicializamos los componentes
        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _spawnBulletEnemy = GetNode<Marker2D>("SpawnBullet");
        _myCollision = GetNode<CollisionShape2D>("CollisionShape2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Con la velocidad asiganda movemos la nave en el eje "y"
        Position += new Vector2(0, _speed);

        // Destruimos la nave si sobrepasa los 720 pixeles
        if(Position.Y > 720)
        {
            QueueFree();
        }

        Shoot();
    }

    public async void OnBodyEnteredShip(Area2D area)
    {
        if(area.IsInGroup("Bullet"))
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
