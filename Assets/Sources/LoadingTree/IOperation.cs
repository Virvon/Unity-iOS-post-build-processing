﻿using Cysharp.Threading.Tasks;

namespace Assets.Sources.LoadingTree
{
    public interface IOperation
    {
        void Run(SharedDataBundle.SharedBundle bundle);
    }
}
