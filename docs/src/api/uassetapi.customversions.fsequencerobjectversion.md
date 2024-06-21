# FSequencerObjectVersion

Namespace: UAssetAPI.CustomVersions

```csharp
public enum FSequencerObjectVersion
```

Inheritance [Object](https://docs.microsoft.com/en-us/dotnet/api/system.object) → [ValueType](https://docs.microsoft.com/en-us/dotnet/api/system.valuetype) → [Enum](https://docs.microsoft.com/en-us/dotnet/api/system.enum) → [FSequencerObjectVersion](./uassetapi.customversions.fsequencerobjectversion.md)<br>
Implements [IComparable](https://docs.microsoft.com/en-us/dotnet/api/system.icomparable), [ISpanFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.ispanformattable), [IFormattable](https://docs.microsoft.com/en-us/dotnet/api/system.iformattable), [IConvertible](https://docs.microsoft.com/en-us/dotnet/api/system.iconvertible)

## Fields

| Name | Value | Description |
| --- | --: | --- |
| BeforeCustomVersionWasAdded | 0 | Before any version changes were made |
| RenameMediaSourcePlatformPlayers | 1 | Per-platform overrides player overrides for media sources changed name and type. |
| ConvertEnableRootMotionToForceRootLock | 2 | Enable root motion isn't the right flag to use, but force root lock |
| ConvertMultipleRowsToTracks | 3 | Convert multiple rows to tracks |
| WhenFinishedDefaultsToRestoreState | 4 | When finished now defaults to restore state |
| EvaluationTree | 5 | EvaluationTree added |
| WhenFinishedDefaultsToProjectDefault | 6 | When finished now defaults to project default |
| FloatToIntConversion | 7 | Use int range rather than float range in FMovieSceneSegment |
| PurgeSpawnableBlueprints | 8 | Purged old spawnable blueprint classes from level sequence assets |
| FinishUMGEvaluation | 9 | Finish UMG evaluation on end |
| SerializeFloatChannel | 10 | Manual serialization of float channel |
| ModifyLinearKeysForOldInterp | 11 | Change the linear keys so they act the old way and interpolate always. |
| SerializeFloatChannelCompletely | 12 | Full Manual serialization of float channel |
| SpawnableImprovements | 13 | Set ContinuouslyRespawn to false by default, added FMovieSceneSpawnable::bNetAddressableName |
