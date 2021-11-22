using System;
using UnityEngine;

namespace Core
{
    public interface IChangingViewAffiliation
    {
        public event Action<int> ChangedViewAffiliationEvent;
    }
}