using Game.Application.Commands;
using Zenject;

namespace Game.Application
{
    public class ApplicationLauncher 
    {
        public ApplicationLauncher(
            IInstantiator instantiator)
        {
            var command = instantiator.Instantiate<InitGameSceneCommand>();
            command.OnDone += () =>
            {
                var command2 = instantiator.Instantiate<LaunchGameCommand>();
                command2.Do();
            };
            command.Do();
        }
    }
}
