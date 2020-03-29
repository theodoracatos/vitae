using Microsoft.AspNetCore.Mvc;

using System;

namespace Model.ViewModels
{
    [Serializable]
    public abstract class BaseVM
    {
        public bool Collapsed { get; set; }
        public int Order { get; set; }
    }
}