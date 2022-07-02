using System;

namespace Game.Command
{
    public abstract class Command
    {
        public Action OnDone;
        
        public abstract void Do();
    }
}