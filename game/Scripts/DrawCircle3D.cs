using Godot;
using System;

public partial class DrawCircle3D : MeshInstance3D
{
	[Export] public float _radius = 0.5f;
	[Export] public int _segments = 64;
	[Export] public Color _cirlceColor = new Color(0.0f, 0.8f, 1.0f);

    public override void _Ready()
	{
		var _immediateMesh = new ImmediateMesh();
		Mesh = _immediateMesh;
		var _material = new StandardMaterial3D
		{
            ShadingMode = StandardMaterial3D.ShadingModeEnum.Unshaded,
            AlbedoColor = _cirlceColor,
            BillboardMode = StandardMaterial3D.BillboardModeEnum.Enabled
        };
        MaterialOverride = _material;
        _immediateMesh.SurfaceBegin(Mesh.PrimitiveType.Lines);
        for (int i = 0; i < _segments; i++)
        {
            float angle1 = (float)i / _segments * Mathf.Tau;
            float angle2 = (float)(i + 1) / _segments * Mathf.Tau;

            Vector3 vertex1 = new Vector3(Mathf.Cos(angle1) * _radius, Mathf.Sin(angle1) * _radius, 0);
            Vector3 vertex2 = new Vector3(Mathf.Cos(angle2) * _radius, Mathf.Sin(angle2) * _radius, 0);

            _immediateMesh.SurfaceAddVertex(vertex1);
            _immediateMesh.SurfaceAddVertex(vertex2);
        }
        _immediateMesh.SurfaceEnd();
    }
}
