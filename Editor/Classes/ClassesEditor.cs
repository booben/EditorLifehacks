using UnityEditor;
using UnityEngine;

namespace CompanyName
{
    /// <summary>
    /// Класс, отвечающий за отрисовку таблицы
    /// </summary>
    public class EditorTable
    {
        private EditorTableSettings settings = null;
        private Vector2             position = new Vector2();

        /// <summary>
        /// Текущая координата X с учетом размера ячеек и смещения
        /// </summary>
        private float X
        {
            get
            {
                float result = 0;
                for (int i = 0; i < position.x; i++)
                {
                    result += settings.GetSize(i).x + settings.Offset.x;
                }

                return settings.Position.x + result;
            }
        }

        /// <summary>
        /// Текущая координата Y с учетом размера ячеек и смещения
        /// </summary>
        private float Y
        {
            get
            {
                float result = 0;
                for (int i = 0; i < position.y; i++)
                {
                    result += settings.GetSize(i).y + settings.Offset.y;
                }

                return settings.Position.y + result;
            }
        }

        /// <summary>
        /// Ширина ячейки с учетом положения
        /// </summary>
        private float Width
        {
            get
            {
                return settings.GetSize((int)position.x).x;
            }
        }

        /// <summary>
        /// Высота ячейки с учетом положения
        /// </summary>
        private float Height
        {
            get
            {
                return settings.GetSize((int)position.y).y;
            }
        }

        /// <summary>
        /// Прямоугольник, получаемый по свойствам выше
        /// </summary>
        private Rect Rect
        {
            get
            {
                return new Rect(X, Y, Width, Height);
            }
        }

        /// <summary>
        /// Задания индекса текущей строки
        /// </summary>
        /// <param name="row"></param>
        public void SetRow(int row)
        {
            position.y = row;
        }

        /// <summary>
        /// Задание индексе текущего столбца
        /// </summary>
        /// <param name="column"></param>
        public void SetColumn(int column)
        {
            position.x = column;
        }

        /// <summary>
        /// Следующий столбец
        /// </summary>
        public void NextColumn()
        {
            position.x++;
        }

        /// <summary>
        /// Следующая строка
        /// </summary>
        public void NexRow()
        {
            position.y++;
        }

        /// <summary>
        /// Метод для отрисовки строки целиком
        /// Применение, преимущественно в шапках
        /// </summary>
        /// <param name="values"></param>
        public void LabelRow(params string[] values)
        {
            float y = Y;

            for (int i = 0; i < values.Length; i++)
            {
                SetColumn(i);
                EditorGUI.LabelField(Rect, values[i]);
            }
        }

        /// <summary>
        /// Просто надпись
        /// </summary>
        /// <param name="content"></param>
        public void Label(object content)
        {
            EditorGUI.LabelField(Rect, content.ToString());
        }

        /// <summary>
        /// Целочисленное поле
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Field(int value)
        {
            return EditorGUI.IntField(Rect, value);
        }

        /// <summary>
        /// Строковое поле
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Field(string value)
        {
            return EditorGUI.TextField(Rect, value);
        }

        /// <summary>
        /// Кнопка
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public bool Button(string label)
        {
            return GUI.Button(Rect, label);
        }

        /// <summary>
        /// Конструктор, при создании таблицы сразу задаем её настройки
        /// </summary>
        /// <param name="settings"></param>
        public EditorTable(EditorTableSettings settings)
        {
            this.settings = settings;
        }
    }

    /// <summary>
    /// Класс, отвечающий за настройку таблицы
    /// </summary>
    public class EditorTableSettings
    {
        private Vector2     position = new Vector2();
        private Vector2[]   size = null;
        private Vector2     offset = new Vector2();

        /// <summary>
        /// Стартовое положение фрейма
        /// </summary>
        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        /// <summary>
        /// Размер столбцов ячеек
        /// </summary>
        public Vector2[] Size
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Смещение ячеек
        /// </summary>
        public Vector2 Offset
        {
            get
            {
                return offset;
            }
        }

        /// <summary>
        /// Получение размера по индексу столбца
        /// </summary>
        /// <param name="column">Индекс столбца</param>
        /// <returns></returns>
        public Vector2 GetSize(int column)
        {
            if (size.Length <= column)
            {
                return size[size.Length - 1];
            }

            return size[column];
        }
        
        public EditorTableSettings(Vector2 position, Vector2[] size, Vector2 offset)
        {
            this.position = position;
            this.size = size;
            this.offset = offset;
        }
    }
}