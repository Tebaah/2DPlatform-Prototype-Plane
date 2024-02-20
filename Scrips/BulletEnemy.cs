using Godot;
using System;

public partial class BulletEnemy : Area2D
{
    // Varible de movimiento de bala
    [Export] public float speed;

    public override void _PhysicsProcess(double delta)
    {
        // Movimiento de bala en eje y, depende de la variable "speed"
        Position += new Vector2(0,speed);
        // Si la bala sobrepasa el valor indicado se elimina
        if(Position.Y > 730)
        {
            QueueFree();
        }
    }

    public void OnBodyEntered(CharacterBody2D body)
    {
        // Si toca al jugador se elimina la bala
        if(body.Name == "Player")
        {
            QueueFree();
        }
    }
}
