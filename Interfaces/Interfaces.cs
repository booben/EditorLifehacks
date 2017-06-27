namespace CompanyName
{
    /// <summary>
    /// Интерфейс ресурса 
    /// (позже можно будет реализовать этот интерфейс в классах любых ресурсов)
    /// </summary>
    public interface IResource
    {
        int ID
        {
            get;
        }

        string Name
        {
            get;
            set;
        }
    }

    /// <summary>
    /// Интерфейс контейнера ресурсов
    /// </summary>
    public interface IContainer
    {
        IResource[] Resources
        {
            get;
            set;
        }
    }
}