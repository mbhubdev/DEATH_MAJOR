using Godot;
using System;

public partial class World3d : Node3D
{
    private Node3D _planetPivot;
    private Node3D _starPivot;
    public float PlanetSpeed = 0.5f;
    public float StarSpeed = 0.1f;

    public override void _Ready()
	{
        _planetPivot = GetNode<Node3D>("PlanetOrbitPivot");
        _starPivot = GetNode<Node3D>("StarOrbitPivot");
    }

	public override void _Process(double delta)
	{
		
	}
}
