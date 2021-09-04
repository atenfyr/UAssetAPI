namespace UAssetAPI
{
    public class Import
    {
        public string Base;
        public string Class;
        public int Linkage;
        public string Property;
        public int Index;

        public Import(string bbase, string bclass, int link, string property, int index = 0)
        {
            Base = bbase;
            Class = bclass;
            Linkage = link;
            Property = property;
            Index = index;
        }

        public Import()
        {

        }
    }
}
