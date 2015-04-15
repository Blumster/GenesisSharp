using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Clonebase
{
    using Structures;
    using Structures.Specifics;

    public class CloneBaseCharacter : CloneBaseCreature
    {
        public CharacterSpecific CharacterSpecific;
        public List<HeadBody> HashBody;
        public List<HeadDetail> HashEyes;
        public List<HeadDetail> HashHair;
        public List<HeadBody> HashHead;
        public List<HeadDetail> HashHeadDetail;
        public List<HeadDetail> HashHeadDetail2;
        public List<HeadDetail> HashHelmet;
        public List<HeadDetail> HashMouthes;

        public CloneBaseCharacter(BinaryReader br)
            : base(br)
        {
            CharacterSpecific = CharacterSpecific.Read(br);

            HashHead = new List<HeadBody>(br.ReadInt32());
            for (var i = 0; i < HashHead.Capacity; ++i)
                HashHead.Add(HeadBody.Read(br));

            HashBody = new List<HeadBody>(br.ReadInt32());
            for (var i = 0; i < HashBody.Capacity; ++i)
                HashBody.Add(HeadBody.Read(br));

            HashHeadDetail = new List<HeadDetail>(br.ReadInt32());
            for (var i = 0; i < HashHeadDetail.Capacity; ++i)
                HashHeadDetail.Add(HeadDetail.Read(br));

            HashHeadDetail2 = new List<HeadDetail>(br.ReadInt32());
            for (var i = 0; i < HashHeadDetail2.Capacity; ++i)
                HashHeadDetail2.Add(HeadDetail.Read(br));

            HashHair = new List<HeadDetail>(br.ReadInt32());
            for (var i = 0; i < HashHair.Capacity; ++i)
                HashHair.Add(HeadDetail.Read(br));

            HashEyes = new List<HeadDetail>(br.ReadInt32());
            for (var i = 0; i < HashEyes.Capacity; ++i)
                HashEyes.Add(HeadDetail.Read(br));

            HashHelmet = new List<HeadDetail>(br.ReadInt32());
            for (var i = 0; i < HashHelmet.Capacity; ++i)
                HashHelmet.Add(HeadDetail.Read(br));

            HashMouthes = new List<HeadDetail>(br.ReadInt32());
            for (var i = 0; i < HashMouthes.Capacity; ++i)
                HashMouthes.Add(HeadDetail.Read(br));
        }
    }
}
