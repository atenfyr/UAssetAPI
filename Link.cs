namespace UAssetAPI
{
    public class Link
    {
        public long bbase;
        public long bclass;
        public int link;
        public long property;

        public Link(long bbase, long bclass, int link, long property)
        {
            this.bbase = bbase;
            this.bclass = bclass;
            this.link = link;
            this.property = property;
        }

        public Link()
        {

        }
    }
}
