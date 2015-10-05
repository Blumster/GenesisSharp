using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using TNL.NET.Data;
using TNL.NET.Entities;
using TNL.NET.Structs;
using TNL.NET.Types;
using TNL.NET.Utils;

namespace Genesis.Shared.TNL
{
    using Constant;
    using Database;
    using Entities;
    using Manager;
    using Utils;

    public partial class TNLConnection : GhostConnection
    {
        private static NetClassRepInstance<TNLConnection> _dynClassRep;
        private static NetConnectionRep _connRep;

        private uint _key;
        private uint _oneTimeKey;
        private long _playerCOID;
        private ushort _fragmentCounter;

        public uint[] FirstTimeFlags { get; private set; }
        public ulong AccountId { get; private set; }
        public byte AccountLevel { get; private set; }
        public string AccountName { get; private set; }
        public Character CurrentCharacter { get; set; }

        private readonly SFragmentData _fragmentGuaranteed;
        private readonly SFragmentData _fragmentNonGuaranteed;
        private readonly SFragmentData _fragmentGuaranteedOrdered;

        public new static void RegisterNetClassReps()
        {
            ImplementNetConnection(out _dynClassRep, out _connRep, true);

            NetEvent.ImplementNetEvent(out RPCMsgGuaranteed.DynClassRep,                  "RPC_TNLConnection_rpcMsgGuaranteed",                  NetClassMask.NetClassGroupGameMask, 0);
            NetEvent.ImplementNetEvent(out RPCMsgGuaranteedOrdered.DynClassRep,           "RPC_TNLConnection_rpcMsgGuaranteedOrdered",           NetClassMask.NetClassGroupGameMask, 0);
            NetEvent.ImplementNetEvent(out RPCMsgNonGuaranteed.DynClassRep,               "RPC_TNLConnection_rpcMsgNonGuaranteed",               NetClassMask.NetClassGroupGameMask, 0);
            NetEvent.ImplementNetEvent(out RPCMsgGuaranteedFragmented.DynClassRep,        "RPC_TNLConnection_rpcMsgGuaranteedFragmented",        NetClassMask.NetClassGroupGameMask, 0);
            NetEvent.ImplementNetEvent(out RPCMsgGuaranteedOrderedFragmented.DynClassRep, "RPC_TNLConnection_rpcMsgGuaranteedOrderedFragmented", NetClassMask.NetClassGroupGameMask, 0);
            NetEvent.ImplementNetEvent(out RPCMsgNonGuaranteedFragmented.DynClassRep,     "RPC_TNLConnection_rpcMsgNonGuaranteedFragmented",     NetClassMask.NetClassGroupGameMask, 0);
        }

        public TNLConnection()
        {
            _key = 0U;
            _oneTimeKey = 0U;
            _playerCOID = 0L;
            _fragmentCounter = 1;

            SetFixedRateParameters(50, 50, 40000, 40000);
            SetPingTimeouts(7000, 6);

            _fragmentGuaranteed = new SFragmentData();
            _fragmentNonGuaranteed = new SFragmentData();
            _fragmentGuaranteedOrdered = new SFragmentData();
        }

        ~TNLConnection()
        {
            DeleteLocalGhosts();

            CharacterManager.LogoutCharacter(this);

            Logger.WriteLog("Client ({0} | {1}) disconnected", LogType.Network, AccountId, AccountName);
        }

