using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompanyName
{
    /// <summary>
    /// Класс аттрибута для целочисленной переменной
    /// </summary>
    public class IntAttribute : PropertyAttribute
    {
        private string path = "";

        public string Path
        {
            get
            {
                return path;
            }
        }

        public IntAttribute(string path)
        {
            this.path = path;
        }
    }

    /// <summary>
    /// Класс эффекта, помечаем его сериализуемым 
    /// Это нужно для того, чтобы переменные в вашем классе
    /// можно было назначать через инспектор
    /// </summary>
    [System.Serializable]
    public class Effect : IResource
    {
        [SerializeField]
        private string  name = "";
        [SerializeField]
        private int     id = 0;

        public int ID
        {
            get
            {
                return id;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public Effect()
        {
        }

        public Effect(IResource resource)
        {
            id = resource.ID;
            name = resource.Name;
        }

        public Effect(int id, string path)
        {
            this.id = id;
            name = path;
        }
    }
}