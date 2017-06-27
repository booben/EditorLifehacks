using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CompanyName
{
    [CustomPropertyDrawer(typeof(IntAttribute))]
    public class IntAttributeDrawer : PropertyDrawer
    {
        protected string[]  values = null;
        protected List<int> idents = null;

        protected virtual void Init(SerializedProperty property)
        {
            if (attribute != null)
            {
                IntAttribute intAttribute = (IntAttribute)attribute;
                //можно ввести проверки на null, но, я думаю, вы сами справитесь
                IResource[] resources = Resources.Load<GameObject>(intAttribute.Path).GetComponent<IContainer>().Resources;
                values = new string[resources.Length + 1];
                idents = new List<int>(resources.Length + 1);

                //добавляем нулевой элемент для назначения -1 значения
                values[0] = "-: None";
                idents.Add(-1);
                for (int i = 0; i < resources.Length; i++)
                {
                    values[i + 1] = string.Format("{0}[{1}]", resources[i].Name, resources[i].ID);
                    idents.Add(resources[i].ID);
                }
            }
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property == null)
            {
                return;
            }

            Init(property);
            EditorGUI.BeginProperty(position, label, property);

            //рисуем подпись к переменной, для этого вычисляем отступ стандартным
            //методом EditorGUI
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            //говорим редактору, что нам не нужно никаких оступов 
            //(например, когда рисуются вложенные объекты, эти отступы прибавляются +1)
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //Вычисляем прямоугольник нашего элемента
            Rect pathRect = new Rect(position.x, position.y, position.width - 6, position.height);

            int intValue = property.intValue;
            //Используем Popup элемент для отрисовки списка значений
            //Возвращает номер элемента списка, выбранного пользователем значения
            intValue = idents[EditorGUI.Popup(pathRect, Mathf.Max(0, idents.IndexOf(intValue)), values)];
            property.intValue = intValue;

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}