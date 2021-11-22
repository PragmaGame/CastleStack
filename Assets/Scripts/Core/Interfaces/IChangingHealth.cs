using System;

namespace Core
{
    public interface IChangingHealth
    {
        public event Action<int> ChangedHealthEvent;
    }
}