using Godot;

public partial class SpaceSignal
{
    public Vector3 _position;
    public AudioStream _sound;
    public bool _isDiscovered;
    public float _accuracyRequired;
    public DrawCircle3D _visualMarker;

    public SpaceSignal(Vector3 position, AudioStream sound, float accuracy = 0.995f)
    {
        _position = position;
        _sound = sound;
        _isDiscovered = false;
        _accuracyRequired = accuracy;
    }
}
