namespace UAssetAPI
{
    public class ExportReference
    {
        public int connection;
        public int connect;
        public int category;
        public int link;
        public int typeIndex;
        public ushort type;
        public int lengthV;
        public int startV;

        public int garbage1;
        public int garbage2;
        public ushort garbageNew;
        public byte[] garbage3;

        public ExportReference(int connection, int connect, int category, int link, int typeIndex, ushort type, int lengthV, int startV, int garbage1, int garbage2, ushort garbageNew, byte[] garbage3)
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
            this.garbageNew = garbageNew;
            this.garbage3 = garbage3;
        }

        public ExportReference(ExportReference refer)
        {
            this.connection = refer.connection;
            this.connect = refer.connect;
            this.category = refer.category;
            this.link = refer.link;
            this.typeIndex = refer.typeIndex;
            this.type = refer.type;
            this.lengthV = refer.lengthV;
            this.startV = refer.startV;

            this.garbage1 = refer.garbage1;
            this.garbage2 = refer.garbage2;
            this.garbageNew = refer.garbageNew;
            this.garbage3 = refer.garbage3;
        }

        public ExportReference(int connection, int connect, int category, int link, int typeIndex, ushort type, int lengthV, int startV)
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

        public ExportReference()
        {

        }
    }
}
