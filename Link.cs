namespace UAssetAPI
{
    public class Link
    {
        public ulong Base;
        public ulong Class;
        public int Linkage;
        public ulong Property;
        public int Index;

        public Link(string bbase, string bclass, int link, string property, AssetReader asset, int index = 0)
        {
            Base = (ulong)asset.SearchHeaderReference(bbase);
            Class = (ulong)asset.SearchHeaderReference(bclass);
            Linkage = link;
            Property = (ulong)asset.SearchHeaderReference(property);
            Index = index;
        }

        public Link(ulong bbase, ulong bclass, int link, ulong property, int index = 0)
        {
            Base = bbase;
            Class = bclass;
            Linkage = link;
            Property = property;
            Index = index;
        }

        public Link()
        {

        }
    }
}
