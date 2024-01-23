using Godot;
using System;

public partial class MenuController : Node
{
        public void OnPressed()
    {
        GetTree().ChangeSceneToFile("res://Scenes/Scenarios/main.tscn");
    }
}
