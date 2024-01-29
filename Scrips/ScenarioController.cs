using Godot;
using System;

public partial class ScenarioController : TileMap
{
    [Export] public float speed;
    
    public override void _PhysicsProcess(double delta)
    {
        MoveMap(speed);
        if(Position.Y == 3600) // Al llegar a la posicion detiene el movimiento
        {
            speed = 0;
        }
    }

    private void MoveMap(float speedMove) // Mueve el mapa hacia abajo
    {
        Position += new Vector2(0 ,speedMove);
    }
}
