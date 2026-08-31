using Godot;
using System;
using System.Collections.Generic;

public partial class World3d : Node3D
{
    [Export] public Texture2D _markerTexture;
    [Export] public int _totalSignals = 5;
    [Export] public float _spawnRadius = 10;
    [Export] public AudioStream _defaultSignalSound;
    private List<SpaceSignal> _activeSignals = new();
    private AudioStreamPlayer3D _audioPlayer;
    private Camera3D _camera;
    private Node3D _planetPivot;
    private Node3D _starPivot;
    public float _planetSpeed = 0.1f;
    public float _starSpeed = 0.001f;

    public override void _Ready()
	{
        _planetPivot = GetNode<Node3D>("PlanetOrbitPivot");
        _starPivot = GetNode<Node3D>("StarOrbitPivot");
        _camera = GetNode<Camera3D>("Camera3D");
        _audioPlayer = GetNode<AudioStreamPlayer3D>("SignalAudioPlayer");

        GenerateRandomSignals();
    }

	public override void _Process(double delta)
	{
        _starPivot.RotateY(_starSpeed * (float)delta);
        _planetPivot.RotateY(_planetSpeed * (float)delta);

        CheckSignalsUnderCrosshair();
    }

    public void GenerateRandomSignals()
    {
        var _random = new Random();

        for (int i = 0; i < _totalSignals; i++)
        {
            float u = (float)_random.NextDouble();
            float v = (float)_random.NextDouble();
            float _theta = u * 2.0f * Mathf.Pi;
            float _phi = Mathf.Acos(2.0f * v - 1.0f);
            float x = Mathf.Sin(_phi) * Mathf.Cos(_theta);
            float y = Mathf.Sin(_phi) * Mathf.Sin(_theta);
            float z = Mathf.Cos(_phi);
            Vector3 _randomDirection = new Vector3(x, y, z).Normalized();
            Vector3 _spawnPositon = _randomDirection * _spawnRadius;
            SpaceSignal _newSignal = new SpaceSignal(_spawnPositon, _defaultSignalSound, 0.995f);
            Sprite3D _newMarker = new();
            _newMarker.Texture = _markerTexture;
            _newMarker.Billboard = BaseMaterial3D.BillboardModeEnum.Enabled;
            _newMarker.Modulate = new Color(1, 1, 1, 0.6f);
            AddChild(_newMarker);
            _newMarker.GlobalPosition = _spawnPositon;
            _newSignal._visualMarker = _newMarker;
            _activeSignals.Add(_newSignal);
        }
    }

    public void CheckSignalsUnderCrosshair()
    {
        if (_camera == null || _audioPlayer == null) return;
        Vector3 _cameraDirection = -_camera.GlobalTransform.Basis.Z.Normalized();
        SpaceSignal _bestSignal = null;
        float _highestDot = -1.0f;
        foreach (var _signal in _activeSignals)
        {
            if (_signal._isDiscovered) continue;
            Vector3 _directionToSignal = (_signal._position - _camera.GlobalPosition).Normalized();
            float _dotResult = _cameraDirection.Dot(_directionToSignal);
            if (_dotResult > _signal._accuracyRequired && _dotResult > _highestDot)
            {
                _highestDot = _dotResult;
                _bestSignal = _signal;
            }
        }
        if (_bestSignal != null)
        {
            _audioPlayer.GlobalPosition = _bestSignal._position;
            if (!_audioPlayer.Playing)
            {
                _audioPlayer.Stream = _bestSignal._sound;
                _audioPlayer.Play();
            }
        }
        else
        {
            if (_audioPlayer.Playing)
            {
                _audioPlayer.Stop();
            }
        }
    }
}
