using System;

namespace Genesis.Shared.Entities
{
    using Base;
    using Constant;
    using Map;

    public class Inventory
    {
        private Int32 _x;
        private Int32 _y;
        private Int32 _numPages;
        private ClonedObjectBase _owner;
        private Boolean _doubleCheckBlock;
        private Boolean _dirty;
        private Int64[] _gridSpace;
        private InventoryType _inventoryType;
        private Int32 _maxItems;
        private Int32 _colsPerPage;
        private Int32 _rowsPerPage;
        private SectorMap _map;

        public Inventory(Int32 x, Int32 y, Int32 numPages)
        {
            _map = null;
            _gridSpace = null;
            _inventoryType = InventoryType.None;
            _owner = null;
            _doubleCheckBlock = false;
            _dirty = false;
            _x = x;
            _y = y;
            _numPages = numPages;

            if (_x <= 0)
                _x = 1;

            if (_y <= 0)
                _y = 1;

            if (_numPages <= 0)
                _numPages = 1;

            CreateGridSpace();
        }

        public void CreateGridSpace()
        {
            _maxItems = _y * _x;
            _colsPerPage = _y;
            _rowsPerPage = (_y * _x) / _numPages;
            _gridSpace = new Int64[_maxItems];

            for (var i = 0; i < _maxItems; ++i)
                _gridSpace[i] = -1;
        }

        public void SetInventoryType(InventoryType type)
        {
            _inventoryType = type;
        }

        public void SetOwner(ClonedObjectBase owner)
        {
            _owner = owner;

            // TODO: foreach item in inevntory: setowner(owner)
        }

        public void SetMap(SectorMap map)
        {
            _map = map;
        }

        public static Boolean FitsInTempInventoryAtPosition(Byte sizeX, Byte sizeY, Byte posX, Byte posY, Int64[] tempInventory, Int32 totalX, Int32 totalY, Int32 rowsPerPage)
        {
            var sizeXa = sizeX + posX;
            if (sizeXa > totalX)
                return false;

            var sizeYa = sizeY + posY;
            if (sizeYa > totalY)
                return false;

            if (posY % rowsPerPage + sizeY > rowsPerPage)
                return false;

            if (posX < sizeX)
            {
                // TODO
            }

            return true;
        }

        public void SetDirtyState()
        {
            if (_owner != null)
                _owner.SetDirtyState();

            _dirty = true;
        }

        public Character GetSuperCharacter(Boolean includeSummons)
        {
            return _owner != null ? _owner.GetSuperCharacter(includeSummons) : null;
        }

        public void FindPositionByCOID(Int64 coidItem, out Byte posX, out Byte posY)
        {
            posX = 0;
            posY = 0;

            Byte loopy = 0;

            Byte currY = 0;

            if (_y > 0)
            {
                Byte currX;

                while (true)
                {
                    currX = 0;
                    if (_x > 0)
                    {
                        while (_gridSpace[currY + currX * _y] != coidItem)
                        {
                            if (++currX >= _x)
                            {
                                currY = loopy;
                                break;
                            }
                        }
                    }

                    ++currY;
                    loopy = currY;

                    if (currY >= _y)
                        return;
                }

                posX = currX;
                posY = loopy;
            }
        }

        public Boolean FitsInInventoryAtPosition(Byte sizeX, Byte sizeY, Byte posX, Byte posY, out Int64 coid)
        {
            coid = -1;
            return false;
        }
    }
}
