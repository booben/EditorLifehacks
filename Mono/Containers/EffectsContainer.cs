using UnityEngine;

namespace CompanyName
{
    /// <summary>
    /// Класс контейнера эффектов
    /// </summary>
    [AddComponentMenu("CompanyName/Containers/Effects")]
    public class EffectsContainer : ResourcesContainer
    {
        [SerializeField]
        private Effect[] effects = null;

        /// <summary>
        /// Перегружаем свойство класса-родителя
        /// </summary>
        public override IResource[] Resources
        {
            get
            {
                return effects;
            }

            set
            {
                IResource[] res = value;
                effects = new Effect[res.Length];

                for (int i = 0; i < effects.Length; i++)
                {
                    effects[i] = (Effect)res[i];
                }                
            }
        }
    }
}