using Godot;
using System;

public partial class ScenarioController : TileMap
{
    [Export] public float speed;
    private Global _global;

    public override void _Ready()
    {
        _global = GetNode<Global>("/root/Global");
    }

    public override void _PhysicsProcess(double delta)
    {
        MoveMap(speed);
        if(Position.Y == 3600) // Al llegar a la posicion detiene el movimiento
        {
            speed = 0;
        }

        _global.positionScenarioY = Position.Y;

    }

    private void MoveMap(float speedMove) // Mueve el mapa hacia abajo
    {
        Position += new Vector2(0 ,speedMove);
    }
}
