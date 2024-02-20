using Godot;
using System;

public partial class BulletTank : Area2D
{
    // Variable para obtener objetivo
    private CharacterBody2D _target;
    // Variable para direccion de bala
    private Vector2 _direction;

    public override void _Ready()
    {
        // Se inicializa la variable, se determina la posicion del objetivo 
        // y se asigna esa posicion a la bala, para determinar la direccion
        _target = (CharacterBody2D)GetParent().GetNode("/root/Main/Player");
        LookAt(_target.Position);
        _direction = Position.DirectionTo(_target.Position);
    }
    public override void _PhysicsProcess(double delta)
    {
        // Movimiento de la bala * 2 unidades (pixeles)          
        Position += _direction * 2;
        // Si sobrepasa el valor indicado se elimina
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
