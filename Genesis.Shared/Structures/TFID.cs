using System;
using System.IO;

namespace Genesis.Shared
{
    public class TFID
    {
        public Int64 Coid { get; set; }
        public Boolean Global { get; set; }

        public TFID()
        {
            Coid = -1L;
            Global = false;
        }

        public TFID(Int64 coid, Boolean global)
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

        public override Boolean Equals(Object obj)
        {
            if (!(obj is TFID))
                return false;

            var other = obj as TFID;

            return Coid == other.Coid && Global == other.Global;
        }

        public override Int32 GetHashCode()
        {
            return 37 * Coid.GetHashCode() + Global.GetHashCode();
        }

        public static Boolean operator ==(TFID a, TFID b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Coid == b.Coid && a.Global == b.Global;
        }

        public static Boolean operator !=(TFID a, TFID b)
        {
            return ReferenceEquals(a, null) || ReferenceEquals(b, null) || a.Coid != b.Coid || a.Global != b.Global;
        }

        public static Boolean operator <(TFID a, TFID b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Coid < b.Coid;
        }

        public static Boolean operator >(TFID a, TFID b)
        {
            return !ReferenceEquals(a, null) && !ReferenceEquals(b, null) && a.Coid > b.Coid;
        }
    }
}
