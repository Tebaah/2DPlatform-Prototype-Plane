using Godot;
using System;

public partial class BulletController : Area2D
{
    // Varibles de movieminto de bala
    [Export] public float speed;
    public override void _PhysicsProcess(double delta)
    {
        // Movimiento de bala en eje y, depende de la variable "speed"
        Position -= new Vector2(0,speed);
        // si sobre pasa el valor indicado se elimina la bala
        if(Position.Y < -2)
        {
            QueueFree();
        }
    }
    public void OnBodyEntered(CharacterBody2D body)
    {
        // Si toca un enemigo se elimina la bala
        if(body.IsInGroup("Enemy"))
        {
            QueueFree();
        }
    }

    public void OnAreaEntered(Area2D area)
    {
        // Si toca un enemigo se elimina la bala
        if(area.IsInGroup("Enemy"))
        {
            QueueFree();
        }
    }
}
