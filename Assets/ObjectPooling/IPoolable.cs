using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleObjectPooling
{
    public interface IPoolable
    {
        public PoolMember MemberName { get; }
        public void ReturnToPool();
    }
    public enum PoolMember
    {
        Cube
    }
}
