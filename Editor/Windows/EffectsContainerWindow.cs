using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

namespace CompanyName
{   
    /// <summary>
    /// Класс окна редактора, отвечающего за отрисовку контейнера эффектов
    /// </summary>
    public class EffectsContainerWindow : EditorWindow
    {
        private EffectsContainer    container = null;
        private Vector2             scrollPos = new Vector2();

        private EditorTable         table = null;

        private IResource           additionalItem = null;        
        private IList<IResource>    resources = null;

        /// <summary>
        /// Открываем окно, при помощи статического метода,
        /// который вызывается при помощи добавленной нами кнопки
        /// в главное меню
        /// </summary>
        [MenuItem("Tools/Open Effects Editor")]
        private static void Open()
        {
            EffectsContainerWindow window = GetWindow<EffectsContainerWindow>();
            window.minSize = new Vector2(800, 600);
            window.titleContent.text = "Effects Editor";
            window.Init();            
        }

        /// <summary>
        /// Просто инициализация нашего окна
        /// </summary>
        private void Init()
        {
            container = Resources.Load<EffectsContainer>("EffectsContainer");

            if (container == null)
            {
                return;
            }
            
            Vector2 position = new Vector2(4, 4);
            Vector2[] size = new Vector2[3];
            size[0] = new Vector2(48, 16);
            size[1] = new Vector2(256, 16);
            size[2] = new Vector2(48, 16);
            Vector2 offset = new Vector2(4, 4);

            table = new EditorTable(new EditorTableSettings(position, size, offset));

            SetUpdated();
        }        

        private void OnGUI()
        {
            if (container == null)
            {
                return;
            }

            if(table == null)
            {
                return;
            }

            
            BeginWindows();

            //Шапка и заголовок
            Hat();

            //Строка добавления нового элемента
            AdditionRow();

            //Строки с отрисовкой элементов, уже находящихся в массиве
            Content();

            //Даем понять движку, что целевой объект изменился
            //и его нужно сохранить
            if (GUI.changed)
            {
                EditorUtility.SetDirty(container);
            }

            EndWindows();
        }

        /// <summary>
        /// Заголовок и шапка таблицы
        /// </summary>
        private void Hat()
        {
            table.SetRow(0);

            table.LabelRow("Effects");
            table.NexRow();

            table.LabelRow("ID", "Path", "Action");
        }

        /// <summary>
        /// Строка добавления нового элемента
        /// </summary>
        private void AdditionRow()
        {
            DrawItem(additionalItem, true);            
        }

        /// <summary>
        /// Отрисовка текущего списка
        /// </summary>
        private void Content()
        {
            for (int i = 0; i < resources.Count; i++)
            {
                IResource resource = resources[i];

                DrawItem(resource, false);
            }
        }

        /// <summary>
        /// Отрисовка конкретного элемента
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="add"></param>
        private void DrawItem(IResource resource, bool add)
        {
            table.NexRow();

            table.SetColumn(0);

            table.Label(resource.ID);
            table.NextColumn();

            resource.Name = table.Field(resource.Name);
            table.NextColumn();

            //Кнопка добавления или удаления элемента - напротив каждого
            if (add)
            {
                GUI.SetNextControlName("AdditionButton");
                if (table.Button("+"))
                {
                    AddItem(resource);
                }
            }
            else
            {
                if (table.Button("-"))
                {
                    RemoveItem(resource);
                }
            }
        }

        /// <summary>
        /// Получаем ID для добавления следующего элемента
        /// </summary>
        /// <returns></returns>
        private int GetAdditionalID()
        {
            //Применяем простейшую сортировку для поиска следующего id
            resources = new List<IResource>(container.Resources);

            List<IResource> sorted = new List<IResource>(resources);
            sorted.Sort((IResource one, IResource two) => { return one.ID.CompareTo(two.ID); });

            int id = -1;
            int lastId = -1;
            for (int i = 0; i < sorted.Count; i++)
            {                
                if (sorted[i].ID - lastId > 1)
                {
                    break;
                }
                lastId = sorted[i].ID;
            }

            id = lastId + 1;
            return id;
        }

        /// <summary>
        /// Добавление элемента в ресурсы
        /// </summary>
        /// <param name="additionalItem"></param>
        private void AddItem(IResource additionalItem)
        {
            resources.Add(new Effect(additionalItem));
            container.Resources = resources.ToArray();

            SetUpdated();
        }

        /// <summary>
        /// Удаление элемента.
        /// Реализация через список будет проще,
        /// но мы же не ищем легких путей.
        /// </summary>
        /// <param name="id"></param>
        private void RemoveItem(IResource removeItem)
        {
            resources.Remove(removeItem);            
            container.Resources = resources.ToArray();

            SetUpdated();
        }

        private void SetUpdated()
        {
            additionalItem = new Effect(GetAdditionalID(), "");
            GUI.FocusControl("AdditionButton");

            EditorUtility.SetDirty(container);
        }
    }
}