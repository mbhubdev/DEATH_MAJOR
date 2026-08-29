using Godot;
using System;

public partial class Camera3d : Camera3D
{
    [Export] float MouseSensetivity = 0.002f;
    float MinPitch = Mathf.DegToRad(-89f);
    float MaxPitch = Mathf.DegToRad(89f);
    float rotationX = 0f;
    float rotationY = 0f;

    public override void _Ready()
	{
        Input.MouseMode = Input.MouseModeEnum.Captured;
	}

    public override void _Input(InputEvent @event)
    {
        if (@event.IsActionPressed("ui_cancel"))
        {
            if (Input.MouseMode == Input.MouseModeEnum.Captured)
            {
                Input.MouseMode = Input.MouseModeEnum.Visible;
            }
            else
            {
                Input.MouseMode = Input.MouseModeEnum.Captured;
            }
        }

        if (@event is InputEventMouseMotion MouseMotion && Input.MouseMode == Input.MouseModeEnum.Captured)
        {
            rotationX -= MouseMotion.Relative.Y * MouseSensetivity;
            rotationY -= MouseMotion.Relative.X * MouseSensetivity;
            rotationX = Mathf.Clamp(rotationX, MinPitch, MaxPitch);
            Transform = Transform3D.Identity;
            RotateObjectLocal(Vector3.Up, rotationY);
            RotateObjectLocal(Vector3.Right, rotationX);
        }
    }
}
