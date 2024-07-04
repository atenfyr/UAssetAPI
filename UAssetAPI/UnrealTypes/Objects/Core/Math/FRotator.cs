using Newtonsoft.Json;
using UAssetAPI.JSON;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Implements a container for rotation information.
/// All rotation values are stored in degrees.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public struct FRotator
{
    private float? _pitch1;
    private double _pitch2;
    private float? _yaw1;
    private double _yaw2;
    private float? _roll1;
    private double _roll2;

    /// <summary>Rotation around the right axis (around Y axis), Looking up and down (0=Straight Ahead, +Up, -Down)</summary>
    [JsonProperty]
    [JsonConverter(typeof(FSignedZeroJsonConverter))]
    public double Pitch
    {
        get
        {
            return _pitch1 == null ? _pitch2 : (double)_pitch1;
        }
        set
        {
            _pitch1 = null;
            _pitch2 = value;
        }
    }

    [JsonIgnore]
    public float PitchFloat => _pitch1 == null ? (float)_pitch2 : (float)_pitch1;

    /// <summary>Rotation around the up axis (around Z axis), Running in circles 0=East, +North, -South.</summary>
    [JsonProperty]
    [JsonConverter(typeof(FSignedZeroJsonConverter))]
    public double Yaw
    {
        get
        {
            return _yaw1 == null ? _yaw2 : (double)_yaw1;
        }
        set
        {
            _yaw1 = null;
            _yaw2 = value;
        }
    }

    [JsonIgnore]
    public float YawFloat => _yaw1 == null ? (float)_yaw2 : (float)_yaw1;

    /// <summary>Rotation around the forward axis (around X axis), Tilting your head, 0=Straight, +Clockwise, -CCW.</summary>
    [JsonProperty]
    [JsonConverter(typeof(FSignedZeroJsonConverter))]
    public double Roll
    {
        get
        {
            return _roll1 == null ? _roll2 : (double)_roll1;
        }
        set
        {
            _roll1 = null;
            _roll2 = value;
        }
    }

    [JsonIgnore]
    public float RollFloat => _roll1 == null ? (float)_roll2 : (float)_roll1;

    public FRotator(double pitch, double yaw, double roll)
    {
        _pitch1 = null; _yaw1 = null; _roll1 = null;
        _pitch2 = pitch;
        _yaw2 = yaw;
        _roll2 = roll;
    }

    public FRotator(float pitch, float yaw, float roll)
    {
        _pitch2 = 0; _yaw2 = 0; _roll2 = 0;
        _pitch1 = pitch;
        _yaw1 = yaw;
        _roll1 = roll;
    }

    public FRotator(AssetBinaryReader reader)
    {
        if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            _pitch1 = null; _yaw1 = null; _roll1 = null;
            _pitch2 = reader.ReadDouble();
            _yaw2 = reader.ReadDouble();
            _roll2 = reader.ReadDouble();
        }
        else
        {
            _pitch2 = 0; _yaw2 = 0; _roll2 = 0;
            _pitch1 = reader.ReadSingle();
            _yaw1 = reader.ReadSingle();
            _roll1 = reader.ReadSingle();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            writer.Write(Pitch);
            writer.Write(Yaw);
            writer.Write(Roll);
            return sizeof(double) * 3;
        }
        else
        {
            writer.Write(PitchFloat);
            writer.Write(YawFloat);
            writer.Write(RollFloat);
            return sizeof(float) * 3;
        }
    }
}
