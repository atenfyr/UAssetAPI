using Newtonsoft.Json;
using UAssetAPI.JSON;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// A vector in 3-D space composed of components (X, Y, Z) with floating/double point precision.
/// </summary>
public struct FVector
{
    private float? _x1;
    private double _x2;
    private float? _y1;
    private double _y2;
    private float? _z1;
    private double _z2;

    /// <summary>The vector's X-component.</summary>
    [JsonProperty]
    [JsonConverter(typeof(FSignedZeroJsonConverter))]
    public double X
    {
        get
        {
            return _x1 == null ? _x2 : (double)_x1;
        }
        set
        {
            _x1 = null;
            _x2 = value;
        }
    }

    [JsonIgnore]
    public float XFloat => _x1 == null ? (float)_x2 : (float)_x1;

    /// <summary>The vector's Y-component.</summary>
    [JsonProperty]
    [JsonConverter(typeof(FSignedZeroJsonConverter))]
    public double Y
    {
        get
        {
            return _y1 == null ? _y2 : (double)_y1;
        }
        set
        {
            _y1 = null;
            _y2 = value;
        }
    }

    [JsonIgnore]
    public float YFloat => _y1 == null ? (float)_y2 : (float)_y1;

    /// <summary>The vector's Z-component.</summary>
    [JsonProperty]
    [JsonConverter(typeof(FSignedZeroJsonConverter))]
    public double Z
    {
        get
        {
            return _z1 == null ? _z2 : (double)_z1;
        }
        set
        {
            _z1 = null;
            _z2 = value;
        }
    }

    [JsonIgnore]
    public float ZFloat => _z1 == null ? (float)_z2 : (float)_z1;

    public FVector(double x, double y, double z)
    {
        _x1 = null; _y1 = null; _z1 = null;
        _x2 = x;
        _y2 = y;
        _z2 = z;
    }

    public FVector(float x, float y, float z)
    {
        _x2 = 0; _y2 = 0; _z2 = 0;
        _x1 = x;
        _y1 = y;
        _z1 = z;
    }

    public FVector(AssetBinaryReader reader)
    {
        if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            _x1 = null; _y1 = null; _z1 = null;
            _x2 = reader.ReadDouble();
            _y2 = reader.ReadDouble();
            _z2 = reader.ReadDouble();
        }
        else
        {
            _x2 = 0; _y2 = 0; _z2 = 0;
            _x1 = reader.ReadSingle();
            _y1 = reader.ReadSingle();
            _z1 = reader.ReadSingle();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            writer.Write(X);
            writer.Write(Y);
            writer.Write(Z);
            return sizeof(double) * 3;
        }
        else
        {
            writer.Write(XFloat);
            writer.Write(YFloat);
            writer.Write(ZFloat);
            return sizeof(float) * 3;
        }
    }
}
