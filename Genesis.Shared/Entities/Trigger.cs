using System.Collections.Generic;
using System.IO;

namespace Genesis.Shared.Entities
{
    using Base;
    using Structures;
    using Utils.Extensions;

    public class Trigger : GraphicsPhysicsBase
    {
        public float ActivateDelay;
        public int ActivationCount;
        public bool AllConditionNeeded;
        public bool ApplyToAllColliders;
        public uint Color;
        public bool DoCollision;
        public bool DoConditionals;
        public bool DoOnActivate;
        public Vector4 Location;
        public string Name;
        public Vector4 Quaternion;
        public float Radius;
        public List<ulong> Reactions = new List<ulong>();
        public float RetriggerDelay;
        public bool ShowMapTransitionDecals;
        public byte TargetType;
        public List<TFID> Targets = new List<TFID>();
        public List<TriggerConditional> TriggerConditionals = new List<TriggerConditional>();
        public uint TriggerId;

        public override void Unserialize(BinaryReader br, uint mapVersion)
        {
            Location = Vector4.Read(br);
            Quaternion = Vector4.Read(br);
            Radius = br.ReadSingle();
            Name = br.ReadUtf8StringOn(64);
            RetriggerDelay = br.ReadSingle();
            ActivateDelay = br.ReadSingle();
            ActivationCount = br.ReadInt32();

            TargetType = br.ReadByte();
            DoCollision = br.ReadBoolean();
            DoConditionals = br.ReadBoolean();

            if (mapVersion >= 44)
                ShowMapTransitionDecals = br.ReadBoolean();

            DoOnActivate = br.ReadBoolean();
            AllConditionNeeded = br.ReadBoolean();

            if (mapVersion >= 60)
                ApplyToAllColliders = br.ReadBoolean();
            else
                ApplyToAllColliders = DoCollision && RetriggerDelay <= 0.0f;

            var numReac = br.ReadUInt32();
            for (var i = 0U; i < numReac; ++i)
                Reactions.Add(br.ReadUInt32());

            var numTarget = br.ReadUInt32();
            for (var i = 0U; i < numTarget; ++i)
                Targets.Add(TFID.Read(br));

            var numConditials = br.ReadUInt32();
            for (var i = 0U; i < numConditials; ++i)
                TriggerConditionals.Add(TriggerConditional.Read(br));

            if (mapVersion >= 9)
                Color = br.ReadUInt32();

            if (mapVersion >= 55)
                TriggerId = br.ReadUInt32();
        }
    }
}