        public void SendPacket(Packet p, RPCGuaranteeType type)
        {
            var opcode = (uint) p.Opcode;
            Logger.WriteLog("Outgoing Packet: {0}", LogType.Network, p.Opcode);

            var arr = p.ToArray();

            using (var sw = new StreamWriter("sent.txt", true, Encoding.UTF8))
            {
                sw.WriteLine(BitConverter.ToString(arr));
                sw.WriteLine();
            }

            var arrLength = (uint) arr.Length;
            if (arrLength > 1400U)
            {
                ++_fragmentCounter;

                var doneSize = 0U;
                var count = (ushort) Math.Ceiling(arrLength / 220.0);
                for (ushort i = 0; i < count; ++i)
                {
                    var buffSize = 220U;
                    if (buffSize >= arrLength - doneSize)
                        buffSize = arrLength - doneSize;

                    var tempBuff = new byte[buffSize];

                    Array.Copy(arr, i * 220, tempBuff, 0, buffSize);

                    var stream = new ByteBuffer(tempBuff, buffSize);

                    doneSize += buffSize;

                    switch (type)
                    {
                        case RPCGuaranteeType.RPCGuaranteed:
                            rpcMsgGuaranteedFragmented(opcode, _fragmentCounter, i, count, stream);
                            break;

                        case RPCGuaranteeType.RPCGuaranteedOrdered:
                            rpcMsgGuaranteedOrderedFragmented(opcode, _fragmentCounter, i, count, stream);
                            break;

                        case RPCGuaranteeType.RPCUnguaranteed:
                            rpcMsgNonGuaranteedFragmented(opcode, _fragmentCounter, i, count, stream);
                            break;
                    }
                }
            }
            else
            {
                var stream = new ByteBuffer(arr, arrLength);

                switch (type)
                {
                    case RPCGuaranteeType.RPCGuaranteed:
                        rpcMsgGuaranteed(opcode, stream);
                        break;

                    case RPCGuaranteeType.RPCGuaranteedOrdered:
                        rpcMsgGuaranteedOrdered(opcode, stream);
                        break;

                    case RPCGuaranteeType.RPCUnguaranteed:
                        rpcMsgNonGuaranteed(opcode, stream);
                        break;
                }
            }
        }

        #region Handler

        private void HandlePacket(ByteBuffer buffer)
        {
            var packet = new Packet(buffer.GetBuffer());

            Logger.WriteLog("Incoming Packet: {0}", LogType.Network, packet.Opcode);
            try
            {
                switch (packet.Opcode)
                {
                    // Global
                    case Opcode.LoginRequest:
                        HandleLoginRequest(packet);
                        break;

                    case Opcode.LoginNewCharacter:
                        HandleLoginNewCharacter(packet);
                        break;

                    case Opcode.LoginDeleteCharacter:
                        HandleLoginDeleteCharacter(packet);
                        break;

                    case Opcode.News:
                        HandleNews(packet);
                        break;

                    case Opcode.Login:
                        HandleGlobalLogin(packet);
                        break;

                    case Opcode.Disconnect:
                        HandleDisconnect(packet);
                        break;

                    case Opcode.Chat:
                        ChatManager.HandleChat(this, packet);
                        break;

                    case Opcode.GetFriends:
                        SocialManager.GetFriends(this);
                        break;

                    case Opcode.GetEnemies:
                        SocialManager.GetEnemies(this);
                        break;

                    case Opcode.GetIgnored:
                        SocialManager.GetIgnored(this);
                        break;

                    case Opcode.AddFriend:
                        SocialManager.AddEntry(this, packet, SocialType.Friend);
                        break;

                    case Opcode.AddEnemy:
                        SocialManager.AddEntry(this, packet, SocialType.Enemy);
                        break;

                    case Opcode.AddIgnore:
                        SocialManager.AddEntry(this, packet, SocialType.Ignore);
                        break;

                    case Opcode.RemoveFriend:
                        SocialManager.RemoveEntry(this, packet.ReadPadding(4).ReadLong(), SocialType.Friend);
                        break;

                    case Opcode.RemoveEnemy:
                        SocialManager.RemoveEntry(this, packet.ReadPadding(4).ReadLong(), SocialType.Enemy);
                        break;

                    case Opcode.RemoveIgnore:
                        SocialManager.RemoveEntry(this, packet.ReadPadding(4).ReadLong(), SocialType.Ignore);
                        break;

                    case Opcode.RequestClanInfo:
                        ClanManager.RequestInfo(this);
                        break;

                    case Opcode.ConvoyMissionsRequest:
                        ConvoyManager.MissionsRequest(this);
                        break;

                    // Sector
                    case Opcode.TransferFromGlobal:
                        HandleTransferFromGlobal(packet);
                        break;

                    case Opcode.TransferFromGlobalStage2:
                        HandleTransferFromGlobalStage2(packet);
                        break;

                    case Opcode.TransferFromGlobalStage3:
                        HandleTransferFromGlobalStage3(packet);
                        break;

                    case Opcode.UpdateFirstTimeFlagsRequest:
                        HandleUpdateFirstTimeFlagsRequest(packet);
                        break;

                    case Opcode.Broadcast:
                        ChatManager.HandleBroadcast(this, packet);
                        break;

                    default:
                        Logger.WriteLog("Unhandled Opcode: {0}", LogType.Error, packet.Opcode);
                        break;
                }
            }
            catch (Exception e)
            {
                Logger.WriteLog("Caught exception while handling packets!", LogType.Error);
                Logger.WriteLog("Exception: {0}", LogType.Error, e);
            }
        }

