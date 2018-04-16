using System;
using System.Linq;

namespace Subble.Core.Plugin
{
    public struct SemVersion : IEquatable<SemVersion>, IComparable<SemVersion>
    {
        public SemVersion(uint major, uint minor, uint patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        public SemVersion(string version)
        {
            var split = version?.Split('.')
                .Where(e => !string.IsNullOrEmpty(e) && UInt32.TryParse(e, out var _))
                .Select(e => UInt32.Parse(e))
                .ToArray() ?? new uint[0];

            Patch = split.Length >= 3 ? split[2] : 0;
            Minor = split.Length >= 2 ? split[1] : 0;
            Major = split.Length >= 1 ? split[0] : 0;
        }

        /// <summary>
        /// Increment when you make incompatible API changes
        /// </summary>
        public uint Major { get; }

        /// <summary>
        /// Increment when you add functionality in a backwards-compatible manner
        /// </summary>
        public uint Minor { get; }

        /// <summary>
        /// Increment when you make backwards-compatible bug fixes
        /// </summary>
        public uint Patch { get; }

        /// <summary>
        /// Check if versions are compatible, 
        /// major must match, minor must be equal or higher
        /// </summary>
        /// <param name="other">version to compare</param>
        /// <returns></returns>
        public bool IsCompatible(SemVersion other)
        {
            return Major == other.Major
                && ((Minor == other.Minor && Patch >= other.Patch)
                    || Minor > other.Minor);
        }

        public int CompareTo(SemVersion other)
        {
            if (Equals(other)) return 0;
            
            return (Major > other.Major
                || (Major == other.Major && Minor > other.Minor)
                || (Major == other.Major && Minor == other.Minor && Patch > other.Patch)) ? 1 : -1;
        }

        public bool Equals(SemVersion other)
        {
            return Major == other.Major
                && Minor == other.Minor
                && Patch == other.Patch;
        }

        public override bool Equals(object obj)
        {
            if(obj is SemVersion)
                return Equals((SemVersion)obj);

            return false;
        }

        public override int GetHashCode()
        {
            var hashCode = -639545495;
            hashCode = (hashCode * -1521134295) + base.GetHashCode();
            hashCode = (hashCode * -1521134295) + Major.GetHashCode();
            hashCode = (hashCode * -1521134295) + Minor.GetHashCode();
            return (hashCode * -1521134295) + Patch.GetHashCode();
        }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Patch}";
        }

        public static bool operator >(SemVersion p1, SemVersion p2)
            => p1.CompareTo(p2) > 0;

        public static bool operator <(SemVersion p1, SemVersion p2)
            => p1.CompareTo(p2) < 0;

        public static bool operator >=(SemVersion p1, SemVersion p2)
            => p1.CompareTo(p2) >= 0;

        public static bool operator <=(SemVersion p1, SemVersion p2)
            => p1.CompareTo(p2) <= 0;

        public static bool operator ==(SemVersion p1, SemVersion p2)
            => p1.CompareTo(p2) == 0;

        public static bool operator !=(SemVersion p1, SemVersion p2)
            => p1.CompareTo(p2) != 0;

        public static implicit operator SemVersion((uint major, uint minor, uint patch) tuple)
        {
            return new SemVersion(tuple.major, tuple.minor, tuple.patch);
        }
    }
}
