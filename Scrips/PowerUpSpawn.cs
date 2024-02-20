using Godot;
using System;

public partial class PowerUpSpawn : Node
{
    // Variable 
    [Export] public PackedScene powerUp;

    // Variable de creacion objeto
    private Marker2D _spawnPowerUp;
    private bool _canSpawn = true;
    private float _pointX;
    [Export] public int timeSpawn;

    // Methods

    public override void _Ready()
    {
        // Inicializa el marcador
        _spawnPowerUp = GetNode<Marker2D>("SpawnPowerUp");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Se crea un numero random entre 25 y 455 que se almacena en variable
        var random = new RandomNumberGenerator();
        random.Randomize();
        _pointX = random.RandiRange(25, 455);

        // Con el numero almacenado se determina el punto en "x" donde se creara el objeto
        _spawnPowerUp.Position = new Vector2(_pointX, 0);

        // Se ejecuta la funcion
        SpawnPowerUps(); 
    }

    public async void SpawnPowerUps()
    {
        // Se evalua si puede crear elementos
        if(_canSpawn == true)
        {
            // Crea un nuevo elemento 
            Area2D newPowerUp = (Area2D)powerUp.Instantiate();
            newPowerUp.GlobalPosition = _spawnPowerUp.GlobalPosition;
            GetParent().AddChild(newPowerUp);

            // Se cambia la variable para detener la creacion 
            _canSpawn = false;

            // Despues de "x" segundos se cambia la variable para poder crear elementos nuevos
            await ToSignal(GetTree().CreateTimer(timeSpawn), "timeout");
            _canSpawn = true;
        }
    }
}
