using System;
using System.Collections.Generic;

namespace Morph.Editor
{
    /// <summary>
    /// Application data
    /// </summary>
    [Serializable]
    public struct ApplicationData
    {
        public string Name;
        public string Scene;
        public List<string> Dependencies;

        public override bool Equals(object obj)
        {
            return obj != null && Equals((ApplicationData)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Scene != null ? Scene.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Dependencies != null ? Dependencies.GetHashCode() : 0);
                return hashCode;
            }
        }

        public bool Equals(ApplicationData other)
        {
            return string.Equals(Name, other.Name);
        }
    }
}
