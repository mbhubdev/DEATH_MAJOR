using Godot;
using System;

public partial class World3d : Node3D
{
    private Node3D PlanetPivot;
    private Node3D StarPivot;
    public float PlanetSpeed = 0.1f;
    public float StarSpeed = 0.01f;

    public override void _Ready()
	{
        PlanetPivot = GetNode<Node3D>("PlanetOrbitPivot");
        StarPivot = GetNode<Node3D>("StarOrbitPivot");
    }

	public override void _Process(double delta)
	{
        StarPivot.RotateY(StarSpeed * (float)delta);
        PlanetPivot.RotateY(PlanetSpeed * (float)delta);
    }
}
