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
        public Opcode Opcode { get; private set; }

        private readonly MemoryStream _stream;
        private readonly BinaryReader _reader;
        private readonly BinaryWriter _writer;

        public Packet(Opcode opcode, Boolean writeOpcode = true)
        {
            Opcode = opcode;
            _stream = new MemoryStream();
            _writer = new BinaryWriter(_stream);

            if (writeOpcode)
                WriteOpcode(Opcode);
        }

        public Packet(Byte[] data)
        {
            _stream = new MemoryStream(data);
            _reader = new BinaryReader(_stream);

            Opcode = (Opcode) _reader.ReadUInt32();
        }

        public Packet ReadPadding(Int32 length)
        {
            _reader.ReadBytes(length);
            return this;
        }

        public Packet WritePadding(Int32 length)
        {
            for (var i = 0; i < length; ++i)
                _writer.Write((Byte) 0);

            return this;
        }

        public Byte[] ToArray()
        {
            return _stream.ToArray();
        }

        public Boolean IsFinished()
        {
            if (_reader != null)
                return _reader.BaseStream.Length == _reader.BaseStream.Position;

            return false;
        }

        public Byte[] ReadBytes(Int32 length)
        {
            return _reader.ReadBytes(length);
        }

        public UInt64 ReadULong()
        {
            return _reader.ReadUInt64();
        }

        public Int64 ReadLong()
        {
            return _reader.ReadInt64();
        }

        public UInt32 ReadUInteger()
        {
            return _reader.ReadUInt32();
        }

        public Int32 ReadInteger()
        {
            return _reader.ReadInt32();
        }

        public UInt16 ReadUShort()
        {
            return _reader.ReadUInt16();
        }

        public Int16 ReadShort()
        {
            return _reader.ReadInt16();
        }

        public Byte ReadByte()
        {
            return _reader.ReadByte();
        }

        public SByte ReadSByte()
        {
            return _reader.ReadSByte();
        }

        public Single ReadSingle()
        {
            return _reader.ReadSingle();
        }

        public Double ReadDouble()
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

        public String ReadUtf16NullString()
        {
            return _reader.ReadUtf16StringNull();
        }

        public String ReadUtf8NullString()
        {
            return _reader.ReadUtf8StringNull();
        }

        public String ReadUtf8StringOn(Int32 length)
        {
            return _reader.ReadUtf8StringOn(length);
        }

        public Boolean ReadBoolean()
        {
            return _reader.ReadBoolean();
        }

        public void WriteOpcode(Opcode opcode)
        {
            _writer.Write((UInt32) opcode);
        }

        public void WriteBytes(Byte[] value)
        {
            _writer.Write(value);
        }

        public void WriteBytes(Byte[] value, Int32 offset, Int32 length)
        {
            _writer.Write(value, offset, length);
        }

        public void WriteLong(UInt64 value)
        {
            _writer.Write(value);
        }

        public void WriteLong(Int64 value)
        {
            _writer.Write(value);
        }

        public void WriteInteger(UInt32 value)
        {
            _writer.Write(value);
        }

        public void WriteInteger(Int32 value)
        {
            _writer.Write(value);
        }

        public void WriteShort(UInt16 value)
        {
            _writer.Write(value);
        }

        public void WriteShort(Int16 value)
        {
            _writer.Write(value);
        }

        public void WriteByte(Byte value)
        {
            _writer.Write(value);
        }

        public void WriteByte(SByte value)
        {
            _writer.Write(value);
        }

        public void WriteDouble(Double value)
        {
            _writer.Write(value);
        }

        public void WriteSingle(Single value)
        {
            _writer.Write(value);
        }

        public void WriteUtf8NullString(String value)
        {
            _writer.Write(Encoding.UTF8.GetBytes(value));
            _writer.Write((Byte)0);
        }

        public void WriteUtf8StringOn(String value, Int32 len)
        {
            Debug.Assert(value.Length <= len);
            _writer.Write(Encoding.UTF8.GetBytes(value));

            for (var i = 0; i < len - value.Length; ++i)
                _writer.Write((Byte)0);
        }

        public void WriteTFID(Int64 coid, Boolean global)
        {
            WriteLong(coid);
            WriteBoolean(global);
            WritePadding(7);
        }

        public void WriteBoolean(Boolean value)
        {
            _writer.Write(value);
        }

        public void WriteTFID(TFID tfid)
        {
            WriteTFID(tfid.Coid, tfid.Global);
        }

        public override String ToString()
        {
            return String.Format("Opc: {0} | Length: {1}", Opcode, _stream.Length);
        }


        public String ToHexString()
        {
            return BitConverter.ToString(_stream.GetBuffer(), 0, (Int32)_stream.Position);
        }

        public void Dispose()
        {
            if (_reader != null)
                _reader.Close();

            if (_writer != null)
                _writer.Close();

            _stream.Close();
        }
    }
}
