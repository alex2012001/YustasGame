using Game.Application.Commands;
using UniRx;
using UnityEngine;

namespace Game.Points
{
    public class PointsEffector
    {
        private int _standardCounter;

        private const int RottenBaseChance = 10;
        private const int MagicBaseChance = 10;
        private const int RottenStandardModifyOfChance = 5;
        
        public PointsEffector()
        {
            MessageBroker.Default.Receive<OnRestartMessage>()
                .Subscribe((message => _standardCounter = 0));
        }
        
        public PointEffects CreateEffect()
        {
            var value = Random.Range(0, 100);

            var rottenChance = RottenBaseChance + RottenStandardModifyOfChance * _standardCounter;

            if (value <= rottenChance)
            {
                _standardCounter = 0;
                return PointEffects.Rotten;
            }
            
            value = Random.Range(0, 100);

            if (value <= MagicBaseChance)
            {
                // В задании было спорно написанно, но решил добавить увеличение шанса появления протухших корзин
                // при спавне магической корзины
                _standardCounter++;
                return PointEffects.Magic;
            }

            _standardCounter++;
            return PointEffects.Standard;
        }
    }
    public enum PointEffects
    {
        Standard,
        Rotten,
        Magic
    }
}