        #endregion

        public void UpdateFirstTimeFlags(uint f1, uint f2, uint f3, uint f4)
        {
            FirstTimeFlags[0] = f1;
            FirstTimeFlags[1] = f2;
            FirstTimeFlags[2] = f3;
            FirstTimeFlags[2] = f4;

            DataAccess.Account.UpdateFirstTimeFlags(AccountId, FirstTimeFlags);
        }

        public bool LoginAccount(uint oneTimeKey, string user = null, string password = null)
        {
            var data = DataAccess.Account.LoginAccount(oneTimeKey);
            if (data == null)
                return false;

            if (user != null && (data.UserName != user || data.Password != password))
                return false;

            AccountId = data.Id;
            AccountLevel = data.Level;
            AccountName = data.UserName;
            FirstTimeFlags = new uint[4];

            Array.Copy(data.FirstTimeFlags, FirstTimeFlags, 4);

            return true;
        }

        #region RPC Calls

        // ReSharper disable InconsistentNaming
        // ReSharper disable UnusedMember.Local
        public void rpcMsgGuaranteed(uint type, ByteBuffer data)
        #region rpcMsgGuaranteed
        {
            var rpcEvent = new RPCMsgGuaranteed();
            rpcEvent.Functor.Set(new object[] { type, data });

            PostNetEvent(rpcEvent);
        }

// ReSharper disable UnusedParameter.Local
        private void rpcMsgGuaranteed_remote(uint type, ByteBuffer data)
// ReSharper restore UnusedParameter.Local
        #endregion
        {
            HandlePacket(data);
        }

        public void rpcMsgGuaranteedOrdered(uint type, ByteBuffer data)
        #region rpcMsgGuaranteedOrdered
        {
            var rpcEvent = new RPCMsgGuaranteedOrdered();
            rpcEvent.Functor.Set(new object[] { type, data });

            PostNetEvent(rpcEvent);
        }

// ReSharper disable UnusedParameter.Local
        private void rpcMsgGuaranteedOrdered_remote(uint type, ByteBuffer data)
// ReSharper restore UnusedParameter.Local
        #endregion
        {
            HandlePacket(data);
        }

        public void rpcMsgNonGuaranteed(uint type, ByteBuffer data)
        #region rpcMsgNonGuaranteed
        {
            var rpcEvent = new RPCMsgNonGuaranteed();
            rpcEvent.Functor.Set(new object[] { type, data });

            PostNetEvent(rpcEvent);
        }

// ReSharper disable UnusedParameter.Local
        private void rpcMsgNonGuaranteed_remote(uint type, ByteBuffer data)
// ReSharper restore UnusedParameter.Local
        #endregion
        {
            HandlePacket(data);
        }

        public void rpcMsgGuaranteedFragmented(uint type, ushort fragment, ushort fragmentId, ushort fragmentCount, ByteBuffer data)
        #region rpcMsgGuaranteedFragmented
        {
            var rpcEvent = new RPCMsgGuaranteedFragmented();
            rpcEvent.Functor.Set(new object[] { type, fragment, fragmentId, fragmentCount, data });

            PostNetEvent(rpcEvent);
        }

