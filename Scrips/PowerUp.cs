using Godot;
using System;

public partial class PowerUp : Area2D
{
    // Variable de movimiento
    [Export] public int speed;

    public override void _PhysicsProcess(double delta)
    {
        // Se mueve en el eje "y" segun la velocidad ingresada
        Position += new Vector2(0,speed);
        // Si sobre pasa la posisicion indicada se elimina
        if(Position.Y > 730)
        {
            QueueFree();
        }
    }

    public void OnBodyEntered(CharacterBody2D body)
    {
        // Si toca al jugador se elimina
        if(body.Name == "Player")
        {
            QueueFree();
        }
    }
}
