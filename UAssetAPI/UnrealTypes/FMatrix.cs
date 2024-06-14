namespace UAssetAPI.UnrealTypes
{
    public struct FMatrix
    {
        public FVector4 Row1;
        public FVector4 Row2;
        public FVector4 Row3;
        public FVector4 Row4;

        public FMatrix(FVector4 row1, FVector4 row2, FVector4 row3, FVector4 row4)
        {
            Row1 = row1;
            Row2 = row2;
            Row3 = row3;
            Row4 = row4;
        }

        public FMatrix(AssetBinaryReader reader)
        {
            Row1 = new FVector4(reader);
            Row2 = new FVector4(reader);
            Row3 = new FVector4(reader);
            Row4 = new FVector4(reader);
        }

        public int Write(AssetBinaryWriter writer)
        {
            Row1.Write(writer);
            Row2.Write(writer);
            Row3.Write(writer);
            Row4.Write(writer);
            return 64;
        }
    }
}
