using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySeventeen
{
    public readonly struct ConwayCubeInfo
    {
        private readonly Guid _id;

        public Point Point { get; }

        public Guid Id => _id;

        public Boolean IsActive { get; }

        public ConwayCubeInfo(in Point point, in Boolean active)
        {
            Point = new Point(point.X, point.Y, point.Z, point.W);
            _id = Guid.NewGuid();
            IsActive = active;
        }
        public ConwayCubeInfo(in Int64 x, in Int64 y, in Int64 z, in Int64 w, in Boolean active)
        {
            Point = new Point(in x, in y, in z, in w);
            _id = Guid.NewGuid();
            IsActive = active;
        }
    }
}
