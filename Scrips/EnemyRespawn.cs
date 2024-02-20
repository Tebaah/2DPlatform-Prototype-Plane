using Godot;
using System;

public partial class EnemyRespawn : Node
{
    // Variable para objeto 
    [Export] public PackedScene enemy;

    // Variables para creacion de enemigos
    [Export] public int timeRespawn;
    private Marker2D _spawnEnemy;
    private bool _canSpawn = true;
    private float _pointX;

    // Variable para obtener objetivo
    private PlayerController _target;
   

    public override void _Ready()
    {
        // Inicializamos los nodos
        _spawnEnemy = GetNode<Marker2D>("SpawnEnemy");
        _target = (PlayerController)GetParent().GetNode("Player");

    }

    public override void _PhysicsProcess(double delta)
    {
        // Obtenemos un numero al azar enre 25 y 455 y se almacena en la variable
        var random = new RandomNumberGenerator();
        random.Randomize();
        _pointX = random.RandiRange(25,455);

        // Utilizamos el numero para indicar la posicion en "x" donde aparacere el enemigo
        _spawnEnemy.Position = new Vector2(_pointX, 0);
        
        // Se crean enemigos mientras el jugador esta vivo
        if(_target.isAlive == true)
        {
            // Ejecutamos funcion
            SpawnEnemies();
        }        
    }

    public async void SpawnEnemies()
    {
        // Si puede disparar hara ...        
        if(_canSpawn == true)
        {
            // ... creara un nuevo enemigo (isntancia)
            Area2D newEnemy = (Area2D)enemy.Instantiate();
            newEnemy.GlobalPosition = _spawnEnemy.GlobalPosition;
            GetParent().AddChild(newEnemy);

            // Se cambia variable para dejar de crear enemigos
            _canSpawn = false;

            // Luego de "x" segundos podrar crear nuevos enemigos
            await ToSignal(GetTree().CreateTimer(timeRespawn), "timeout");
            _canSpawn = true;
        }
    }
}
