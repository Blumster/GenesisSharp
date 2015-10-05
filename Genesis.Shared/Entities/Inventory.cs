namespace Genesis.Shared.Entities
{
    using Base;
    using Constant;
    using Map;

    public class Inventory
    {
        private int _x;
        private int _y;
        private int _numPages;
        private ClonedObjectBase _owner;
        private bool _doubleCheckBlock;
        private bool _dirty;
        private long[] _gridSpace;
        private InventoryType _inventoryType;
        private int _maxItems;
        private int _colsPerPage;
        private int _rowsPerPage;
        private SectorMap _map;

        public Inventory(int x, int y, int numPages)
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
            _gridSpace = new long[_maxItems];

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

        public static bool FitsInTempInventoryAtPosition(byte sizeX, byte sizeY, byte posX, byte posY, long[] tempInventory, int totalX, int totalY, int rowsPerPage)
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

        public Character GetSuperCharacter(bool includeSummons)
        {
            return _owner != null ? _owner.GetSuperCharacter(includeSummons) : null;
        }

        public void FindPositionByCOID(long coidItem, out byte posX, out byte posY)
        {
            posX = 0;
            posY = 0;

            byte loopy = 0;

            byte currY = 0;

            if (_y > 0)
            {
                byte currX;

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

        public bool FitsInInventoryAtPosition(byte sizeX, byte sizeY, byte posX, byte posY, out long coid)
        {
            coid = -1;
            return false;
        }
    }
}
