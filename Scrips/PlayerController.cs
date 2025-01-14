using Godot;
using System;

public partial class PlayerController : CharacterBody2D
{
     // Variables
    [Export] public int speed; // Velocidad de la nave
    public Vector2 screenSize; // Guardar dimensiones de la pantalla
    [Export] public PackedScene[] bullet; // Para instanciar la bala
    private bool _canShoot = true; // Para el temporizador de la bal
    private Marker2D _spawnBullet; // Nos permite disparar desde una ubicacion
    public bool isAlive = true; // almacenar estado vivo o muerto (true o false)
    private int _indexBullet = 0; // contador para determinar el arma 
    private Global _global;
    private AudioStreamPlayer2D _audio;

    // Methods

    public override void _Ready()
    {
        screenSize = GetViewportRect().Size;
        _spawnBullet = GetNode<Marker2D>("SpawnBullet");
        _audio = GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D");

        _global = GetNode<Global>("/root/Global");
    }

    public override void _PhysicsProcess(double delta)
    {
        GetInput();
        MoveAndSlide();
        Shoot();

    }

    public void GetInput()
    {
        // Obtenemos la direccion del movimiento
        Vector2 inputDirection = Input.GetVector("left","right","up","down"); 

        // Aplicamos la velocidad
        Velocity = inputDirection * speed; 

        // Nos movemos al nuevo vector limitando con el borde de la pantalla
        Position = new Vector2(
            x: Mathf.Clamp(Position.X, 0, screenSize.X),
            y: Mathf.Clamp(Position.Y, 0, screenSize.Y));

    }

    public async void Shoot()
    {
        if(Input.IsActionJustPressed("space"))
        {
            if(_canShoot == true)
            {
                // Instancionamos la bala, le damos la posiciion y se agrega como hijo 
                Area2D newBullet = (Area2D)bullet[_indexBullet].Instantiate();
                newBullet.GlobalPosition = _spawnBullet.GlobalPosition;
                GetParent().AddChild(newBullet);
                _audio.Play();
                
                _canShoot = false;

                // Espera 0.5 seg para siguiente disparo
                await ToSignal(GetTree().CreateTimer(0.2), "timeout");
                _canShoot = true;
            }
        }

    }

    public async void OnAreaEntered(Area2D area)
    {
        if(area.IsInGroup("Enemy") || area.IsInGroup("BulletEnemy"))
        {
            _global.lifePlayer -= 1;
            
            if(_global.lifePlayer == 0)
            {
                isAlive = false;
                QueueFree();
            }
        }

        if(area.IsInGroup("PowerUp"))
        {
            _indexBullet = 1;

            await ToSignal(GetTree().CreateTimer(10), "timeout");
            _indexBullet = 0;
        }
    }

}
