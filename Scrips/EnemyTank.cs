using Godot;
using System;

public partial class EnemyTank : Area2D
{
    public CharacterBody2D target; // almacena al player como character body
    private PlayerController _target; // almacena al player como player controller
    private AnimatedSprite2D _spriteController; // almacena las animaciones
    private CollisionShape2D _collisionController; // almacena la colision 
    private bool _isDead = false; // para validar si esta vivo o muerto
    [Export] public PackedScene bulletEnemy; // almacena la municion
     private bool _canShoot = true; // para validar si puede disparar
     private Marker2D _spawnBulletEnemy; // punto de disparo del enemigo
     private float _distanceToEnemy; // almacena la distancia entre el enemigo y el jugador
     private Global _global;
    public override void _Ready()
    {
        // inicilizamos las variables de almacenamiento para su utilizacion 
        target = (CharacterBody2D)GetParent().GetNode("/root/Main/Player");
        _target = (PlayerController)GetParent().GetNode("/root/Main/Player");
        _spriteController = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        _collisionController = GetNode<CollisionShape2D>("CollisionShape2D");

        _spawnBulletEnemy = GetNode<Marker2D>("SpawnBullet");

        _global = GetNode<Global>("/root/Global");
    }

    public override void _PhysicsProcess(double delta)
    {
        // le damos un movimento al tank en direccion positivo eje y
        // TODO: se debe modificar para que el valor de velocidad sea tomado desde el script de tilemap
        Position += new Vector2(0, 0.5f);     

        // si el jugador esta vivo ejecutamos las funciones
        if(_target.isAlive == true)
        {
            Shoot();
            LookAtTarget();
        }        
    }

    public void LookAtTarget()
    {
        // el nodo (tank) apunta en direccion al jugador
        LookAt(target.Position);

    }

    public async void OnAreaEnteredTank(Area2D area)
    {
        // si toca una municion ejecuta el codigo
        if(area.IsInGroup("Bullet"))
        {
            // colision desaparece, cambia el sprite y esta muerto para a true            
            _collisionController.Visible = false;
            _spriteController.Play("death");

            _global.pointsPlayer += 10;

            _isDead = true;
            // se emite senal para destruir el nodo despues de 3 segundos
            await ToSignal(GetTree().CreateTimer(0.3),"timeout");
            QueueFree();
        }
    }

    public async void Shoot()
    {
        // calculamos la distancia entre el enemigo y el jugador para ser utilizado
        _distanceToEnemy = Position.DistanceTo(target.Position);

        // si no esta muerto y la distancia es menor a (TODO: determinar distancia) ejecuta el codigo
        if(_isDead == false && _distanceToEnemy <= 600)
        {
            // si puede disprar ejecuta el codigo
            if(_canShoot == true)
            {
                // se isntancia una nueva municion que se agrega al punto de disparo como hijo
                Area2D newBulettEnemy = (Area2D)bulletEnemy.Instantiate();
                newBulettEnemy.GlobalPosition = _spawnBulletEnemy.GlobalPosition;
                GetParent().AddChild(newBulettEnemy);

                // negamos un disparo inmediato pasan variable a false
                _canShoot = false;

                // emitimos una senal de 3 segundos para cambiar la variable a true y autorizar a disparar nuevamente 
                await ToSignal(GetTree().CreateTimer(2.0f), "timeout");
                _canShoot = true;
            }
        }
    }
}
