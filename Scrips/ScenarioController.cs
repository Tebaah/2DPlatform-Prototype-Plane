using Godot;
using System;

public partial class ScenarioController : TileMap
{
    // Variable movimiento mapa
    [Export] public float speed;

    // Variable global
    private Global _global;

    public override void _Ready()
    {
        // Se inicializa la variable
        _global = GetNode<Global>("/root/Global");
    }

    public override void _PhysicsProcess(double delta)
    {
        // Se ejecuta la funcion
        MoveMap(speed);

        // Al llegar al pixel 3600 deja de moverse
        if(Position.Y == 3600) 
        {
            speed = 0;
        }

        // Almacena el valor en la variable global para ser utilizada por otras clases
        _global.positionScenarioY = Position.Y;

    }

    private void MoveMap(float speedMove) // Mueve el mapa hacia abajo
    {
        // Se mueve el mapa segun la velocidad ingresada
        Position += new Vector2(0 ,speedMove);
    }
}
