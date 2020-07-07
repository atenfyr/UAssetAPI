namespace UAssetAPI
{
    public class CategoryReference
    {
        public int connection;
        public int connect;
        public int category;
        public int link;
        public int typeIndex;
        public int type;
        public int lengthV;
        public int startV;

        public int garbage1;
        public int garbage2;
        public byte[] garbage3;

        public CategoryReference(int connection, int connect, int category, int link, int typeIndex, int type, int lengthV, int startV, int garbage1, int garbage2, byte[] garbage3)
        {
            this.connection = connection;
            this.connect = connect;
            this.category = category;
            this.link = link;
            this.typeIndex = typeIndex;
            this.type = type;
            this.lengthV = lengthV;
            this.startV = startV;

            this.garbage1 = garbage1;
            this.garbage2 = garbage2;
            this.garbage3 = garbage3;
        }

        public CategoryReference(int connection, int connect, int category, int link, int typeIndex, int type, int lengthV, int startV)
        {
            this.connection = connection;
            this.connect = connect;
            this.category = category;
            this.link = link;
            this.typeIndex = typeIndex;
            this.type = type;
            this.lengthV = lengthV;
            this.startV = startV;
        }

        public CategoryReference()
        {

        }
    }
}
