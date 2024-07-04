using Newtonsoft.Json;
using UAssetAPI.JSON;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// A vector in 2-D space composed of components (X, Y) with floating/double point precision.
/// </summary>
public struct FVector2D
{
    private float? _x1;
    private double _x2;
    private float? _y1;
    private double _y2;

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

    public FVector2D(double x, double y)
    {
        _x1 = null; _y1 = null;
        _x2 = x;
        _y2 = y;
    }

    public FVector2D(float x, float y, float z)
    {
        _x2 = 0; _y2 = 0;
        _x1 = x;
        _y1 = y;
    }

    public FVector2D(AssetBinaryReader reader)
    {
        if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            _x1 = null; _y1 = null;
            _x2 = reader.ReadDouble();
            _y2 = reader.ReadDouble();
        }
        else
        {
            _x2 = 0; _y2 = 0;
            _x1 = reader.ReadSingle();
            _y1 = reader.ReadSingle();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            writer.Write(X);
            writer.Write(Y);
            return sizeof(double) * 2;
        }
        else
        {
            writer.Write(XFloat);
            writer.Write(YFloat);
            return sizeof(float) * 2;
        }
    }
}
