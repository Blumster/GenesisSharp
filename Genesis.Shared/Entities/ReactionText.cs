using System;
using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Structures;
    using Utils.Extensions;

    public class ReactionText
    {
        public String Main;
        public Byte TargetType;
        public List<TextChoice> TextChoices = new List<TextChoice>();
        public List<TextParam> TextParams = new List<TextParam>();

        public Byte Type;

        public ReactionText(BinaryReader br, UInt32 mapVersion)
        {
            Type = br.ReadByte();
            TargetType = br.ReadByte();
            Main = br.ReadLengthedString();

            var numParams = br.ReadUInt32();
            for (var i = 0U; i < numParams; ++i)
                TextParams.Add(TextParam.Read(br, mapVersion));

            var numChoices = br.ReadUInt32();
            for (var i = 0U; i < numChoices; ++i)
            {
                var tChoice = new TextChoice
                {
                    TriggerCOID = br.ReadUInt64(),
                    Text = br.ReadLengthedString(),
                    TextParams = new List<TextParam>()
                };

                var numChoiceParams = br.ReadUInt32();
                for (var j = 0U; j < numChoiceParams; ++j)
                    tChoice.TextParams.Add(TextParam.Read(br, mapVersion));
            }
        }
    }
}
