using Godot;
using System;


public partial class PlayerController : CharacterBody2D
{
    // Variables
    [Export] public int speed; // Velocidad de la nave
    public Vector2 screenSize; // Guardar dimensiones de la pantalla
    [Export] public PackedScene bullet; // Para instanciar la bala
    private bool _canShoot = true; // Para el temporizador de la bal
    private Marker2D _spawnBullet; // Nos permite disparar desde una ubicacion
    public bool isAlive = true;

    // Methods

    public override void _Ready()
    {
        screenSize = GetViewportRect().Size;
        _spawnBullet = GetNode<Marker2D>("SpawnBullet");
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
                CharacterBody2D newBullet = (CharacterBody2D)bullet.Instantiate();
                newBullet.GlobalPosition = _spawnBullet.GlobalPosition;
                GetParent().AddChild(newBullet);
                
                _canShoot = false;

                // Espera 0.5 seg para siguiente disparo
                await ToSignal(GetTree().CreateTimer(0.2), "timeout");
                _canShoot = true;
            }
        }

    }

    public void OnBodyEntered(CharacterBody2D body)
    {
        if(body.IsInGroup("Enemy") || body.IsInGroup("BulletEnemy"))
        {
            isAlive = false;
            QueueFree();
        }
    }

}
