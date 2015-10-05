using System.Collections.Generic;
using System.Linq;

using TNL.NET.Entities;

namespace Genesis.Shared.TNL
{
    using Ghost;

    public class TNLInterface : NetInterface
    {
        private readonly object _lock = new object();

        public static TNLInterface Instance { get; private set; }

        public bool UnlimitedBandwith { get; }
        public bool Adaptive { get; private set; }
        public int Version { get; private set; }
        public ushort FragmentSize { get; private set; }
        public long ConnectionId { get; private set; }
        public Dictionary<long, TNLConnection> MapConnections { get; }
        public Dictionary<long, NetObject> Ghosts { get; }

        public static void RegisterNetClassReps()
        {
            GhostObject.RegisterNetClassReps();
            GhostCreature.RegisterNetClassReps();
            GhostCharacter.RegisterNetClassReps();
            GhostVehicle.RegisterNetClassReps();

            TNLConnection.RegisterNetClassReps();
        }

        public TNLInterface(int port, bool adaptive, int version, bool unlimitedBandwith)
            : base(port)
        {
            Adaptive = adaptive;
            Version = version;
            UnlimitedBandwith = unlimitedBandwith;
            FragmentSize = 220;
            ConnectionId = 0;
            MapConnections = new Dictionary<long, TNLConnection>();
            Ghosts = new Dictionary<long, NetObject>();

            Instance = this;
        }

        public TNLConnection FindConnection(long connectionId)
        {
            lock (_lock)
                return MapConnections.ContainsKey(connectionId) ? MapConnections[connectionId] : null;
        }

        public override void AddConnection(NetConnection conn)
        {
            var tConn = conn as TNLConnection;
            if (tConn != null)
            {
                lock (_lock)
                {
                    var connId = ConnectionId++;
                    tConn.SetPlayerCOID(connId);
                    MapConnections.Add(connId, tConn);
                }
            }

            if (UnlimitedBandwith)
                conn.SetPingTimeouts(3000, 10);

            base.AddConnection(conn);
        }

        protected override void RemoveConnection(NetConnection conn)
        {
            var tConn = conn as TNLConnection;
            if (tConn != null)
                lock (_lock)
                    MapConnections.Remove(tConn.GetPlayerCOID());

            base.RemoveConnection(conn);
        }

        public void AddGhost(TNLConnection conn, NetObject ghost)
        {
            Ghosts.Add(conn.GetPlayerCOID(), ghost);
        }

        public void RemoveGhost(TNLConnection conn)
        {
            Ghosts.Remove(conn.GetPlayerCOID());
        }

        public void DoScoping()
        {
            foreach (var conn in MapConnections)
            {
                var temp = conn;

                foreach (var obj in from ghost in Ghosts let obj = temp.Value.GetScopeObject() where obj != null && obj != ghost.Value select obj)
                    conn.Value.ObjectInScope(obj);
            }
        }
    }
}
