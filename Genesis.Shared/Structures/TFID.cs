using System.IO;

namespace Genesis.Shared
{
    public class TFID
    {
        public long Coid { get; set; }
        public bool Global { get; set; }

        public TFID()
        {
            Coid = -1L;
            Global = false;
        }

        public TFID(long coid, bool global)
        {
            Coid = coid;
            Global = global;
        }

        public static TFID Read(BinaryReader br)
        {
            return new TFID
            {
                Global = br.ReadByte() != 0,
                Coid = br.ReadUInt32()
            };
        }

        public override bool Equals(object obj)
        {
            if (!(obj is TFID))
                return false;

            var other = obj as TFID;

            return Coid == other.Coid && Global == other.Global;
        }

        public override int GetHashCode()
        {
            return 37 * Coid.GetHashCode() + Global.GetHashCode();
        }

        public static bool operator ==(TFID a, TFID b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Coid == b.Coid && a.Global == b.Global;
        }

        public static bool operator !=(TFID a, TFID b)
        {
            return ReferenceEquals(a, null) || ReferenceEquals(b, null) || a.Coid != b.Coid || a.Global != b.Global;
        }

        public static bool operator <(TFID a, TFID b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Coid < b.Coid;
        }

        public static bool operator >(TFID a, TFID b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Coid > b.Coid;
        }
    }
}