        private void rpcMsgGuaranteedFragmented_remote(uint type, ushort fragment, ushort fragmentId, ushort fragmentCount, ByteBuffer data)
        #endregion
        {
            Console.WriteLine("MsgGuaranteedFragmented | Type: {0} | Fragment: {1} | FragmentId: {2} | FragmentCount: {3}", type, fragment, fragmentId, fragmentCount);

            ProcessFragment(data, _fragmentGuaranteed, type, fragment, fragmentId, fragmentCount);
        }

        public void rpcMsgGuaranteedOrderedFragmented(uint type, ushort fragment, ushort fragmentId, ushort fragmentCount, ByteBuffer data)
        #region rpcMsgGuaranteedOrderedFragmented
        {
            var rpcEvent = new RPCMsgGuaranteedOrderedFragmented();
            rpcEvent.Functor.Set(new object[] { type, fragment, fragmentId, fragmentCount, data });

            PostNetEvent(rpcEvent);
        }

        private void rpcMsgGuaranteedOrderedFragmented_remote(uint type, ushort fragment, ushort fragmentId, ushort fragmentCount, ByteBuffer data)
        #endregion
        {
            Console.WriteLine("MsgGuaranteedOrderedFragmented | Type: {0} | Fragment: {1} | FragmentId: {2} | FragmentCount: {3}", type, fragment, fragmentId, fragmentCount);

            ProcessFragment(data, _fragmentGuaranteedOrdered, type, fragment, fragmentId, fragmentCount);
        }

        public void rpcMsgNonGuaranteedFragmented(uint type, ushort fragment, ushort fragmentId, ushort fragmentCount, ByteBuffer data)
        #region rpcMsgNonGuaranteedFragmented
        {
            var rpcEvent = new RPCMsgNonGuaranteedFragmented();
            rpcEvent.Functor.Set(new object[] { type, fragment, fragmentId, fragmentCount, data });

            PostNetEvent(rpcEvent);
        }

        private void rpcMsgNonGuaranteedFragmented_remote(uint type, ushort fragment, ushort fragmentId, ushort fragmentCount, ByteBuffer data)
        #endregion
        {
            Console.WriteLine("MsgNonGuaranteedFragmented | Type: {0} | Fragment: {1} | FragmentId: {2} | FragmentCount: {3}", type, fragment, fragmentId, fragmentCount);

            ProcessFragment(data, _fragmentNonGuaranteed, type, fragment, fragmentId, fragmentCount);
        }
        // ReSharper restore UnusedMember.Local
        // ReSharper restore InconsistentNaming

        #endregion RPC Calls

        public override NetClassRep GetClassRep()
        {
            return _dynClassRep;
        }

        public void SetPlayerCOID(long connId)
        {
            _playerCOID = connId;
        }

        public long GetPlayerCOID()
        {
            return _playerCOID;
        }

        public override NetClassGroup GetNetClassGroup()
        {
            return NetClassGroup.NetClassGroupGame;
        }

        public override void PrepareWritePacket()
        {
            
        }
        
        public void DoScoping()
        {
            base.PrepareWritePacket();
        }

        public void GetFixedRateParameters(out uint minPacketSendPeriod, out uint minPacketRecvPeriod, out uint maxSendBandwidth, out uint maxRecvBandwidth)
        {
            minPacketSendPeriod = LocalRate.MinPacketSendPeriod;
            minPacketRecvPeriod = LocalRate.MinPacketRecvPeriod;
            maxSendBandwidth    = LocalRate.MaxSendBandwidth;
            maxRecvBandwidth    = LocalRate.MaxRecvBandwidth;
        }

        public override void WriteConnectRequest(BitStream stream)
        {
            base.WriteConnectRequest(stream);

            var tInterface = Interface as TNLInterface;
            if (tInterface == null)
                return;

            stream.Write(tInterface.Version);
            stream.Write(_key);
            stream.Write(_playerCOID);
        }

