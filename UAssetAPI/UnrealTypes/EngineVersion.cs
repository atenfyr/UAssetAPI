namespace UAssetAPI.UnrealTypes
{
    /// <summary>
    /// An enum used to represent all retail versions of the Unreal Engine. Each version entry both a particular <see cref="ObjectVersion"/> and the default set of all applicable <see cref="CustomVersion"/> enum values.
    /// </summary>
    public enum EngineVersion
    {
        UNKNOWN,
        VER_UE4_OLDEST_LOADABLE_PACKAGE,

        /// <summary>4.0</summary>
        VER_UE4_0,
        /// <summary>4.1</summary>
        VER_UE4_1,
        /// <summary>4.2</summary>
        VER_UE4_2,
        /// <summary>4.3</summary>
        VER_UE4_3,
        /// <summary>4.4</summary>
        VER_UE4_4,
        /// <summary>4.5</summary>
        VER_UE4_5,
        /// <summary>4.6</summary>
        VER_UE4_6,
        /// <summary>4.7</summary>
        VER_UE4_7,
        /// <summary>4.8</summary>
        VER_UE4_8,
        /// <summary>4.9</summary>
        VER_UE4_9,
        /// <summary>4.10</summary>
        VER_UE4_10,
        /// <summary>4.11</summary>
        VER_UE4_11,
        /// <summary>4.12</summary>
        VER_UE4_12,
        /// <summary>4.13</summary>
        VER_UE4_13,
        /// <summary>4.14</summary>
        VER_UE4_14,
        /// <summary>4.15</summary>
        VER_UE4_15,
        /// <summary>4.16</summary>
        VER_UE4_16,
        /// <summary>4.17</summary>
        VER_UE4_17,
        /// <summary>4.18</summary>
        VER_UE4_18,
        /// <summary>4.19</summary>
        VER_UE4_19,
        /// <summary>4.20</summary>
        VER_UE4_20,
        /// <summary>4.21</summary>
        VER_UE4_21,
        /// <summary>4.22</summary>
        VER_UE4_22,
        /// <summary>4.23</summary>
        VER_UE4_23,
        /// <summary>4.24</summary>
        VER_UE4_24,
        /// <summary>4.25</summary>
        VER_UE4_25,
        /// <summary>4.26</summary>
        VER_UE4_26,
        /// <summary>4.27</summary>
        VER_UE4_27,

        /// <summary>5.0</summary>
        VER_UE5_0,

        VER_UE4_AUTOMATIC_VERSION_PLUS_ONE,
        /// <summary>The newest specified version of the Unreal Engine.</summary>
        VER_UE4_AUTOMATIC_VERSION = VER_UE4_AUTOMATIC_VERSION_PLUS_ONE - 1,
    };
}
