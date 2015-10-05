using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace Genesis.Shared
{
    using Constant;
    using Utils.Extensions;

    public class Packet
    {
        public Opcode Opcode { get; }

        private readonly MemoryStream _stream;
        private readonly BinaryReader _reader;
        private readonly BinaryWriter _writer;

        public Packet(Opcode opcode, bool writeOpcode = true)
        {
            Opcode = opcode;
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream);

            if (writeOpcode)
                WriteOpcode(Opcode);
        }

        public Packet(byte[] data)
        {
            _stream = new MemoryStream(data);
            _reader = new BinaryReader(_stream);

            Opcode = (Opcode) _reader.ReadUInt32();
        }

        public Packet ReadPadding(int length)
        {
            _reader.ReadBytes(length);
            return this;
        }

        public Packet WritePadding(int length)
        {
            for (var i = 0; i < length; ++i)
                _writer.Write((byte) 0);

            return this;
        }

        public byte[] ToArray()
        {
            return _stream.ToArray();
        }

        public bool IsFinished()
        {
            return _reader?.BaseStream.Length == _reader?.BaseStream.Position && _reader != null;
        }

        public byte[] ReadBytes(int length)
        {
            return _reader.ReadBytes(length);
        }

        public ulong ReadULong()
        {
            return _reader.ReadUInt64();
        }

        public long ReadLong()
        {
            return _reader.ReadInt64();
        }

        public uint ReadUInteger()
        {
            return _reader.ReadUInt32();
        }

        public int ReadInteger()
        {
            return _reader.ReadInt32();
        }

        public ushort ReadUShort()
        {
            return _reader.ReadUInt16();
        }

        public short ReadShort()
        {
            return _reader.ReadInt16();
        }

        public byte ReadByte()
        {
            return _reader.ReadByte();
        }

        public sbyte ReadSByte()
        {
            return _reader.ReadSByte();
        }

        public float ReadSingle()
        {
            return _reader.ReadSingle();
        }

        public double ReadDouble()
        {
            return _reader.ReadDouble();
        }

        public TFID ReadTFID()
        {
            var id = new TFID
            {
                Coid = ReadLong(),
                Global = ReadBoolean()
            };
            ReadPadding(7);
            return id;
        }

        public string ReadUtf16NullString()
        {
            return _reader.ReadUtf16StringNull();
        }

        public string ReadUtf8NullString()
        {
            return _reader.ReadUtf8StringNull();
        }

        public string ReadUtf8StringOn(int length)
        {
            return _reader.ReadUtf8StringOn(length);
        }

        public bool ReadBoolean()
        {
            return _reader.ReadBoolean();
        }

        public void WriteOpcode(Opcode opcode)
        {
            _writer.Write((uint) opcode);
        }

        public void WriteBytes(byte[] value)
        {
            _writer.Write(value);
        }

        public void WriteBytes(byte[] value, int offset, int length)
        {
            _writer.Write(value, offset, length);
        }

        public void WriteLong(ulong value)
        {
            _writer.Write(value);
        }

        public void WriteLong(long value)
        {
            _writer.Write(value);
        }

        public void WriteInteger(uint value)
        {
            _writer.Write(value);
        }

        public void WriteInteger(int value)
        {
            _writer.Write(value);
        }

        public void WriteShort(ushort value)
        {
            _writer.Write(value);
        }

        public void WriteShort(short value)
        {
            _writer.Write(value);
        }

        public void WriteByte(byte value)
        {
            _writer.Write(value);
        }

        public void WriteByte(sbyte value)
        {
            _writer.Write(value);
        }

        public void WriteDouble(double value)
        {
            _writer.Write(value);
        }

        public void WriteSingle(float value)
        {
            _writer.Write(value);
        }

        public void WriteUtf8NullString(string value)
        {
            _writer.Write(Encoding.UTF8.GetBytes(value));
            _writer.Write((byte)0);
        }

        public void WriteUtf8StringOn(string value, int len)
        {
            Debug.Assert(value.Length <= len);
            _writer.Write(Encoding.UTF8.GetBytes(value));

            for (var i = 0; i < len - value.Length; ++i)
                _writer.Write((byte)0);
        }

        public void WriteTFID(long coid, bool global)
        {
            WriteLong(coid);
            WriteBoolean(global);
            WritePadding(7);
        }

        public void WriteBoolean(bool value)
        {
            _writer.Write(value);
        }

        public void WriteTFID(TFID tfid)
        {
            WriteTFID(tfid.Coid, tfid.Global);
        }

        public override string ToString()
        {
            return $"Opc: {Opcode} | Length: {_stream.Length}";
        }


        public string ToHexString()
        {
            return BitConverter.ToString(_stream.GetBuffer(), 0, (int)_stream.Position);
        }

        public void Dispose()
        {
            _reader?.Close();
            _writer?.Close();
            _stream.Close();
        }
    }
}