        public override bool ReadConnectRequest(BitStream stream, ref string errorString)
        {
            if (!base.ReadConnectRequest(stream, ref errorString))
                return false;

            var tInterface = Interface as TNLInterface;
            if (tInterface == null)
                return false;

            int version;
            if (!stream.Read(out version) || version != tInterface.Version)
            {
                errorString = "Incorrect Version";
                return false;
            }

            if (!stream.Read(out _key))
            {
                errorString = "Unknown Key";
                return false;
            }

            if (!stream.Read(out _playerCOID))
            {
                errorString = "Unknown player ID";
                return false;
            }

            return true;
        }

        public override void OnConnectionEstablished()
        {
            var tInterface = Interface as TNLInterface;
            if (tInterface != null && !tInterface.Adaptive)
            {
                SetGhostTo(false);
                SetGhostFrom(true);
                ActivateGhosting();
            }

            SetIsAdaptive();
            SetIsConnectionToClient();

            Logger.WriteLog("Client ({1}) connected from {0}", LogType.Network, GetNetAddressString(), _playerCOID);
        }

        protected override void ComputeNegotiatedRate()
        {
            var tnlInterface = Interface as TNLInterface;

            if (tnlInterface != null && tnlInterface.UnlimitedBandwith)
            {
                CurrentPacketSendSize = 1490U;
                CurrentPacketSendPeriod = 1U;
            }
            else
                base.ComputeNegotiatedRate();
        }

        public NetObject GetGhost()
        {
            return GetScopeObject();
        }

        public int GetTimeSinceLastMessage()
        {
            return Interface.GetCurrentTime() - LastPacketRecvTime;
        }

// ReSharper disable UnusedParameter.Local
        private void ProcessFragment(ByteBuffer theData, SFragmentData sFragment, uint type, ushort fragment, ushort fragmentId, ushort fragmentCount)
// ReSharper restore UnusedParameter.Local
        {
            if (sFragment.FragmentId != fragment)
            {
                if (fragment > 0)
                    Console.WriteLine("Dropped fragment: {0} vs {1}", sFragment.FragmentId, fragment);

                sFragment.FragmentId = fragment;
                sFragment.TotalSize = 0;
                sFragment.MapFragments.Clear();
            }

            sFragment.MapFragments.Add(fragmentId, theData);
            sFragment.TotalSize += theData.GetBufferSize();

            if (sFragment.MapFragments.Count == fragmentCount)
            {
                Console.WriteLine("Reassembling fragment {0} ({1} fragments", sFragment.FragmentId, fragmentCount);

                var combined = new ByteBuffer(sFragment.TotalSize);

                var off = 0U;

                for (var i = 0; i < sFragment.MapFragments.Count; ++i)
                {
                    if (!sFragment.MapFragments.ContainsKey(i))
                    {
                        Console.WriteLine("Big error! Fragment doesn't contain a buffer! Fragment: {0} | Index: {1}", fragment, i);
                        return;
                    }

                    var buff = sFragment.MapFragments[i];

                    Array.Copy(buff.GetBuffer(), 0, combined.GetBuffer(), off, buff.GetBufferSize());
                    off += buff.GetBufferSize();
                }

                sFragment.FragmentId = 0;
                sFragment.TotalSize = 0;
                sFragment.MapFragments.Clear();

                HandlePacket(combined);
            }
        }

        private class SFragmentData
        {
            public uint FragmentId { get; set; }
            public uint TotalSize { get; set; }
            public readonly Dictionary<int, ByteBuffer> MapFragments;

            public SFragmentData()
            {
                FragmentId = 0;
                TotalSize = 0;
                MapFragments = new Dictionary<int, ByteBuffer>();
            }
        }

        #region RPC Classes

