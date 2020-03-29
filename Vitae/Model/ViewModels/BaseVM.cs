using System;

namespace Model.ViewModels
{
    [Serializable]
    public abstract class BaseVM
    {
        public int Order { get; set; }
    }
}