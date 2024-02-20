using Godot;
using System;

public partial class EnemyController : Area2D
{
    // Variables de movimiento
    private float _speed;

    // Variables de control de nodos hijos
    private AnimatedSprite2D _spriteController; // Nodo de animacinoes
    private CollisionShape2D _myCollision; // Nodo de colision
    private Marker2D _spawnBulletEnemy; // Nodo de marcador 
    private AudioStreamPlayer2D _audio; // Nodo de audio

    // Variable para instanciar
    [Export] public PackedScene bulletEnemy; // Municion de la nave

    // Variables de control
    private bool _canShoot = true; // Nos permite disparar o no 
    private bool _isDead = false; // Nos permite saber si esta viva o no 

    // Variables globales
    private Global _global; // Nos permite almacenar elementos generales a lo largo del juego
  
    // Methods
    public override void _Ready()
    {
        // Obtenemos un valor rando entre 1 y 3 para asiganar a la velocidad de la nave enemiga
        var random = new RandomNumberGenerator();
        random.Randomize();
        _speed = random.RandiRange(1,3);

        // Inicializamos los componentes
        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _spawnBulletEnemy = GetNode<Marker2D>("SpawnBullet");
        _myCollision = GetNode<CollisionShape2D>("CollisionShape2D");
        _audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

        _global = GetNode<Global>("/root/Global");

    }

    public override void _PhysicsProcess(double delta)
    {
        // Con la velocidad asignada movemos la nave en el eje "y"
        Position += new Vector2(0, _speed);

        // Destruimos la nave si sobrepasa los 720 pixeles
        if(Position.Y > 720)
        {
            QueueFree();
        }
        // Ejecutamos la funcion de disparo
        Shoot();
    }

    public async void OnBodyEnteredShip(Area2D area)
    {
        // Si es tocado por una bala
        if(area.IsInGroup("Bullet"))
        {
            // Se desaparece colision y se ejecuta animacion de muerte
            _myCollision.Visible = false;   
            _spriteController.Play("death");

            // Aumenta en 5 puntos la variable global
            _global.pointsPlayer += 5;
            
            // Pasa a estado verdadero variable muerte y luego de 3 segundos se elimina
            _isDead = true;
            await ToSignal(GetTree().CreateTimer(0.3),"timeout");
            QueueFree();
        }
    }
    public async void OnBodyEntered(CharacterBody2D body)
    {
        // Si es tocado por el jugador
        if(body.Name == "Player")
        {
            // Se desaparece colision y se ejecuta animacion de muerte
            _myCollision.Visible = false;   
            _spriteController.Play("death");

            // Pasa a estado verdadero variable muerte y luego de 3 segundos se elimina
            _isDead = true;
            await ToSignal(GetTree().CreateTimer(0.3),"timeout");
            QueueFree();
        }
    }

    public async void Shoot()
    {
        // Si esta muerto no puede disparar
        if(_isDead == false)
        {
            // Si puede disparar hara ...
            if(_canShoot == true)
            {
                // Creara una nueva bala (instanciada)
                Area2D newBulettEnemy = (Area2D)bulletEnemy.Instantiate();
                newBulettEnemy.GlobalPosition = _spawnBulletEnemy.GlobalPosition;
                GetParent().AddChild(newBulettEnemy);

                // Emitira sonido de disparo
                _audio.Play();

                // Cambiara de estado variable de disparo, sin poder disparar
                _canShoot = false;
                
                // Luego de 2 segundo podra nuevamente dispara
                await ToSignal(GetTree().CreateTimer(2.0f), "timeout");
                _canShoot = true;
            }
        }
    }

}