        private class RPCMsgGuaranteed : RPCEvent
        {
            public static NetClassRepInstance<RPCMsgGuaranteed> DynClassRep;
            public RPCMsgGuaranteed()
                : base(RPCGuaranteeType.RPCGuaranteed, RPCDirection.RPCDirAny)
            { Functor = new FunctorDecl<TNLConnection>("rpcMsgGuaranteed_remote", new[] { typeof(uint), typeof(ByteBuffer) }); }
            public override bool CheckClassType(object obj) { return (obj as TNLConnection) != null; }
            public override NetClassRep GetClassRep() { return DynClassRep; }
        }

        private class RPCMsgGuaranteedOrdered : RPCEvent
        {
            public static NetClassRepInstance<RPCMsgGuaranteedOrdered> DynClassRep;
            public RPCMsgGuaranteedOrdered()
                : base(RPCGuaranteeType.RPCGuaranteedOrdered, RPCDirection.RPCDirAny)
            { Functor = new FunctorDecl<TNLConnection>("rpcMsgGuaranteedOrdered_remote", new[] { typeof(uint), typeof(ByteBuffer) }); }
            public override bool CheckClassType(object obj) { return (obj as TNLConnection) != null; }
            public override NetClassRep GetClassRep() { return DynClassRep; }
        }

        private class RPCMsgNonGuaranteed : RPCEvent
        {
            public static NetClassRepInstance<RPCMsgNonGuaranteed> DynClassRep;
            public RPCMsgNonGuaranteed()
                : base(RPCGuaranteeType.RPCUnguaranteed, RPCDirection.RPCDirAny)
            { Functor = new FunctorDecl<TNLConnection>("rpcMsgNonGuaranteed_remote", new[] { typeof(uint), typeof(ByteBuffer) }); }
            public override bool CheckClassType(object obj) { return (obj as TNLConnection) != null; }
            public override NetClassRep GetClassRep() { return DynClassRep; }
        }

        private class RPCMsgGuaranteedFragmented : RPCEvent
        {
            public static NetClassRepInstance<RPCMsgGuaranteedFragmented> DynClassRep;
            public RPCMsgGuaranteedFragmented()
                : base(RPCGuaranteeType.RPCGuaranteed, RPCDirection.RPCDirAny)
            { Functor = new FunctorDecl<TNLConnection>("rpcMsgGuaranteedFragmented_remote", new[] { typeof(uint), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ByteBuffer) }); }
            public override bool CheckClassType(object obj) { return (obj as TNLConnection) != null; }
            public override NetClassRep GetClassRep() { return DynClassRep; }
        }

        private class RPCMsgGuaranteedOrderedFragmented : RPCEvent
        {
            public static NetClassRepInstance<RPCMsgGuaranteedOrderedFragmented> DynClassRep;
            public RPCMsgGuaranteedOrderedFragmented()
                : base(RPCGuaranteeType.RPCGuaranteedOrdered, RPCDirection.RPCDirAny)
            { Functor = new FunctorDecl<TNLConnection>("rpcMsgGuaranteedOrderedFragmented_remote", new[] { typeof(uint), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ByteBuffer) }); }
            public override bool CheckClassType(object obj) { return (obj as TNLConnection) != null; }
            public override NetClassRep GetClassRep() { return DynClassRep; }
        }

        private class RPCMsgNonGuaranteedFragmented : RPCEvent
        {
            public static NetClassRepInstance<RPCMsgNonGuaranteedFragmented> DynClassRep;
            public RPCMsgNonGuaranteedFragmented()
                : base(RPCGuaranteeType.RPCUnguaranteed, RPCDirection.RPCDirAny)
            { Functor = new FunctorDecl<TNLConnection>("rpcMsgNonGuaranteedFragmented_remote", new[] { typeof(uint), typeof(ushort), typeof(ushort), typeof(ushort), typeof(ByteBuffer) }); }
            public override bool CheckClassType(object obj) { return (obj as TNLConnection) != null; }
            public override NetClassRep GetClassRep() { return DynClassRep; }
        }

        #endregion
    }
}
