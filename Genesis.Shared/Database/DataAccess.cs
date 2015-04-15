namespace Genesis.Shared.Database
{
    using Tables;

    public static class DataAccess
    {
        public static Account Account { get; private set; }
        public static Character Character { get; private set; }
        public static Clan Clan { get; private set; }
        public static Convoy Convoy { get; private set; }
        public static Item Item { get; private set; }
        public static Realmlist Realmlist { get; private set; }
        public static Social Social { get; private set; }
        public static Vehicle Vehicle { get; private set; }

        public static IDatabaseAccess DatabaseAccess { get; private set; }

        public static void Initialize(IDatabaseAccess access)
        {
            DatabaseAccess = access;

            Account = new Account();
            Character = new Character();
            Clan = new Clan();
            Convoy = new Convoy();
            Item = new Item();
            Realmlist = new Realmlist();
            Social = new Social();
            Vehicle = new Vehicle();
        }
    }
}
