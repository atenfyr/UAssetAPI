using Newtonsoft.Json;
using UAssetAPI.JSON;

namespace UAssetAPI.UnrealTypes;

/// <summary>
/// Structure for three dimensional planes.
/// Stores the coeffecients as Xx+Yy+Zz=W.
/// This is different from many other Plane classes that use Xx+Yy+Zz+W=0.
/// </summary>
[JsonObject(MemberSerialization.OptIn)]
public struct FPlane 
{
    private float? _x1;
    private double _x2;
    private float? _y1;
    private double _y2;
    private float? _z1;
    private double _z2;
    private float? _w1;
    private double _w2;

    /// <summary>The plane's X-component.</summary>
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

    /// <summary>The plane's Y-component.</summary>
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

    /// <summary>The plane's Z-component.</summary>
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

    /// <summary>The plane's W-component.</summary>
    [JsonProperty]
    [JsonConverter(typeof(FSignedZeroJsonConverter))]
    public double W
    {
        get
        {
            return _w1 == null ? _w2 : (double)_w1;
        }
        set
        {
            _w1 = null;
            _w2 = value;
        }
    }

    [JsonIgnore]
    public float WFloat => _w1 == null ? (float)_w2 : (float)_w1;

    public FPlane(double x, double y, double z, double w)
    {
        _x1 = null; _y1 = null; _z1 = null; _w1 = null;
        _x2 = x;
        _y2 = y;
        _z2 = z;
        _w2 = w;
    }

    public FPlane(float x, float y, float z, float w)
    {
        _x2 = 0; _y2 = 0; _z2 = 0; _w2 = 0;
        _x1 = x;
        _y1 = y;
        _z1 = z;
        _w1 = w;
    }

    public FPlane(AssetBinaryReader reader)
    {
        if (reader.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            _x1 = null; _y1 = null; _z1 = null; _w1 = null;
            _x2 = reader.ReadDouble();
            _y2 = reader.ReadDouble();
            _z2 = reader.ReadDouble();
            _w2 = reader.ReadDouble();
        }
        else
        {
            _x2 = 0; _y2 = 0; _z2 = 0; _w2 = 0;
            _x1 = reader.ReadSingle();
            _y1 = reader.ReadSingle();
            _z1 = reader.ReadSingle();
            _w1 = reader.ReadSingle();
        }
    }

    public int Write(AssetBinaryWriter writer)
    {
        if (writer.Asset.ObjectVersionUE5 >= ObjectVersionUE5.LARGE_WORLD_COORDINATES)
        {
            writer.Write(_x2);
            writer.Write(_y2);
            writer.Write(_z2);
            writer.Write(_w2);
            return sizeof(double) * 4;
        }
        else
        {
            writer.Write(XFloat);
            writer.Write(YFloat);
            writer.Write(ZFloat);
            writer.Write(WFloat);
            return sizeof(float) * 4;
        }
    }
}
