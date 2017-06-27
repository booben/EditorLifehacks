using UnityEngine;

namespace CompanyName
{
    /// <summary>
    /// Базовый класс контейнера ресурсов
    /// </summary>
    public abstract class ResourcesContainer : MonoBehaviour, IContainer
    {
        public virtual IResource[] Resources
        {
            get;
            set;
        }
    }
}